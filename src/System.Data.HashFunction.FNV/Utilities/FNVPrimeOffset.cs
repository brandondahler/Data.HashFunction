using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.FNV.Utilities
{
    /// <summary>
    /// Class for storing FNV prime and offset combinations.
    /// 
    /// Values specified as System.Numerics.BigInteger, converted to collections of UInt32 values.
    /// </summary>
    internal sealed class FNVPrimeOffset
    {
        /// <summary>
        /// FNV prime number as an <see cref="IReadOnlyList{UInt32}" />.
        /// </summary>
        /// <value>
        /// The prime number as an <see cref="IReadOnlyList{UInt32}" />.
        /// </value>
        public IReadOnlyList<UInt32> Prime { get; }

        /// <summary>
        /// FNV offset as an <see cref="IReadOnlyList{UInt32}" />.
        /// </summary>
        /// <value>
        /// The offset as an <see cref="IReadOnlyList{UInt32}" />.
        /// </value>
        public IReadOnlyList<UInt32> Offset { get; }



        private static readonly ConcurrentDictionary<(BigInteger, int), IReadOnlyList<UInt32>> _calculatedUintArrays = 
            new ConcurrentDictionary<(BigInteger, int), IReadOnlyList<uint>>();


        /// <summary>
        /// Initializes a new instance of <see cref="FNVPrimeOffset"/>.
        /// </summary>
        /// <param name="prime">Prime value to be represented.</param>
        /// <param name="offset">Offset value to be represented.</param>
        private FNVPrimeOffset(IReadOnlyList<UInt32> prime, IReadOnlyList<UInt32> offset)
        {
            Prime = prime ?? throw new ArgumentNullException(nameof(prime));
            Offset = offset ?? throw new ArgumentNullException(nameof(offset));
        }


        /// <summary>
        /// Creates a new instance of <see cref="FNVPrimeOffset"/>.
        /// </summary>
        /// <param name="bitSize">Number of bits the prime and offset use each.</param>
        /// <param name="prime">Prime integer to be represented.</param>
        /// <param name="offset">Offset integer to be represented.</param>
        public static FNVPrimeOffset Create(int bitSize, BigInteger prime, BigInteger offset)
        { 
            if (bitSize < 0 || bitSize % 32 != 0)
                throw new ArgumentOutOfRangeException(nameof(bitSize), $"{nameof(bitSize)} must be a positive a multiple of 32.");

            if (prime == BigInteger.Zero)
                throw new ArgumentException($"{nameof(prime)} must be non-zero.", nameof(prime));

            if (offset == BigInteger.Zero)
                throw new ArgumentException($"{nameof(offset)} must be non-zero.", nameof(offset));


            return new FNVPrimeOffset(
                _calculatedUintArrays.GetOrAdd((prime, bitSize), ToUInt32Array),
                _calculatedUintArrays.GetOrAdd((offset, bitSize), ToUInt32Array));
        }



        private static IReadOnlyList<UInt32> ToUInt32Array((BigInteger, int) tuple) =>
            ToUInt32Array(tuple.Item1, tuple.Item2);

        private static IReadOnlyList<UInt32> ToUInt32Array(BigInteger value, int bitSize)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (bitSize < 0 || bitSize % 32 != 0)
                throw new ArgumentOutOfRangeException(nameof(bitSize), $"{nameof(bitSize)} must be a positive a multiple of 32.");


            var uint32Values = new UInt32[bitSize / 32];
            var bigIntegerBytes = value.ToByteArray();


            var copyLength = uint32Values.Length * 4;

            if (bigIntegerBytes.Length < copyLength)
                copyLength = bigIntegerBytes.Length;


            Buffer.BlockCopy(
                bigIntegerBytes, 0,
                uint32Values, 0,
                copyLength);

            return uint32Values;
        }

    }
}
