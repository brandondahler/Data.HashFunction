using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core;
using OpenSource.Data.HashFunction.Core.Utilities;
using System.Linq;
using System.Threading;

namespace OpenSource.Data.HashFunction.xxHash
{
    internal class xxHash_Implementation
        : StreamableHashFunctionBase,
            IxxHash
    {

        public IxxHashConfig Config => _config.Clone();

        public override int HashSizeInBits => _config.HashSizeInBits;
        


        private readonly IxxHashConfig _config;



        private static readonly IEnumerable<int> _validHashSizes = new HashSet<int>() { 32, 64 };


        public xxHash_Implementation(IxxHashConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Clone();


            if (!_validHashSizes.Contains(_config.HashSizeInBits))
                throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.HashSizeInBits)}", _config.HashSizeInBits, $"{nameof(config)}.{nameof(config.HashSizeInBits)} must be contained within xxHash.ValidHashSizes");
        }


        public override IBlockTransformer CreateBlockTransformer()
        {
            switch (_config.HashSizeInBits)
            {
                case 32:
                    return new BlockTransformer32((UInt32) _config.Seed);

                case 64:
                    return new BlockTransformer64(_config.Seed);

                default:
                    throw new NotImplementedException();
            }
        }

        private class BlockTransformer32
            : BlockTransformerBase<BlockTransformer32>
        {
            private static readonly IReadOnlyList<UInt32> _primes32 = 
                new[] {
                    2654435761U,
                    2246822519U,
                    3266489917U,
                     668265263U,
                     374761393U
                };

            private UInt32 _seed;

            private UInt32 _a;
            private UInt32 _b;
            private UInt32 _c;
            private UInt32 _d;

            private ulong _bytesProcessed = 0;


            public BlockTransformer32() 
                : base(inputBlockSize: 16)
            {

            }

            public BlockTransformer32(UInt32 seed)
                : this()
            {
                _seed = seed;

                _a = _seed + _primes32[0] + _primes32[1];
                _b = _seed + _primes32[1];
                _c = _seed;
                _d = _seed - _primes32[0];
            }

            protected override void CopyStateTo(BlockTransformer32 other)
            {
                base.CopyStateTo(other);

                other._seed = _seed;

                other._a = _a;
                other._b = _b;
                other._c = _c;
                other._d = _d;

                other._bytesProcessed = _bytesProcessed;
            }

            protected override void TransformByteGroupsInternal(ArraySegment<byte> data)
            {
                var dataArray = data.Array;
                var dataOffset = data.Offset;
                var dataCount = data.Count;

                var endOffset = dataOffset + dataCount;

                var tempA = _a;
                var tempB = _b;
                var tempC = _c;
                var tempD = _d;

                var tempPrime0 = _primes32[0];
                var tempPrime1 = _primes32[1];

                for (var currentIndex = dataOffset; currentIndex < endOffset; currentIndex += 16)
                {
                    tempA += BitConverter.ToUInt32(dataArray, currentIndex) * tempPrime1;
                    tempA = RotateLeft(tempA, 13);
                    tempA *= tempPrime0;

                    tempB += BitConverter.ToUInt32(dataArray, currentIndex + 4) * tempPrime1;
                    tempB = RotateLeft(tempB, 13);
                    tempB *= tempPrime0;

                    tempC += BitConverter.ToUInt32(dataArray, currentIndex + 8) * tempPrime1;
                    tempC = RotateLeft(tempC, 13);
                    tempC *= tempPrime0;

                    tempD += BitConverter.ToUInt32(dataArray, currentIndex + 12) * tempPrime1;
                    tempD = RotateLeft(tempD, 13);
                    tempD *= tempPrime0;
                }

                _a = tempA;
                _b = tempB;
                _c = tempC;
                _d = tempD;

                _bytesProcessed += (ulong) dataCount;
            }

            protected override IHashValue FinalizeHashValueInternal(CancellationToken cancellationToken)
            {
                UInt32 hashValue;
                {
                    if (_bytesProcessed > 0)
                        hashValue = RotateLeft(_a, 1) + RotateLeft(_b, 7) + RotateLeft(_c, 12) + RotateLeft(_d, 18);
                    else 
                        hashValue = _seed + _primes32[4];
                }

                var remainder = FinalizeInputBuffer;
                var remainderLength = (remainder?.Length).GetValueOrDefault();

                hashValue += (UInt32) (_bytesProcessed + (ulong) remainderLength);

                if (remainderLength > 0)
                {
                    // In 4-byte chunks, process all process all full chunks
                    {
                        var endOffset = remainderLength - (remainderLength % 4);

                        for (var currentOffset = 0; currentOffset < endOffset; currentOffset += 4)
                        {
                            hashValue += BitConverter.ToUInt32(remainder, currentOffset) * _primes32[2];
                            hashValue = RotateLeft(hashValue, 17) * _primes32[3];
                        }
                    }

                    // Process last 4 bytes in 1-byte chunks (only runs if data.Length % 4 != 0)
                    {
                        var startOffset = remainderLength - (remainderLength % 4);
                        var endOffset = remainderLength;

                        for (var currentOffset = startOffset; currentOffset < endOffset; currentOffset += 1)
                        {
                            hashValue += (UInt32) remainder[currentOffset] * _primes32[4];
                            hashValue = RotateLeft(hashValue, 11) * _primes32[0];
                        }
                    }
                }

                hashValue ^= hashValue >> 15;
                hashValue *= _primes32[1];
                hashValue ^= hashValue >> 13;
                hashValue *= _primes32[2];
                hashValue ^= hashValue >> 16;

                return new HashValue(
                    BitConverter.GetBytes(hashValue),
                    32);
            }


            private static UInt32 RotateLeft(UInt32 operand, int shiftCount)
            {
                shiftCount &= 0x1f;

                return
                    (operand << shiftCount) |
                    (operand >> (32 - shiftCount));
            }
        }


        private class BlockTransformer64
            : BlockTransformerBase<BlockTransformer64>
        {
            private static readonly IReadOnlyList<UInt64> _primes64 =
                new[] {
                    11400714785074694791UL,
                    14029467366897019727UL,
                     1609587929392839161UL,
                     9650029242287828579UL,
                     2870177450012600261UL
                };


            private UInt64 _seed;

            private UInt64 _a;
            private UInt64 _b;
            private UInt64 _c;
            private UInt64 _d;

            private ulong _bytesProcessed = 0;


            public BlockTransformer64()
                : base(inputBlockSize: 32)
            {

            }

            public BlockTransformer64(UInt64 seed)
                : this()
            {
                _seed = seed;

                _a = _seed + _primes64[0] + _primes64[1];
                _b = _seed + _primes64[1];
                _c = _seed;
                _d = _seed - _primes64[0];
            }

            protected override void CopyStateTo(BlockTransformer64 other)
            {
                base.CopyStateTo(other);

                other._seed = _seed;

                other._a = _a;
                other._b = _b;
                other._c = _c;
                other._d = _d;

                other._bytesProcessed = _bytesProcessed;
            }

            protected override void TransformByteGroupsInternal(ArraySegment<byte> data)
            {
                var dataArray = data.Array;
                var dataOffset = data.Offset;
                var dataCount = data.Count;

                var endOffset = dataOffset + dataCount;

                var tempA = _a;
                var tempB = _b;
                var tempC = _c;
                var tempD = _d;

                var tempPrime0 = _primes64[0];
                var tempPrime1 = _primes64[1];

                for (var currentIndex = dataOffset; currentIndex < endOffset; currentIndex += 32)
                {
                    tempA += BitConverter.ToUInt64(dataArray, currentIndex) * tempPrime1;
                    tempA = RotateLeft(tempA, 31);
                    tempA *= tempPrime0;

                    tempB += BitConverter.ToUInt64(dataArray, currentIndex + 8) * tempPrime1;
                    tempB = RotateLeft(tempB, 31);
                    tempB *= tempPrime0;

                    tempC += BitConverter.ToUInt64(dataArray, currentIndex + 16) * tempPrime1;
                    tempC = RotateLeft(tempC, 31);
                    tempC *= tempPrime0;

                    tempD += BitConverter.ToUInt64(dataArray, currentIndex + 24) * tempPrime1;
                    tempD = RotateLeft(tempD, 31);
                    tempD *= tempPrime0;
                }

                _a = tempA;
                _b = tempB;
                _c = tempC;
                _d = tempD;

                _bytesProcessed += (ulong)dataCount;
            }

            protected override IHashValue FinalizeHashValueInternal(CancellationToken cancellationToken)
            {
                UInt64 hashValue;
                {
                    if (_bytesProcessed > 0)
                    {
                        var tempA = _a;
                        var tempB = _b;
                        var tempC = _c;
                        var tempD = _d;


                        hashValue = RotateLeft(_a, 1) + RotateLeft(_b, 7) + RotateLeft(_c, 12) + RotateLeft(_d, 18);

                        // A
                        tempA *= _primes64[1];
                        tempA = RotateLeft(tempA, 31);
                        tempA *= _primes64[0];

                        hashValue ^= tempA;
                        hashValue = (hashValue * _primes64[0]) + _primes64[3];

                        // B
                        tempB *= _primes64[1];
                        tempB = RotateLeft(tempB, 31);
                        tempB *= _primes64[0];

                        hashValue ^= tempB;
                        hashValue = (hashValue * _primes64[0]) + _primes64[3];

                        // C
                        tempC *= _primes64[1];
                        tempC = RotateLeft(tempC, 31);
                        tempC *= _primes64[0];

                        hashValue ^= tempC;
                        hashValue = (hashValue * _primes64[0]) + _primes64[3];

                        // D
                        tempD *= _primes64[1];
                        tempD = RotateLeft(tempD, 31);
                        tempD *= _primes64[0];

                        hashValue ^= tempD;
                        hashValue = (hashValue * _primes64[0]) + _primes64[3];

                    } else {
                        hashValue = _seed + _primes64[4];
                    }
                }

                var remainder = FinalizeInputBuffer;
                var remainderLength = (remainder?.Length).GetValueOrDefault();

                hashValue += (UInt64) _bytesProcessed + (UInt64) remainderLength;

                if (remainderLength > 0)
                {

                    // In 8-byte chunks, process all full chunks
                    for (int x = 0; x < remainder.Length / 8; ++x)
                    {
                        hashValue ^= RotateLeft(BitConverter.ToUInt64(remainder, x * 8) * _primes64[1], 31) * _primes64[0];
                        hashValue = (RotateLeft(hashValue, 27) * _primes64[0]) + _primes64[3];
                    }

                    // Process a 4-byte chunk if it exists
                    if ((remainderLength % 8) >= 4)
                    {
                        var startOffset = remainderLength - (remainderLength % 8);

                        hashValue ^= ((UInt64) BitConverter.ToUInt32(remainder, startOffset)) * _primes64[0];
                        hashValue = (RotateLeft(hashValue, 23) * _primes64[1]) + _primes64[2];
                    }

                    // Process last 4 bytes in 1-byte chunks (only runs if data.Length % 4 != 0)
                    {
                        var startOffset = remainderLength - (remainderLength % 4);
                        var endOffset = remainderLength;

                        for (var currentOffset = startOffset; currentOffset < endOffset; currentOffset += 1)
                        {
                            hashValue ^= (UInt64) remainder[currentOffset] * _primes64[4];
                            hashValue = RotateLeft(hashValue, 11) * _primes64[0];
                        }
                    }
                }

                hashValue ^= hashValue >> 33;
                hashValue *= _primes64[1];
                hashValue ^= hashValue >> 29;
                hashValue *= _primes64[2];
                hashValue ^= hashValue >> 32;

                return new HashValue(
                    BitConverter.GetBytes(hashValue),
                    64);
            }


            private static UInt64 RotateLeft(UInt64 operand, int shiftCount)
            {
                shiftCount &= 0x3f;

                return
                    (operand << shiftCount) |
                    (operand >> (64 - shiftCount));
            }
        }

    }
}
