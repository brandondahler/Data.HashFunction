using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Implements xxHash as specified at https://code.google.com/p/xxhash/source/browse/trunk/xxhash.c and 
    ///   https://code.google.com/p/xxhash/.
    /// </summary>
    public class xxHash
        : HashFunctionBase
    {
        /// <inheritdoc/>
        public override IEnumerable<int> ValidHashSizes { get { return new[] { 32 }; } }

        /// <summary>
        /// Seed value for hash calculation.
        /// </summary>
        public UInt32 InitVal { get; set; }

        private static readonly IReadOnlyList<UInt32> Primes = new[] {
            2654435761U,
            2246822519U,
            3266489917U,
             668265263U,
             374761393U
        };


        /// <summary>
        /// Constructs new <see cref="xxHash"/> instance.
        /// </summary>
        public xxHash()
            : base(32)
        {
            InitVal = 0;
        }


        /// <inheritdoc/>
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

                h = values[0].RotateLeft(1) + values[1].RotateLeft(7) + values[2].RotateLeft(12) + values[3].RotateLeft(18);
            }

            h += (UInt32) data.Length;

            // Process last 16 bytes in 4-byte chunks (only runs if data.Length % 16 != 0)
            {
                var startIndex = data.Length - (data.Length % 16);
                var stopIndex = data.Length - (data.Length % 4);

                for (int x = startIndex; x < stopIndex; x += 4)
                {
                    h += BitConverter.ToUInt32(data, x) * Primes[2];
                    h = h.RotateLeft(17) * Primes[3];
                }
            }

            // Process last 4 bytes in 1-byte chunks (only runs if data.Length % 4 != 0)
            {
                var startIndex = data.Length - (data.Length % 4);

                for (int x = startIndex; x < data.Length; ++x)
                {
                    h += (UInt32) data[x] * Primes[4];
                    h = h.RotateLeft(11) * Primes[0];
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
            curValue = curValue.RotateLeft(13);
            curValue *= Primes[0];
        }
    }
}
