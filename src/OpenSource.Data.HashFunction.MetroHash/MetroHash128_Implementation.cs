using System;
using System.Threading;
using OpenSource.Data.HashFunction.Core;
using OpenSource.Data.HashFunction.Core.Utilities;

namespace OpenSource.Data.HashFunction.MetroHash
{
    /// <summary>
    /// Implementation of MetroHash128 as specified at https://github.com/jandrewrogers/MetroHash.
    /// 
    /// "
    /// MetroHash is a set of state-of-the-art hash functions for non-cryptographic use cases. 
    /// They are notable for being algorithmically generated in addition to their exceptional performance. 
    /// The set of published hash functions may be expanded in the future, 
    /// having been selected from a very large set of hash functions that have been constructed this way.
    /// "
    /// </summary>
    internal class MetroHash128_Implementation
        : StreamableHashFunctionBase,
            IMetroHash128
    {
        private const UInt64 _k0 = 0xC83A91E1;
        private const UInt64 _k1 = 0x8648DBDB;
        private const UInt64 _k2 = 0x7BDEC03B;
        private const UInt64 _k3 = 0x2F5870A5;
        


        public IMetroHashConfig Config => _config.Clone();
        public override int HashSizeInBits { get; } = 128;


        private readonly IMetroHashConfig _config;

        public MetroHash128_Implementation(IMetroHashConfig config)
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
                _a = (seed - _k0) * _k3;
                _b = (seed + _k1) * _k2;
                _c = (seed + _k0) * _k2;
                _d = (seed - _k1) * _k3;

                _bytesProcessed = 0;
            }

            protected override void CopyStateTo(BlockTransformer other)
            {
                base.CopyStateTo(other);

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

                _bytesProcessed += (UInt64)dataCount;
            }

            protected override IHashValue FinalizeHashValueInternal(CancellationToken cancellationToken)
            {
                var tempA = _a;
                var tempB = _b;
                var tempC = _c;
                var tempD = _d;

                if (_bytesProcessed > 0)
                {
                    tempC ^= RotateRight(((tempA + tempD) * _k0) + tempB, 21) * _k1;
                    tempD ^= RotateRight(((tempB + tempC) * _k1) + tempA, 21) * _k0;
                    tempA ^= RotateRight(((tempA + tempC) * _k0) + tempD, 21) * _k1;
                    tempB ^= RotateRight(((tempB + tempD) * _k1) + tempC, 21) * _k0;
                }


                var remainder = FinalizeInputBuffer;
                var remainderOffset = 0;
                var remainderCount = (remainder?.Length).GetValueOrDefault();


                if (remainderCount >= 16)
                {
                    tempA += BitConverter.ToUInt64(remainder, remainderOffset) * _k2;
                    tempA = RotateRight(tempA, 33) * _k3;

                    tempB += BitConverter.ToUInt64(remainder, remainderOffset + 8) * _k2;
                    tempB = RotateRight(tempB, 33) * _k3;

                    tempA ^= RotateRight((tempA * _k2) + tempB, 45) * _k1;
                    tempB ^= RotateRight((tempB * _k3) + tempA, 45) * _k0;

                    remainderOffset += 16;
                    remainderCount -= 16;
                }

                if (remainderCount >= 8)
                {
                    tempA += BitConverter.ToUInt64(remainder, remainderOffset) * _k2;
                    tempA = RotateRight(tempA, 33) * _k3;
                    tempA ^= RotateRight((tempA * _k2) + tempB, 27) * _k1;

                    remainderOffset += 8;
                    remainderCount -= 8;
                }

                if (remainderCount >= 4)
                {
                    tempB += BitConverter.ToUInt32(remainder, remainderOffset) * _k2;
                    tempB = RotateRight(tempB, 33) * _k3;
                    tempB ^= RotateRight((tempB * _k3) + tempA, 46) * _k0;

                    remainderOffset += 4;
                    remainderCount -= 4;
                }

                if (remainderCount >= 2)
                {
                    tempA += BitConverter.ToUInt16(remainder, remainderOffset) * _k2;
                    tempA = RotateRight(tempA, 33) * _k3;
                    tempA ^= RotateRight((tempA * _k2) + tempB, 22) * _k1;

                    remainderOffset += 2;
                    remainderCount -= 2;
                }

                if (remainderCount >= 1)
                {
                    tempB += remainder[remainderOffset] * _k2;
                    tempB = RotateRight(tempB, 33) * _k3;
                    tempB ^= RotateRight((tempB * _k3) + tempA, 58) * _k0;
                }


                tempA += RotateRight((tempA * _k0) + tempB, 13);
                tempB += RotateRight((tempB * _k1) + tempA, 37);
                tempA += RotateRight((tempA * _k2) + tempB, 13);
                tempB += RotateRight((tempB * _k3) + tempA, 37);


                var hashValueBytes = new byte[16];

                Array.Copy(BitConverter.GetBytes(tempA), 0, hashValueBytes, 0, 8);
                Array.Copy(BitConverter.GetBytes(tempB), 0, hashValueBytes, 8, 8);

                return new HashValue(hashValueBytes, 128);
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
