using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core;
using OpenSource.Data.HashFunction.Core.Utilities;
using OpenSource.Data.HashFunction.FNV.Utilities;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading;

namespace OpenSource.Data.HashFunction.FNV
{
    internal abstract class FNV1Base
        : StreamableHashFunctionBase,
            IFNV
    {

        public IFNVConfig Config => _config.Clone();

        public override int HashSizeInBits => _config.HashSizeInBits;



        protected readonly IFNVConfig _config;
        protected readonly FNVPrimeOffset _fnvPrimeOffset;


        protected FNV1Base(IFNVConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));


            _config = config.Clone();
            

            if (_config.HashSizeInBits <= 0 || _config.HashSizeInBits % 32 != 0)
                throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.HashSizeInBits)}", _config.HashSizeInBits, $"{nameof(config)}.{nameof(config.HashSizeInBits)} must be a positive a multiple of 32.");

            if (_config.Prime <= BigInteger.Zero)
                throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.Prime)}", _config.Prime, $"{nameof(config)}.{nameof(config.Prime)} must be greater than zero.");

            if (_config.Offset <= BigInteger.Zero)
                throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.Offset)}", _config.Offset, $"{nameof(config)}.{nameof(config.Offset)} must be greater than zero.");


            _fnvPrimeOffset = FNVPrimeOffset.Create(_config.HashSizeInBits, _config.Prime, _config.Offset);
        }


        protected abstract class BlockTransformer_32BitBase<TSelf>
            : BlockTransformerBase<TSelf>
            where TSelf : BlockTransformer_32BitBase<TSelf>, new()
        {
            protected UInt32 _prime;

            protected UInt32 _hashValue;

            public BlockTransformer_32BitBase()
            {

            }

            public BlockTransformer_32BitBase(FNVPrimeOffset fnvPrimeOffset)
                : this()
            {
                _prime = fnvPrimeOffset.Prime[0];

                _hashValue = fnvPrimeOffset.Offset[0];
            }


            protected override void CopyStateTo(TSelf other)
            {
                base.CopyStateTo(other);

                other._prime = _prime;

                other._hashValue = _hashValue;
            }

            protected override IHashValue FinalizeHashValueInternal(CancellationToken cancellationToken) =>
                new HashValue(BitConverter.GetBytes(_hashValue), 32);
        }
        
        protected abstract class BlockTransformer_64BitBase<TSelf>
            : BlockTransformerBase<TSelf>
            where TSelf : BlockTransformer_64BitBase<TSelf>, new()
        {
            protected UInt64 _prime;

            protected UInt64 _hashValue;

            public BlockTransformer_64BitBase()
            {

            }

            public BlockTransformer_64BitBase(FNVPrimeOffset fnvPrimeOffset)
                : this()
            {
                _prime = ((UInt64) fnvPrimeOffset.Prime[1] << 32) | fnvPrimeOffset.Prime[0];

                _hashValue = ((UInt64) fnvPrimeOffset.Offset[1] << 32) | fnvPrimeOffset.Offset[0];
            }


            protected override void CopyStateTo(TSelf other)
            {
                base.CopyStateTo(other);

                other._prime = _prime;

                other._hashValue = _hashValue;
            }

            protected override IHashValue FinalizeHashValueInternal(CancellationToken cancellationToken) =>
                new HashValue(BitConverter.GetBytes(_hashValue), 64);
        }
        
        protected abstract class BlockTransformer_ExtendedBase<TSelf>
            : BlockTransformerBase<TSelf>
            where TSelf : BlockTransformer_ExtendedBase<TSelf>, new()
        {
            protected UInt32[] _prime;

            protected UInt32[] _hashValue;
            protected int _hashSizeInBytes;

            public BlockTransformer_ExtendedBase()
            {

            }

            public BlockTransformer_ExtendedBase(FNVPrimeOffset fnvPrimeOffset)
                : this()
            {
                _prime = fnvPrimeOffset.Prime.ToArray();

                _hashValue = fnvPrimeOffset.Offset.ToArray();
                _hashSizeInBytes = _hashValue.Length * 4;
            }


            protected override void CopyStateTo(TSelf other)
            {
                base.CopyStateTo(other);

                other._prime = _prime;

                other._hashValue = _hashValue;
                other._hashSizeInBytes = _hashSizeInBytes;
            }

            protected override IHashValue FinalizeHashValueInternal(CancellationToken cancellationToken) =>
                new HashValue(UInt32ArrayToBytes(_hashValue), _hashSizeInBytes * 8);
        }


        /// <summary>
        /// Multiplies operand1 by operand2 as if both operand1 and operand2 were single large integers.
        /// </summary>
        /// <param name="operand1">Array of UInt32 values to be multiplied.</param>
        /// <param name="operand2">Array of UInt32 values to multiply by.</param>
        /// <param name="hashSizeInBytes">Hash size, in bytes, to truncate products at.</param>
        /// <returns></returns>
        protected static UInt32[] ExtendedMultiply(IReadOnlyList<UInt32> operand1, IReadOnlyList<UInt32> operand2, int hashSizeInBytes)
        {
            Debug.Assert(hashSizeInBytes % 4 == 0);

            // Temporary array to hold the results of 32-bit multiplication.
            var product = new UInt32[hashSizeInBytes / 4];

            // Bottom of equation
            for (int y = 0; y < operand2.Count; ++y)
            {
                // Skip multiplying things by zero
                if (operand2[y] == 0)
                    continue;

                UInt32 carryOver = 0;

                // Top of equation
                for (int x = 0; x < operand1.Count; ++x)
                {
                    if (x + y >= product.Length)
                        break;

                    var productResult = product[x + y] + (((UInt64)operand2[y]) * operand1[x]) + carryOver;
                    product[x + y] = (UInt32)productResult;

                    carryOver = (UInt32)(productResult >> 32);
                }
            }

            return product;
        }
        
        private static IEnumerable<byte> UInt32ArrayToBytes(IEnumerable<UInt32> values)
        {
            return values.SelectMany(v => BitConverter.GetBytes(v));
        }
    }
}
