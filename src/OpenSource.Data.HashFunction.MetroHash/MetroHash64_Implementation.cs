using System;
using OpenSource.Data.HashFunction.Core;
using System.Threading;
using OpenSource.Data.HashFunction.Core.Utilities;

namespace OpenSource.Data.HashFunction.MetroHash
{
    /// <summary>
    /// Implementation of MetroHash64 as specified at https://github.com/jandrewrogers/MetroHash.
    /// 
    /// "
    /// MetroHash is a set of state-of-the-art hash functions for non-cryptographic use cases. 
    /// They are notable for being algorithmically generated in addition to their exceptional performance. 
    /// The set of published hash functions may be expanded in the future, 
    /// having been selected from a very large set of hash functions that have been constructed this way.
    /// "
    /// </summary>
    internal class MetroHash64_Implementation
        : StreamableHashFunctionBase,
            IMetroHash64
    {
        public IMetroHashConfig Config => _config.Clone();
        public override int HashSizeInBits { get; } = 64;


        private readonly IMetroHashConfig _config;

        public MetroHash64_Implementation(IMetroHashConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Clone();
        }

        public override IHashFunctionBlockTransformer CreateBlockTransformer() =>
            new BlockTransformer(_config.Seed);

        private class BlockTransformer
            : HashFunctionBlockTransformerBase<BlockTransformer>
        {
            private const UInt64 _k0 = 0xD6D018F5;
            private const UInt64 _k1 = 0xA2AA033B;
            private const UInt64 _k2 = 0x62992FC1;
            private const UInt64 _k3 = 0x30BC5B29;


            private UInt64 _initialValue;

            private UInt64 _a;
            private UInt64 _b;
            private UInt64 _c;
            private UInt64 _d;

            private UInt64 _bytesProcessed;



            public BlockTransformer()
                : base(inputBlockSize: 32)
            {

            }

            public BlockTransformer(UInt64 seed)
                : this()
            {
                _initialValue = (seed + _k2) * _k0;

                _a = _initialValue;
                _b = _initialValue;
                _c = _initialValue;
                _d = _initialValue;

                _bytesProcessed = 0;
            }

            protected override void CopyStateTo(BlockTransformer other)
            {
                base.CopyStateTo(other);

                other._initialValue = _initialValue;

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

                for (var currentOffset = dataOffset; currentOffset < endOffset; currentOffset += 32)
                {
                    tempA += BitConverter.ToUInt64(dataArray, currentOffset) * _k0;
                    tempA = RotateRight(tempA, 29) + tempC;

                    tempB += BitConverter.ToUInt64(dataArray, currentOffset + 8) * _k1;
                    tempB = RotateRight(tempB, 29) + tempD;

                    tempC += BitConverter.ToUInt64(dataArray, currentOffset + 16) * _k2;
                    tempC = RotateRight(tempC, 29) + tempA;

                    tempD += BitConverter.ToUInt64(dataArray, currentOffset + 24) * _k3;
                    tempD = RotateRight(tempD, 29) + tempB;
                }

                _a = tempA;
                _b = tempB;
                _c = tempC;
                _d = tempD;

                _bytesProcessed += (UInt64) dataCount;
            }

            protected override IHashValue FinalizeHashValueInternal(CancellationToken cancellationToken)
            {
                var tempA = _a;
                var tempB = _b;
                var tempC = _c;
                var tempD = _d;

                if (_bytesProcessed > 0)
                {
                    tempC ^= RotateRight(((tempA + tempD) * _k0) + tempB, 37) * _k1;
                    tempD ^= RotateRight(((tempB + tempC) * _k1) + tempA, 37) * _k0;
                    tempA ^= RotateRight(((tempA + tempC) * _k0) + tempD, 37) * _k1;
                    tempB ^= RotateRight(((tempB + tempD) * _k1) + tempC, 37) * _k0;


                    tempA = _initialValue + (tempA ^ tempB);
                }


                var remainder = FinalizeInputBuffer;
                var remainderOffset = 0;
                var remainderCount = (remainder?.Length).GetValueOrDefault();

                if (remainderCount >= 16)
                {
                    tempB = tempA + (BitConverter.ToUInt64(remainder, remainderOffset) * _k2);
                    tempB = RotateRight(tempB, 29) * _k3;

                    tempC = tempA + (BitConverter.ToUInt64(remainder, remainderOffset + 8) * _k2);
                    tempC = RotateRight(tempC, 29) * _k3;

                    tempB ^= RotateRight(tempB * _k0, 21) + tempC;
                    tempC ^= RotateRight(tempC * _k3, 21) + tempB;
                    tempA += tempC;


                    remainderOffset += 16;
                    remainderCount -= 16;
                }

                if (remainderCount >= 8)
                {
                    tempA += BitConverter.ToUInt64(remainder, remainderOffset) * _k3;
                    tempA ^= RotateRight(tempA, 55) * _k1;

                    remainderOffset += 8;
                    remainderCount -= 8;
                }

                if (remainderCount >= 4)
                {
                    tempA += BitConverter.ToUInt32(remainder, remainderOffset) * _k3;
                    tempA ^= RotateRight(tempA, 26) * _k1;

                    remainderOffset += 4;
                    remainderCount -= 4;
                }

                if (remainderCount >= 2)
                {
                    tempA += BitConverter.ToUInt16(remainder, remainderOffset) * _k3;
                    tempA ^= RotateRight(tempA, 48) * _k1;

                    remainderOffset += 2;
                    remainderCount -= 2;
                }

                if (remainderCount >= 1)
                {
                    tempA += remainder[remainderOffset] * _k3;
                    tempA ^= RotateRight(tempA, 37) * _k1;
                }


                tempA ^= RotateRight(tempA, 28);
                tempA *= _k0;
                tempA ^= RotateRight(tempA, 29);

                return new HashValue(
                    BitConverter.GetBytes(tempA),
                    64);
            }

            private static UInt64 RotateRight(UInt64 operand, int shiftCount)
            {
                shiftCount &= 0x3f;

                return
                    (operand >> shiftCount) |
                    (operand << (64 - shiftCount));
            }
        }
    }
}
