using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Data.HashFunction.Utilities.IntegerManipulation;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Implementation of Bob Jenkins' Lookup3 hash function as specified at http://burtleburtle.net/bob/c/lookup3.c.
    /// </summary>
    public class JenkinsLookup3
        : HashFunctionBase
    {

        /// <summary>
        /// First seed value for hash calculation.
        /// </summary>
        /// <value>
        /// The first seed value for hash calculation.
        /// </value>
        /// <remarks>
        /// Only seed value for 32-bit mode, first seed value for 64-bit mode.
        /// </remarks>
        public UInt32 InitVal1 { get { return _InitVal1; } }

        /// <summary>
        /// Second seed value for hash calculation.
        /// </summary>
        /// <value>
        /// The second seed value for hash calculation.
        /// </value>
        /// <remarks>
        /// Not used for 32-bit mode, second seed value for 64-bit mode.
        /// </remarks>
        public UInt32 InitVal2 { get { return _InitVal2; } }


        /// <summary>
        /// The list of possible hash sizes that can be provided to the <see cref="JenkinsLookup3" /> constructor.
        /// </summary>
        /// <value>
        /// The list of valid hash sizes.
        /// </value>
        public static IEnumerable<int> ValidHashSizes { get { return _ValidHashSizes; } }


        /// <inheritdoc />
        protected override bool RequiresSeekableStream { get { return true; } }


        private readonly UInt32 _InitVal1;
        private readonly UInt32 _InitVal2;

        private static readonly IEnumerable<int> _ValidHashSizes = new[] { 32, 64 };



        /// <remarks>
        /// Defaults <see cref="InitVal1" /> to 0. <inheritdoc cref="JenkinsLookup3(UInt32)" />
        /// </remarks>
        /// <inheritdoc cref="JenkinsLookup3(UInt32)" />
        public JenkinsLookup3()
            : this(0U)
        {
            
        }

        /// <remarks>
        /// Defaults <see cref="InitVal2" /> to 0. <inheritdoc cref="JenkinsLookup3(UInt32, UInt32)" />
        /// </remarks>
        /// <inheritdoc cref="JenkinsLookup3(UInt32, UInt32)" />
        public JenkinsLookup3(UInt32 initVal1)
            : this(initVal1, 0U)
        {

        }

        /// <remarks>
        /// Defaults <see cref="HashFunctionBase.HashSize" /> to 32.
        /// </remarks>
        /// <inheritdoc cref="JenkinsLookup3(int, UInt32, UInt32)" />
        public JenkinsLookup3(UInt32 initVal1, UInt32 initVal2)
            : this(32, initVal1, initVal2)
        {

        }

        /// <remarks>
        /// Defaults <see cref="InitVal1" /> to 0. <inheritdoc cref="JenkinsLookup3(int, UInt32)" />
        /// </remarks>
        /// <inheritdoc cref="JenkinsLookup3(int, UInt32)" />
        public JenkinsLookup3(int hashSize)
            : this(hashSize, 0U)
        {

        }

        /// <remarks>
        /// Defaults <see cref="InitVal2" /> to 0.
        /// </remarks>
        /// <inheritdoc cref="JenkinsLookup3(int, UInt32, UInt32)" />
        public JenkinsLookup3(int hashSize, UInt32 initVal1)
            : this(hashSize, initVal1, 0U)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JenkinsLookup3"/> class.
        /// </summary>
        /// <param name="hashSize"><inheritdoc cref="HashFunctionBase(int)"/></param>
        /// <param name="initVal1"><inheritdoc cref="InitVal1"/></param>
        /// <param name="initVal2"><inheritdoc cref="InitVal2"/></param>
        /// <exception cref="System.ArgumentOutOfRangeException">hashSize;hashSize must be contained within SpookyHashV2.ValidHashSizes.</exception>
        /// <inheritdoc cref="HashFunctionBase(int)"/>
        public JenkinsLookup3(int hashSize, UInt32 initVal1, UInt32 initVal2)
            : base(hashSize)
        {
            if (!ValidHashSizes.Contains(hashSize))
                throw new ArgumentOutOfRangeException("hashSize", "hashSize must be contained within SpookyHashV2.ValidHashSizes.");

            _InitVal1 = initVal1;
            _InitVal2 = initVal2;
        }



        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(Stream data)
        {
            UInt32 a = 0xdeadbeef + (UInt32) data.Length + InitVal1;
            UInt32 b = 0xdeadbeef + (UInt32) data.Length + InitVal1;
            UInt32 c = 0xdeadbeef + (UInt32) data.Length + InitVal1;

            if (HashSize == 64)
                c += InitVal2;


            var dataGroups = data.AsGroupedStreamData(12);
            int dataCount = 0;
            
            foreach (var dataGroup in dataGroups)
            {
                a += BitConverter.ToUInt32(dataGroup, 0);
                b += BitConverter.ToUInt32(dataGroup, 4);
                c += BitConverter.ToUInt32(dataGroup, 8);

                Mix(ref a, ref b, ref c);

                dataCount += dataGroup.Length;
            }


            var remainder = dataGroups.Remainder;

            switch (remainder.Length)
            {
                case 11: c += (UInt32) remainder[10] << 16;    goto case 10;
                case 10: c += (UInt32) remainder[ 9] << 8;     goto case  9;
                case  9: c += (UInt32) remainder[ 8];           goto case 8;   

                case 8:
                    b += BitConverter.ToUInt32(remainder, 4);
                    goto case 4;

                case 7: b += (UInt32) remainder[6] << 16;  goto case 6;
                case 6: b += (UInt32) remainder[5] <<  8;  goto case 5;
                case 5: b += (UInt32) remainder[4];        goto case 4;

                case 4:
                    a += BitConverter.ToUInt32(remainder, 0);
                    break;
                    
                case 3: a += (UInt32) remainder[2] << 16;  goto case 2;
                case 2: a += (UInt32) remainder[1] <<  8;  goto case 1;
                case 1: a += (UInt32) remainder[0];        break;
            }

            if (remainder.Length != 0 || dataCount == 12)
                Final(ref a, ref b, ref c);

            switch (HashSize)
            {
                case 32:
                    return BitConverter.GetBytes(c);
                case 64:
                    return BitConverter.GetBytes((((UInt64) b) << 32) | c);
                default:
                    throw new InvalidOperationException("HashSize set to an invalid value.");
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Mix(ref UInt32 a, ref UInt32 b, ref UInt32 c)
        {
            a -= c; a ^= c.RotateLeft( 4); c += b;
            b -= a; b ^= a.RotateLeft( 6); a += c;
            c -= b; c ^= b.RotateLeft( 8); b += a;

            a -= c; a ^= c.RotateLeft(16); c += b;
            b -= a; b ^= a.RotateLeft(19); a += c;
            c -= b; c ^= b.RotateLeft( 4); b += a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Final(ref UInt32 a, ref UInt32 b, ref UInt32 c)
        {
            c ^= b; c -= b.RotateLeft(14);
            a ^= c; a -= c.RotateLeft(11);
            b ^= a; b -= a.RotateLeft(25);

            c ^= b; c -= b.RotateLeft(16);
            a ^= c; a -= c.RotateLeft( 4);
            b ^= a; b -= a.RotateLeft(14);

            c ^= b; c -= b.RotateLeft(24);
        }
    }
}
