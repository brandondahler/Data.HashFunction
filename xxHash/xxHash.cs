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
    /// Implements xxHash as specified at https://code.google.com/p/xxhash/source/browse/trunk/xxhash.c and 
    ///   https://code.google.com/p/xxhash/.
    /// </summary>
    public class xxHash
        : HashFunctionBase
    {
        /// <summary>
        /// Seed value for hash calculation.
        /// </summary>
        /// <value>
        /// The seed value for hash calculation.
        /// </value>
        public UInt32 InitVal { get; set; }


        private static readonly IReadOnlyList<UInt32> Primes = new[] {
            2654435761U,
            2246822519U,
            3266489917U,
             668265263U,
             374761393U
        };


        /// <summary>
        /// Initializes a new instance of the <see cref="xxHash"/> class.
        /// </summary>
        /// <inheritdoc cref="HashFunctionBase(int)" />
        public xxHash()
            : base(32)
        {
            InitVal = 0;
        }


        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(Stream data)
        {
            if (HashSize != 32)
                throw new InvalidOperationException("HashSize set to an invalid value.");


            var h = InitVal + Primes[4];

            var dataGroups = data.AsGroupedStreamData(16);
            int dataCount = 0;


            {
                bool fullBlockRead = false;

                var initValues = new[] {
                    InitVal + Primes[0] + Primes[1],
                    InitVal + Primes[1],
                    InitVal,
                    InitVal - Primes[0]
                };


                foreach (var dataGroup in dataGroups)
                {
                    Process16(BitConverter.ToUInt32(dataGroup, 0), ref initValues[0]);
                    Process16(BitConverter.ToUInt32(dataGroup, 4), ref initValues[1]);
                    Process16(BitConverter.ToUInt32(dataGroup, 8), ref initValues[2]);
                    Process16(BitConverter.ToUInt32(dataGroup, 12), ref initValues[3]);

                    dataCount += dataGroup.Length;
                    fullBlockRead = true;
                }


                if (fullBlockRead)
                {
                    h = initValues[0].RotateLeft(1) + 
                        initValues[1].RotateLeft(7) + 
                        initValues[2].RotateLeft(12) + 
                        initValues[3].RotateLeft(18);
                }
            }


            var remainder = dataGroups.Remainder;

            dataCount += remainder.Length;
            h += (UInt32) dataCount;


           // In 4-byte chunks, process all but last byte
            for (int x = 0; x < remainder.Length / 4; ++x)
            {
                h += BitConverter.ToUInt32(remainder, x * 4) * Primes[2];
                h = h.RotateLeft(17) * Primes[3];
            }


            // Process last 4 bytes in 1-byte chunks (only runs if data.Length % 4 != 0)
            for (int x = remainder.Length - (remainder.Length % 4); x < remainder.Length; ++x)
            {
                h += (UInt32) remainder[x] * Primes[4];
                h = h.RotateLeft(11) * Primes[0];
            }


            h ^= h >> 15;
            h *= Primes[1];
            h ^= h >> 13;
            h *= Primes[2];
            h ^= h >> 16;

            return BitConverter.GetBytes(h);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Process16(UInt32 data, ref UInt32 curValue)
        {
            curValue += data * Primes[1];
            curValue = curValue.RotateLeft(13);
            curValue *= Primes[0];
        }
    }
}
