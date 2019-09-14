using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core;
using OpenSource.Data.HashFunction.Core.Utilities;
using OpenSource.Data.HashFunction.Core.Utilities.UnifiedData;
using OpenSource.Data.HashFunction.FNV.Utilities;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSource.Data.HashFunction.FNV
{
    /// <summary>
    /// Abstract implementation of Fowler–Noll–Vo hash function (FNV-1 and FNV-1a) as specified at http://www.isthe.com/chongo/tech/comp/fnv/index.html.
    /// </summary>
    internal abstract class FNV1Base
        : HashFunctionAsyncBase,
            IFNV
    {

        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        public IFNVConfig Config => _config.Clone();

        public override int HashSizeInBits => _config.HashSizeInBits;



        private readonly IFNVConfig _config;

        private readonly FNVPrimeOffset _fnvPrimeOffset;


        /// <summary>
        /// Initializes a new instance of the <see cref="FNV1Base"/> class.
        /// </summary>
        /// <exception cref="System.ArgumentNullException"><paramref name="config"/></exception>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="config"/>.<see cref="IFNVConfig.HashSizeInBits"/>;<paramref name="config"/>.<see cref="IFNVConfig.HashSizeInBits"/> must be a positive a multiple of 32.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="config"/>.<see cref="IFNVConfig.Prime"/>;<paramref name="config"/>.<see cref="IFNVConfig.Prime"/> must be non-zero.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="config"/>.<see cref="IFNVConfig.Offset"/>;<paramref name="config"/>.<see cref="IFNVConfig.Offset"/> must be non-zero.</exception>
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


        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(IUnifiedData data, CancellationToken cancellationToken)
        {
            var prime = _fnvPrimeOffset.Prime;
            var offset = _fnvPrimeOffset.Offset;

            // Handle 32-bit and 64-bit cases in a strongly-typed manner for performance
            if (_config.HashSizeInBits == 32)
            {
                var hash = offset[0];

                data.ForEachRead(
                    (dataBytes, position, length) => {
                        ProcessBytes32(ref hash, prime[0], dataBytes, position, length);
                    },
                    cancellationToken);

                return BitConverter.GetBytes(hash);

            } else if (_config.HashSizeInBits == 64) {
                var hash = ((UInt64) offset[1] << 32) | offset[0];
                var prime64 = ((UInt64) prime[1] << 32) | prime[0];


                data.ForEachRead(
                    (dataBytes, position, length) => {
                        ProcessBytes64(ref hash, prime64, dataBytes, position, length);
                    },
                    cancellationToken);

                return BitConverter.GetBytes(hash);
            }


            // Process extended-sized FNV.
            {
                var hash = offset.ToArray();


                data.ForEachRead(
                    (dataBytes, position, length) => {
                        ProcessBytes(ref hash, prime, dataBytes, position, length);
                    },
                    cancellationToken);

                return UInt32ArrayToBytes(hash)
                    .ToArray();
            }
        }
        
        /// <inheritdoc />
        protected override async Task<byte[]> ComputeHashAsyncInternal(IUnifiedDataAsync data, CancellationToken cancellationToken)
        {
            var prime = _fnvPrimeOffset.Prime;
            var offset = _fnvPrimeOffset.Offset;

            // Handle 32-bit and 64-bit cases in a strongly-typed manner for performance
            if (HashSizeInBits == 32)
            {
                var hash = offset[0];

                await data.ForEachReadAsync(
                        (dataBytes, position, length) => {
                            ProcessBytes32(ref hash, prime[0], dataBytes, position, length);
                        },
                        cancellationToken)
                    .ConfigureAwait(false);

                return BitConverter.GetBytes(hash);

            } else if (HashSizeInBits == 64) {
                var hash = ((UInt64) offset[1] << 32) | offset[0];
                var prime64 = ((UInt64) prime[1] << 32) | prime[0];


                await data.ForEachReadAsync(
                        (dataBytes, position, length) => {
                            ProcessBytes64(ref hash, prime64, dataBytes, position, length);
                        },
                        cancellationToken)
                    .ConfigureAwait(false);

                return BitConverter.GetBytes(hash);
            }


            // Process extended-sized FNV.
            {
                var hash = offset.ToArray();


                await data.ForEachReadAsync(
                        (dataBytes, position, length) => {
                            ProcessBytes(ref hash, prime, dataBytes, position, length);
                        },
                        cancellationToken)
                    .ConfigureAwait(false);

                return UInt32ArrayToBytes(hash)
                    .ToArray();
            }
        }



        /// <summary>
        /// Apply 32-bit FNV algorithm on all data supplied.
        /// </summary>
        /// <param name="hash">Hash value before calculations.</param>
        /// <param name="prime">FNV prime to use for calculations.</param>
        /// <param name="data">Data to process.</param>
        /// <param name="position">The starting index of the data array.</param>
        /// <param name="length">The length of the data in the data array, starting from the position parameter.</param>
        protected abstract void ProcessBytes32(ref UInt32 hash, UInt32 prime, byte[] data, int position, int length);

        /// <summary>
        /// Apply 64-bit FNV algorithm on all data supplied.
        /// </summary>
        /// <param name="hash">Hash value before calculations.</param>
        /// <param name="prime">FNV prime to use for calculations.</param>
        /// <param name="data">Data to process.</param>
        /// <param name="position">The starting index of the data array.</param>
        /// <param name="length">The length of the data in the data array, starting from the position parameter.</param>
        protected abstract void ProcessBytes64(ref UInt64 hash, UInt64 prime, byte[] data, int position, int length);

        /// <summary>
        /// Apply FNV algorithm on all data supplied.
        /// </summary>
        /// <param name="hash">Hash value before calculations.</param>
        /// <param name="prime">FNV prime to use for calculations.</param>
        /// <param name="data">Data to process.</param>
        /// <param name="position">The starting index of the data array.</param>
        /// <param name="length">The length of the data in the data array, starting from the position parameter.</param>
        protected abstract void ProcessBytes(ref UInt32[] hash, IReadOnlyList<UInt32> prime, byte[] data, int position, int length);


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
