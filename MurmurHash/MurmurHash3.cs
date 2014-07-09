using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    public class MurmurHash3
        : HashFunctionBase
    {
        public override IEnumerable<int> ValidHashSizes
        {
            get { return new[] { 32, 128 }; }
        }

        public UInt32 Seed { get; set; }


        public MurmurHash3()
            : base(32)
        {
            Seed = 0;
        }


        public override byte[] ComputeHash(byte[] data)
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

        
        protected byte[] ComputeHash32(byte[] data)
        {
            const UInt32 c1 = 0xcc9e2d51;
            const UInt32 c2 = 0x1b873593;

            UInt32 h1 = Seed;
            

            //----------
            // body
            
            for (int x = 0; x < data.Length / 4; ++x)
            {
                UInt32 k1 = BitConverter.ToUInt32(data, x * 4);

                k1 *= c1;
                k1 = Rotl(k1, 15);
                k1 *= c2;
   
                h1 ^= k1;
                h1 = Rotl(h1, 13);
                h1 = (h1 * 5) + 0xe6546b64;
            }


            //----------
            // tail

            {

                UInt32 k1 = 0;
                var remainderStartIndex = data.Length - (data.Length % 4);

                switch(data.Length % 4)
                {
                    case 3: k1 ^= (UInt32) data[remainderStartIndex + 2] << 16;   goto case 2;
                    case 2: k1 ^= (UInt32) data[remainderStartIndex + 1] <<  8;   goto case 1;
                    case 1: 
                        k1 ^= (UInt32) data[remainderStartIndex];
                        k1 *= c1; 
                        k1 = Rotl(k1, 15); 
                        k1 *= c2; 
                        h1 ^= k1;
                        break;
                }
            }

            //----------
            // finalization

            h1 ^= (UInt32) data.Length;

            h1 = Mix(h1);

            return BitConverter.GetBytes(h1);
        }

        protected byte[] ComputeHash128(byte[] data)
        {
            const UInt64 c1 = 0x87c37b91114253d5;
            const UInt64 c2 = 0x4cf5ad432745937f;

            UInt64 h1 = (UInt64) Seed;
            UInt64 h2 = (UInt64) Seed;


            //----------
            // body
            
            for (int x = 0; x < data.Length / 16; ++x)
            {
            
                UInt64 k1 = BitConverter.ToUInt64(data, 0);
                UInt64 k2 = BitConverter.ToUInt64(data, 8);

                k1 *= c1; 
                k1  = Rotl(k1, 31); 
                k1 *= c2; 
                h1 ^= k1;

                h1  = Rotl(h1, 27); 
                h1 += h2; 
                h1  = (h1 * 5) + 0x52dce729;

                k2 *= c2; 
                k2  = Rotl(k2, 33); 
                k2 *= c1; 
                h2 ^= k2;

                h2  = Rotl(h2, 31); 
                h2 += h1; 
                h2  = (h2 * 5) + 0x38495ab5;
            }

            //----------
            // tail

            {
                UInt64 k1 = 0;
                UInt64 k2 = 0;

                var remainderStartIndex = data.Length - (data.Length % 16);

                switch(data.Length % 16)
                {
                    case 15: k2 ^= (UInt64) data[remainderStartIndex + 14] << 48;   goto case 14;
                    case 14: k2 ^= (UInt64) data[remainderStartIndex + 13] << 40;   goto case 13;
                    case 13: k2 ^= (UInt64) data[remainderStartIndex + 12] << 32;   goto case 12;
                    case 12: k2 ^= (UInt64) data[remainderStartIndex + 11] << 24;   goto case 11;
                    case 11: k2 ^= (UInt64) data[remainderStartIndex + 10] << 16;   goto case 10;
                    case 10: k2 ^= (UInt64) data[remainderStartIndex +  9] <<  8;   goto case 9;
                    case  9: 
                        k2 ^= ((UInt64) data[remainderStartIndex + 8]) <<  0;
                        k2 *= c2; 
                        k2  = Rotl(k2, 33); 
                        k2 *= c1; h2 ^= k2;

                        goto case 8;

                    case  8: 
                        k1 = BitConverter.ToUInt64(data, remainderStartIndex);
                        break;

                    case  7: k1 ^= (UInt64) data[remainderStartIndex + 6] << 48;    goto case 6;
                    case  6: k1 ^= (UInt64) data[remainderStartIndex + 5] << 40;    goto case 5;
                    case  5: k1 ^= (UInt64) data[remainderStartIndex + 4] << 32;    goto case 4;
                    case  4: k1 ^= (UInt64) data[remainderStartIndex + 3] << 24;    goto case 3;
                    case  3: k1 ^= (UInt64) data[remainderStartIndex + 2] << 16;    goto case 2;
                    case  2: k1 ^= (UInt64) data[remainderStartIndex + 1] <<  8;    goto case 1;
                    case  1: 
                        k1 ^= (UInt64) data[remainderStartIndex] << 0;
                        break;
                }

                if (data.Length % 16 != 0)
                {
                    k1 *= c1;
                    k1 = Rotl(k1, 31);
                    k1 *= c2;
                    h1 ^= k1;
                }
            }

            //----------
            // finalization

            h1 ^= (UInt64) data.Length; 
            h2 ^= (UInt64) data.Length;

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
        protected UInt32 Rotl(UInt32 value, int amount)
        {
            return (value << amount | value >> (32 - amount));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected UInt64 Rotl(UInt64 value, int amount)
        {
            return (value << amount | value >> (64 - amount));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected UInt32 Mix (UInt32 h)
        {
            h ^= h >> 16;
            h *= 0x85ebca6b;
            h ^= h >> 13;
            h *= 0xc2b2ae35;
            h ^= h >> 16;

            return h;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected UInt64 Mix(UInt64 k)
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
