using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Data.HashFunction.Utilities.IntegerManipulation;
using System.Data.HashFunction.Utilities.UnifiedData;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Implementation of Bob Jenkins' Lookup2 hash function as specified at http://burtleburtle.net/bob/c/lookup2.c and http://www.burtleburtle.net/bob/hash/doobs.html.
    /// 
    /// This hash function has been superseded by JenkinsLookup3.
    /// </summary>
    public class JenkinsLookup2
        : HashFunctionAsyncBase
    {
        /// <summary>
        /// Seed value for hash calculation.
        /// </summary>
        /// <value>
        /// The seed value for hash calculation.
        /// </value>
        public UInt32 InitVal { get { return _InitVal; } }


        private readonly UInt32 _InitVal;



        /// <remarks>
        /// Defaults <see cref="InitVal" /> to 0. <inheritdoc cref="JenkinsLookup2(UInt32)" />
        /// </remarks>
        /// <inheritdoc cref="JenkinsLookup2(UInt32)" />
        public JenkinsLookup2()
            : this(0U)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JenkinsLookup2"/> class.
        /// </summary>
        /// <param name="initVal"><inheritdoc cref="InitVal" /></param>
        /// <inheritdoc cref="HashFunctionBase(int)" />
        public JenkinsLookup2(UInt32 initVal)
            : base(32)
        {
            _InitVal = initVal;
        }



        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(UnifiedData data)
        {
            if (HashSize != 32)
                throw new InvalidOperationException("HashSize set to an invalid value.");

            UInt32 a = 0x9e3779b9;
            UInt32 b = 0x9e3779b9;
            UInt32 c = InitVal;

            int dataCount = 0;

            data.ForEachGroup(12, 
                dataGroup => {
                    dataCount += ProcessGroup(ref a, ref b, ref c, dataGroup);
                }, 
                remainder => {
                    dataCount += ProcessRemainder(ref a, ref b, ref c, remainder);
                });

            c += (UInt32) dataCount;

            Mix(ref a, ref b, ref c);


            return BitConverter.GetBytes(c);
        }

        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override async Task<byte[]> ComputeHashAsyncInternal(UnifiedData data)
        {
            if (HashSize != 32)
                throw new InvalidOperationException("HashSize set to an invalid value.");

            UInt32 a = 0x9e3779b9;
            UInt32 b = 0x9e3779b9;
            UInt32 c = InitVal;

            int dataCount = 0;

            await data.ForEachGroupAsync(12, 
                dataGroup => {
                    dataCount += ProcessGroup(ref a, ref b, ref c, dataGroup);
                }, 
                remainder => {
                    dataCount += ProcessRemainder(ref a, ref b, ref c, remainder);
                }).ConfigureAwait(false);

            c += (UInt32) dataCount;

            Mix(ref a, ref b, ref c);


            return BitConverter.GetBytes(c);
        }


        private static int ProcessGroup(ref UInt32 a, ref UInt32 b, ref UInt32 c, byte[] dataGroup)
        {
            a += BitConverter.ToUInt32(dataGroup, 0);
            b += BitConverter.ToUInt32(dataGroup, 4);
            c += BitConverter.ToUInt32(dataGroup, 8);

            Mix(ref a, ref b, ref c);

            return dataGroup.Length;
        }

        private static int ProcessRemainder(ref UInt32 a, ref UInt32 b, ref UInt32 c, byte[] remainder)
        {
            // All the case statements fall through on purpose
            switch (remainder.Length)
            {
                case 11: c += (UInt32) remainder[10] << 24; goto case 10;
                case 10: c += (UInt32) remainder[ 9] << 16; goto case 9;
                case 9:  c += (UInt32) remainder[ 8] <<  8; goto case 8;
                // the first byte of c is reserved for the length

                case 8:
                    b += BitConverter.ToUInt32(remainder, 4);
                    goto case 4;

                case 7: b += (UInt32) remainder[6] << 16; goto case 6;
                case 6: b += (UInt32) remainder[5] <<  8; goto case 5;
                case 5: b += (UInt32) remainder[4];       goto case 4;

                case 4:
                    a += BitConverter.ToUInt32(remainder, 0);
                    break;

                case 3: a += (UInt32) remainder[2] << 16; goto case 2;
                case 2: a += (UInt32) remainder[1] <<  8; goto case 1;
                case 1:
                    a += (UInt32) remainder[0];
                    break;
            }

            return remainder.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Mix(ref UInt32 a, ref UInt32 b, ref UInt32 c)
        {
            a -= b; a -= c; a ^= (c >> 13);
            b -= c; b -= a; b ^= (a << 8);
            c -= a; c -= b; c ^= (b >> 13);

            a -= b; a -= c; a ^= (c >> 12);
            b -= c; b -= a; b ^= (a << 16);
            c -= a; c -= b; c ^= (b >> 5);

            a -= b; a -= c; a ^= (c >> 3);
            b -= c; b -= a; b ^= (a << 10);
            c -= a; c -= b; c ^= (b >> 15);
        }
    }
}
