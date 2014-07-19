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
    /// Implementation of MurmurHash3 as specified at https://code.google.com/p/smhasher/source/browse/trunk/MurmurHash3.cpp 
    ///   and https://code.google.com/p/smhasher/wiki/MurmurHash3.
    /// </summary>
    public class MurmurHash3
        : HashFunctionBase
    {
        /// <inheritdoc/>
        public override IEnumerable<int> ValidHashSizes
        {
            get { return new[] { 32, 128 }; }
        }

        /// <summary>
        /// Seed value for hash calculation.
        /// </summary>
        public UInt32 Seed { get; set; }


        /// <summary>Constant c1 for 32-bit calculation as defined by MurmurHash3 specification.</summary>
        protected const UInt32 c1_32 = 0xcc9e2d51;

        /// <summary>Constant c2 for 32-bit calculation as defined by MurmurHash3 specification.</summary>
        protected const UInt32 c2_32 = 0x1b873593;


        /// <summary>Constant c1 for 128-bit calculation as defined by MurMurHash3 specification.</summary>
        protected const UInt64 c1_128 = 0x87c37b91114253d5;

        /// <summary>Constant c2 for 128-bit calculation as defined by MurMurHash3 specification.</summary>
        protected const UInt64 c2_128 = 0x4cf5ad432745937f;




        /// <summary>
        /// Constructs new <see cref="MurmurHash3"/> instance.
        /// </summary>
        public MurmurHash3()
            : base(32)
        {
            Seed = 0;
        }


        /// <inheritdoc/>
        protected override byte[] ComputeHashInternal(Stream data)
        {
            switch (HashSize)
            {
                case 32:
                    return ComputeHash32(data);

                case 128:
                    return ComputeHash128(data);

                default:
                    throw new ArgumentOutOfRangeException("HashSize");
            }
        }


        /// <summary>
        /// Computes 32-bit hash value for given stream.
        /// </summary>
        /// <param name="data">Stream of data to hash.</param>
        /// <returns>Hash value of the data.</returns>
        protected byte[] ComputeHash32(Stream data)
        {
            UInt32 h1 = Seed;

            var dataGroups = data.AsGroupedStreamData(4);
            int dataCount = 0;


            foreach (var dataGroup in dataGroups)
            {
                UInt32 k1 = BitConverter.ToUInt32(dataGroup, 0);

                k1 *= c1_32;
                k1  = k1.RotateLeft(15);
                k1 *= c2_32;
   
                h1 ^= k1;
                h1  = h1.RotateLeft(13);
                h1 = (h1 * 5) + 0xe6546b64;

                dataCount += dataGroup.Length;
            }


            var remainder = dataGroups.Remainder;
            UInt32 k2 = 0;

            switch(remainder.Length)
            {
                case 3: k2 ^= (UInt32) remainder[2] << 16;   goto case 2;
                case 2: k2 ^= (UInt32) remainder[1] <<  8;   goto case 1;
                case 1: 
                    k2 ^= (UInt32) remainder[0];
                    k2 *= c1_32;
                    k2  = k2.RotateLeft(15); 
                    k2 *= c2_32; 
                    h1 ^= k2;
                    break;
            }

            dataCount += remainder.Length;
            

            h1 ^= (UInt32) dataCount;
            h1  = Mix(h1);

            return BitConverter.GetBytes(h1);
        }

        /// <summary>
        /// Computes 64-bit hash value for given byte stream.
        /// </summary>
        /// <param name="data">Stream of data to hash.</param>
        /// <returns>Hash value of the data.</returns>
        protected byte[] ComputeHash128(Stream data)
        {
            UInt64 h1 = (UInt64) Seed;
            UInt64 h2 = (UInt64) Seed;

            var dataGroups = data.AsGroupedStreamData(16);
            int dataCount = 0;

            
            foreach (var dataGroup in dataGroups)
            {
                UInt64 k1 = BitConverter.ToUInt64(dataGroup, 0);
                UInt64 k2 = BitConverter.ToUInt64(dataGroup, 8);

                k1 *= c1_128;
                k1  = k1.RotateLeft(31); 
                k1 *= c2_128; 
                h1 ^= k1;

                h1  = h1.RotateLeft(27); 
                h1 += h2; 
                h1  = (h1 * 5) + 0x52dce729;

                k2 *= c2_128; 
                k2  = k2.RotateLeft(33); 
                k2 *= c1_128; 
                h2 ^= k2;

                h2  = h2.RotateLeft(31); 
                h2 += h1; 
                h2  = (h2 * 5) + 0x38495ab5;

                dataCount += dataGroup.Length;
            }

            //----------
            // tail

            var remainder = dataGroups.Remainder;

            if (remainder.Length > 0)
            {
                UInt64 k1 = 0;
                UInt64 k2 = 0;

                switch(remainder.Length)
                {
                    case 15: k2 ^= (UInt64) remainder[14] << 48;   goto case 14;
                    case 14: k2 ^= (UInt64) remainder[13] << 40;   goto case 13;
                    case 13: k2 ^= (UInt64) remainder[12] << 32;   goto case 12;
                    case 12: k2 ^= (UInt64) remainder[11] << 24;   goto case 11;
                    case 11: k2 ^= (UInt64) remainder[10] << 16;   goto case 10;
                    case 10: k2 ^= (UInt64) remainder[ 9] <<  8;   goto case 9;
                    case  9: 
                        k2 ^= ((UInt64) remainder[8]) <<  0;
                        k2 *= c2_128; 
                        k2  = k2.RotateLeft(33); 
                        k2 *= c1_128; h2 ^= k2;

                        goto case 8;

                    case  8:
                        k1 = BitConverter.ToUInt64(remainder, 0);
                        break;

                    case  7: k1 ^= (UInt64) remainder[6] << 48;    goto case 6;
                    case  6: k1 ^= (UInt64) remainder[5] << 40;    goto case 5;
                    case  5: k1 ^= (UInt64) remainder[4] << 32;    goto case 4;
                    case  4: k1 ^= (UInt64) remainder[3] << 24;    goto case 3;
                    case  3: k1 ^= (UInt64) remainder[2] << 16;    goto case 2;
                    case  2: k1 ^= (UInt64) remainder[1] <<  8;    goto case 1;
                    case  1: 
                        k1 ^= (UInt64) remainder[0] << 0;
                        break;
                }

                k1 *= c1_128;
                k1  = k1.RotateLeft(31);
                k1 *= c2_128;
                h1 ^= k1;

                dataCount += remainder.Length;
            }


            h1 ^= (UInt64) dataCount; 
            h2 ^= (UInt64) dataCount;

            h1 += h2;
            h2 += h1;

            h1 = Mix(h1);
            h2 = Mix(h2);

            h1 += h2;
            h2 += h1;


            var hashBytes = new byte[16];

            BitConverter.GetBytes(h1)
                .CopyTo(hashBytes, 0);

            BitConverter.GetBytes(h2)
                .CopyTo(hashBytes, 8);

            return hashBytes;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UInt32 Mix(UInt32 h)
        {
            h ^= h >> 16;
            h *= 0x85ebca6b;
            h ^= h >> 13;
            h *= 0xc2b2ae35;
            h ^= h >> 16;

            return h;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UInt64 Mix(UInt64 k)
        {
            k ^= k >> 33;
            k *= 0xff51afd7ed558ccd;
            k ^= k >> 33;
            k *= 0xc4ceb9fe1a85ec53;
            k ^= k >> 33;

            return k;
        }
    }
}
