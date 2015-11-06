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
#if !NET40 || INCLUDE_ASYNC
        : HashFunctionAsyncBase
#else
        : HashFunctionBase
#endif
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



        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(UnifiedData data)
        {
            UInt32 a = 0x9e3779b9;
            UInt32 b = 0x9e3779b9;
            UInt32 c = InitVal;

            int dataCount = 0;

            data.ForEachGroup(12, 
                (dataGroup, position, length) => {
                    ProcessGroup(ref a, ref b, ref c, dataGroup, position, length);

                    dataCount += length;
                }, 
                (remainder, position, length) => {
                    ProcessRemainder(ref a, ref b, ref c, remainder, position, length);

                    dataCount += length;
                });

            c += (UInt32) dataCount;

            Mix(ref a, ref b, ref c);


            return BitConverter.GetBytes(c);
        }
        
#if !NET40 || INCLUDE_ASYNC
        /// <inheritdoc />
        protected override async Task<byte[]> ComputeHashAsyncInternal(UnifiedData data)
        {
            UInt32 a = 0x9e3779b9;
            UInt32 b = 0x9e3779b9;
            UInt32 c = InitVal;

            int dataCount = 0;

            await data.ForEachGroupAsync(12, 
                (dataGroup, position, length) => {
                    ProcessGroup(ref a, ref b, ref c, dataGroup, position, length);

                    dataCount += length;
                }, 
                (remainder, position, length) => {
                    ProcessRemainder(ref a, ref b, ref c, remainder, position, length);

                    dataCount += length;
                }).ConfigureAwait(false);

            c += (UInt32) dataCount;

            Mix(ref a, ref b, ref c);


            return BitConverter.GetBytes(c);
        }
#endif


#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static void ProcessGroup(ref UInt32 a, ref UInt32 b, ref UInt32 c, byte[] dataGroup, int position, int length)
        {
            for (var x = position; x < position + length; x += 12)
            {
                a += BitConverter.ToUInt32(dataGroup, x);
                b += BitConverter.ToUInt32(dataGroup, x + 4);
                c += BitConverter.ToUInt32(dataGroup, x + 8);

                Mix(ref a, ref b, ref c);
            }
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static void ProcessRemainder(ref UInt32 a, ref UInt32 b, ref UInt32 c, byte[] remainder, int position, int length)
        {
            // All the case statements fall through on purpose
            switch (length)
            {
                case 11: c += (UInt32) remainder[position + 10] << 24;  goto case 10;
                case 10: c += (UInt32) remainder[position +  9] << 16;  goto case  9;
                case  9: c += (UInt32) remainder[position +  8] <<  8;  goto case  8;
                // the first byte of c is reserved for the length

                case 8:
                    b += BitConverter.ToUInt32(remainder, position + 4);
                    goto case 4;

                case 7: b += (UInt32) remainder[position + 6] << 16;    goto case 6;
                case 6: b += (UInt32) remainder[position + 5] <<  8;    goto case 5;
                case 5: b += (UInt32) remainder[position + 4];          goto case 4;

                case 4:
                    a += BitConverter.ToUInt32(remainder, position);
                    break;

                case 3: a += (UInt32) remainder[position + 2] << 16;    goto case 2;
                case 2: a += (UInt32) remainder[position + 1] <<  8;    goto case 1;
                case 1:
                    a += (UInt32) remainder[position];
                    break;
            }
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
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
