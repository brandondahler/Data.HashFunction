using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core;
using OpenSource.Data.HashFunction.Core.Utilities;
using OpenSource.Data.HashFunction.Core.Utilities.UnifiedData;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSource.Data.HashFunction.CRC
{
    /// <summary>
    /// Implementation of the cyclic redundancy check error-detecting code as defined at http://en.wikipedia.org/wiki/Cyclic_redundancy_check.
    /// This implementation is generalized to encompass all possible CRC parameters from 1 to 64 bits.
    /// </summary>
    internal class CRC_Implementation
        : HashFunctionAsyncBase,
            ICRC
    {
        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        public ICRCConfig Config => _config.Clone();

        public override int HashSizeInBits => _config.HashSizeInBits;


        private readonly ICRCConfig _config;


        private static readonly ConcurrentDictionary<(int, UInt64, bool), IReadOnlyList<UInt64>> _dataDivisionTableCache =
            new ConcurrentDictionary<(int, ulong, bool), IReadOnlyList<ulong>>();


        /// <summary>
        /// Creates a new <see cref="CRC_Implementation" /> instance with given configuration.
        /// </summary>
        /// <param name="config">The configuration to use.</param>
        /// <exception cref="ArgumentNullException"><paramref name="config"/></exception>
        /// <exception cref="ArgumentException"><paramref name="config"/>.<see cref="ICRCConfig.HashSizeInBits"/> must be &gt;= <c>1</c> and &lt;= <c>64</c>;<paramref name="config"/>.<see cref="ICRCConfig.HashSizeInBits"/></exception>
        public CRC_Implementation(ICRCConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Clone();


            if (_config.HashSizeInBits <= 0 || _config.HashSizeInBits > 64)
                throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.HashSizeInBits)}", _config.HashSizeInBits, $"{nameof(config)}.{nameof(config.HashSizeInBits)} must be >= 1 and <= 64");
        }
        


        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(IUnifiedData data, CancellationToken cancellationToken)
        {
            // Use 64-bit variable regardless of CRC bit length
            UInt64 hash = _config.InitialValue;

            // Reflect InitialValue if processing as big endian
            if (_config.ReflectIn)
                hash = ReflectBits(hash, HashSizeInBits);


            // Store table reference in local variable to lower overhead.
            var crcTable = GetDataDivisionTable(_config.HashSizeInBits, _config.Polynomial, _config.ReflectIn);


            // How much hash must be right-shifted to get the most significant byte (HashSize >= 8) or bit (HashSize < 8)
            int mostSignificantShift = _config.HashSizeInBits - 8;

            if (_config.HashSizeInBits < 8)
                mostSignificantShift = _config.HashSizeInBits - 1;


            data.ForEachRead(
                (dataBytes, position, length) => {
                    ProcessBytes(ref hash, crcTable, mostSignificantShift, dataBytes, position, length);
                },
                cancellationToken);


            // Account for mixed-endianness
            if (_config.ReflectIn ^ _config.ReflectOut)
               hash = ReflectBits(hash, HashSizeInBits);


            hash ^= _config.XOrOut;

            return ToBytes(hash, HashSizeInBits);
        }
        
        /// <inheritdoc />
        protected override async Task<byte[]> ComputeHashAsyncInternal(IUnifiedDataAsync data, CancellationToken cancellationToken)
        {
            // Use 64-bit variable regardless of CRC bit length
            UInt64 hash = _config.InitialValue;

            // Reflect InitialValue if processing as big endian
            if (_config.ReflectIn)
                hash = ReflectBits(hash, HashSizeInBits);


            // Store table reference in local variable to lower overhead.
            var crcTable = GetDataDivisionTable(_config.HashSizeInBits, _config.Polynomial, _config.ReflectIn);


            // How much hash must be right-shifted to get the most significant byte (HashSize >= 8) or bit (HashSize < 8)
            int mostSignificantShift = _config.HashSizeInBits - 8;

            if (_config.HashSizeInBits < 8)
                mostSignificantShift = _config.HashSizeInBits - 1;


            await data.ForEachReadAsync(
                    (dataBytes, position, length) => {
                        ProcessBytes(ref hash, crcTable, mostSignificantShift, dataBytes, position, length);
                    },
                    cancellationToken)
                .ConfigureAwait(false);


            // Account for mixed-endianness
            if (_config.ReflectIn ^ _config.ReflectOut)
               hash = ReflectBits(hash, HashSizeInBits);


            hash ^= _config.XOrOut;

            return ToBytes(hash, HashSizeInBits);
        }

        private void ProcessBytes(ref UInt64 hash, IReadOnlyList<UInt64> crcTable, int mostSignificantShift, byte[] dataBytes, int position, int length)
        {
            for (var x = position; x < position + length; ++x)
            {
                if (HashSizeInBits >= 8)
                {
                    // Process per byte, treating hash differently based on input endianness
                    if (_config.ReflectIn)
                        hash = (hash >> 8) ^ crcTable[(byte) hash ^ dataBytes[x]];
                    else
                        hash = (hash << 8) ^ crcTable[((byte) (hash >> mostSignificantShift)) ^ dataBytes[x]];

                } else {
                    // Process per bit, treating hash differently based on input endianness
                    for (int y = 0; y < 8; ++y)
                    {
                        if (_config.ReflectIn)
                            hash = (hash >> 1) ^ crcTable[(byte) (hash & 1) ^ ((byte) (dataBytes[x] >> y) & 1)];
                        else
                            hash =  (hash << 1) ^ crcTable[(byte) ((hash >> mostSignificantShift) & 1) ^ ((byte) (dataBytes[x] >> (7 - y)) & 1)];
                    }

                }
            }
        }

        /// <summary>
        /// Calculates the data-division table for the CRC parameters provided.
        /// </summary>
        /// <param name="hashSizeInBits">Length of the produced CRC value, in bits.</param>
        /// <param name="polynomial">Divisor to use when calculating the CRC.</param>
        /// <param name="reflectIn">If true, the CRC calculation processes input as big endian bit order.</param>
        /// <returns>
        /// Array of UInt64 values that allows a CRC implementation to look up the result
        /// of dividing the index (data) by the polynomial.
        /// </returns>
        /// <remarks>
        /// Resulting array contains 256 items if settings.Bits &gt;= 8, or 2 items if settings.Bits &lt; 8.
        /// The table accounts for reflecting the index bits to fix the input endianness,
        /// but it is not possible completely account for the output endianness if the CRC is mixed-endianness.
        /// </remarks>
        private static IReadOnlyList<UInt64> GetDataDivisionTable(int hashSizeInBits, UInt64 polynomial, bool reflectIn)
        {
            return _dataDivisionTableCache.GetOrAdd(
                (hashSizeInBits, polynomial, reflectIn), 
                GetDataDivisionTableInternal);
        }

        private static IReadOnlyList<UInt64> GetDataDivisionTableInternal((int, UInt64, bool) cacheKey)
        {
            var hashSizeInBits = cacheKey.Item1;
            var polynomial = cacheKey.Item2;
            var reflectIn = cacheKey.Item3;


            var perBitCount = 8;

            if (hashSizeInBits < 8)
                perBitCount = 1;


            var crcTable = new UInt64[1 << perBitCount];
            var mostSignificantBit = 1UL << (hashSizeInBits - 1);


            for (uint x = 0; x < crcTable.Length; ++x)
            {
                UInt64 curValue = x;

                if (perBitCount > 1 && reflectIn)
                    curValue = ReflectBits(curValue, perBitCount);


                curValue <<= (hashSizeInBits - perBitCount);


                for (int y = 0; y < perBitCount; ++y)
                {
                    if ((curValue & mostSignificantBit) > 0UL)
                        curValue = (curValue << 1) ^ polynomial;
                    else
                        curValue <<= 1;
                }


                if (reflectIn)
                    curValue = ReflectBits(curValue, hashSizeInBits);


                curValue &= (UInt64.MaxValue >> (64 - hashSizeInBits));

                crcTable[x] = curValue;
            }


            return crcTable;
        }


        private static byte[] ToBytes(UInt64 value, int bitLength)
        {
            value &= (UInt64.MaxValue >> (64 - bitLength));


            var valueBytes = new byte[(bitLength + 7) / 8];

            for (int x = 0; x < valueBytes.Length; ++x)
            {
                valueBytes[x] = (byte)value;
                value >>= 8;
            }

            return valueBytes;
        }

        private static UInt64 ReflectBits(UInt64 value, int bitLength)
        {
            UInt64 reflectedValue = 0UL;

            for (int x = 0; x < bitLength; ++x)
            {
                reflectedValue <<= 1;

                reflectedValue |= (value & 1);

                value >>= 1;
            }

            return reflectedValue;
        }

    }
}
