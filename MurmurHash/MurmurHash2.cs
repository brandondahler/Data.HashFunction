using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    public class MurmurHash2
        : HashFunctionBase
    {
        public override IEnumerable<int> ValidHashSizes
        {
            get { return new[] { 32, 64 }; }
        }

        public UInt64 Seed { get; set; }


        protected const UInt64 MixConstant = 0xc6a4a7935bd1e995;

        public MurmurHash2()
            : base(64)
        {
            Seed = 0;
        }


        public override byte[] ComputeHash(byte[] data)
        {
            switch (HashSize)
            {
                case 32:
                    return ComputeHash32(data);

                case 64:
                    return ComputeHash64(data);

                default:
                    throw new ArgumentOutOfRangeException("HashSize");
            }

        }

        protected byte[] ComputeHash32(byte[] data)
        {
            const UInt32 m = unchecked((UInt32) MixConstant);

            // Initialize the hash to a 'random' value

            UInt32 h = (UInt32) Seed ^ (UInt32) data.Length;

            // Mix 4 bytes at a time into the hash

            for (int x = 0; x < data.Length / 4; ++x)
            {
                UInt32 k = BitConverter.ToUInt32(data, x * 4);

                k *= m;
                k ^= k >> 24;
                k *= m;

                h *= m;
                h ^= k;
            }
            

            // Handle the last few bytes of the input array

            var remainderStartIndex = data.Length - (data.Length % 4);

            switch(data.Length % 4)
            {
                case 3: h ^= (UInt32) data[remainderStartIndex + 2] << 16;  goto case 2;
                case 2: h ^= (UInt32) data[remainderStartIndex + 1] <<  8;  goto case 1;
                case 1:
                    h ^= data[remainderStartIndex];
                    h *= m;
                    break;
            };

            // Do a few final mixes of the hash to ensure the last few
            // bytes are well-incorporated.

            h ^= h >> 13;
            h *= m;
            h ^= h >> 15;

            return BitConverter.GetBytes(h);
        }

        protected byte[] ComputeHash64(byte[] data)
        {
            const UInt64 m = MixConstant;

            UInt64 h = Seed ^ ((UInt64) data.Length * m);


            for (int x = 0; x < data.Length / 8; ++x)
            {
                UInt64 k = BitConverter.ToUInt64(data, x * 8);

                k *= m;
                k ^= k >> 47;
                k *= m;
   
                h ^= k;
                h *= m;
            }

            var remainderStartIndex = data.Length - (data.Length % 8);

            switch(data.Length % 8)
            {
                case 7: h ^= (UInt64) data[remainderStartIndex + 6] << 48;  goto case 6;
                case 6: h ^= (UInt64) data[remainderStartIndex + 5] << 40;  goto case 5;
                case 5: h ^= (UInt64) data[remainderStartIndex + 4] << 32;  goto case 4;
                case 4: h ^= (UInt64) data[remainderStartIndex + 3] << 24;  goto case 3;
                case 3: h ^= (UInt64) data[remainderStartIndex + 2] << 16;  goto case 2;
                case 2: h ^= (UInt64) data[remainderStartIndex + 1] <<  8;  goto case 1;
                case 1: 
                    h ^= (UInt64) data[remainderStartIndex];
                    h *= m;
                    break;
            };
 
            h ^= h >> 47;
            h *= m;
            h ^= h >> 47;

            return BitConverter.GetBytes(h);
        }
    }
}
