using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    public class xxHash
        : HashFunctionBase
    {
        public override IEnumerable<int> ValidHashSizes { get { return new[] { 32 }; } }

        public UInt32 InitVal { get; set; }

        protected static readonly UInt32[] Primes = new[] {
            2654435761U,
            2246822519U,
            3266489917U,
             668265263U,
             374761393U
        };

        public xxHash()
            : base(32)
        {
            InitVal = 0;
        }


        public override byte[] ComputeHash(byte[] data)
        {
            if (HashSize != 32)
                throw new ArgumentOutOfRangeException("HashSize");

            var h = InitVal + Primes[4];

            if (data.Length > 16)
            {
                var values = new[] {
                    InitVal + Primes[0] + Primes[1],
                    InitVal + Primes[1],
                    InitVal,
                    InitVal - Primes[0]
                };

                for (int x = 0; x < data.Length / 16; ++x)
                {
                    Process16(BitConverter.ToUInt32(data, x * 16 +  0), ref values[0]);
                    Process16(BitConverter.ToUInt32(data, x * 16 +  4), ref values[1]);
                    Process16(BitConverter.ToUInt32(data, x * 16 +  8), ref values[2]);
                    Process16(BitConverter.ToUInt32(data, x * 16 + 12), ref values[3]);
                }

                h = Rotl(values[0], 1) + Rotl(values[1], 7) + Rotl(values[2], 12) + Rotl(values[3], 18);
            }

            h += (UInt32) data.Length;

            // Process last 16 bytes in 4-byte chunks (only runs if data.Length % 16 != 0)
            {
                var startIndex = data.Length - (data.Length % 16);
                var stopIndex = data.Length - (data.Length % 4);

                for (int x = startIndex; x < stopIndex; x += 4)
                {
                    h += BitConverter.ToUInt32(data, x) * Primes[2];
                    h = Rotl(h, 17) * Primes[3];
                }
            }

            // Process last 4 bytes in 1-byte chunks (only runs if data.Length % 4 != 0)
            {
                var startIndex = data.Length - (data.Length % 4);

                for (int x = startIndex; x < data.Length; ++x)
                {
                    h += (UInt32) data[x] * Primes[4];
                    h = Rotl(h, 11) * Primes[0];
                }
            }

            h ^= h >> 15;
            h *= Primes[1];
            h ^= h >> 13;
            h *= Primes[2];
            h ^= h >> 16;

            return BitConverter.GetBytes(h);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Process16(UInt32 data, ref UInt32 curValue)
        {
            curValue += data * Primes[1];
            curValue = Rotl(curValue, 13);
            curValue *= Primes[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private UInt32 Rotl(UInt32 value, int amount)
        {
            return (value << amount) | (value >> (32 - amount));
        }

    }
}
