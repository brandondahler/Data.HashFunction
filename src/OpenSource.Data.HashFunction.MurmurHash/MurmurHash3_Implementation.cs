using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core;
using OpenSource.Data.HashFunction.Core.Utilities;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSource.Data.HashFunction.MurmurHash
{
    /// <summary>
    /// Implementation of MurmurHash3 as specified at https://github.com/aappleby/smhasher/blob/master/src/MurmurHash3.cpp 
    ///   and https://github.com/aappleby/smhasher/wiki/MurmurHash3.
    /// </summary>
    internal class MurmurHash3_Implementation
        : StreamableHashFunctionBase,
            IMurmurHash3
    {

        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        public IMurmurHash3Config Config => _config.Clone();

        public override int HashSizeInBits => _config.HashSizeInBits;


        /// <summary>
        /// Constant c1 for 32-bit calculation as defined by MurmurHash3 specification.
        /// </summary>
        private const UInt32 c1_32 = 0xcc9e2d51;

        /// <summary>
        /// Constant c2 for 32-bit calculation as defined by MurmurHash3 specification.
        /// </summary>
        private const UInt32 c2_32 = 0x1b873593;


        /// <summary>
        /// Constant c1 for 128-bit calculation as defined by MurMurHash3 specification.
        /// </summary>
        private const UInt64 c1_128 = 0x87c37b91114253d5;

        /// <summary>
        /// Constant c2 for 128-bit calculation as defined by MurMurHash3 specification.
        /// </summary>
        private const UInt64 c2_128 = 0x4cf5ad432745937f;


        private readonly IMurmurHash3Config _config;

        private static readonly IEnumerable<int> _validHashSizes = new HashSet<int>() { 32, 128 };



        /// <summary>
        /// Initializes a new instance of the <see cref="MurmurHash3_Implementation"/> class.
        /// </summary>
        /// <param name="config">The configuration to use for this instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="config"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="config"/>.<see cref="IMurmurHash3Config.HashSizeInBits"/>;<paramref name="config"/>.<see cref="IMurmurHash3Config.HashSizeInBits"/> must be contained within MurmurHash3.ValidHashSizes.</exception>
        public MurmurHash3_Implementation(IMurmurHash3Config config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Clone();


            if (!_validHashSizes.Contains(_config.HashSizeInBits))
                throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.HashSizeInBits)}", _config.HashSizeInBits, $"{nameof(config)}.{nameof(config.HashSizeInBits)} must be contained within MurmurHash3.ValidHashSizes.");
        }



        public override IHashFunctionBlockTransformer CreateBlockTransformer()
        {
            switch (_config.HashSizeInBits)
            {
                case 32:
                    return new BlockTransformer32((UInt32) _config.Seed);

                case 128:
                    return new BlockTransformer128(_config.Seed);

                default:
                    throw new NotImplementedException();
            }
        }


        private class BlockTransformer32
            : HashFunctionBlockTransformerBase<BlockTransformer32>
        {
            private UInt32 _hashValue;

            private int _bytesProcessed = 0;

            public BlockTransformer32()
                : base(inputBlockSize: 4)
            {

            }

            public BlockTransformer32(UInt32 seed)
                : this()
            {
                _hashValue = seed;
            }

            protected override void CopyStateTo(BlockTransformer32 other)
            {
                base.CopyStateTo(other);

                other._hashValue = _hashValue;

                other._bytesProcessed = _bytesProcessed;
            }

            protected override void TransformByteGroupsInternal(ArraySegment<byte> data)
            {
                var dataArray = data.Array;
                var dataOffset = data.Offset;
                var dataCount = data.Count;

                var endOffset = dataOffset + dataCount;

                var tempHashValue = _hashValue;

                for (var currentOffset = dataOffset; currentOffset < endOffset; currentOffset += 4)
                {
                    UInt32 k1 = BitConverter.ToUInt32(dataArray, currentOffset);

                    k1 *= c1_32;
                    k1 = RotateLeft(k1, 15);
                    k1 *= c2_32;

                    tempHashValue ^= k1;
                    tempHashValue = RotateLeft(tempHashValue, 13);
                    tempHashValue = (tempHashValue * 5) + 0xe6546b64;
                }

                _hashValue = tempHashValue;

                _bytesProcessed += dataCount;
            }

            protected override IHashValue FinalizeHashValueInternal(CancellationToken cancellationToken)
            {
                var remainder = FinalizeInputBuffer;
                var remainderCount = (remainder?.Length).GetValueOrDefault();

                var tempHashValue = _hashValue;

                var tempBytesProcessed = _bytesProcessed;

                if (remainderCount > 0)
                {
                    UInt32 k2 = 0;

                    switch (remainderCount)
                    {
                        case 3: k2 ^= (UInt32)remainder[2] << 16; goto case 2;
                        case 2: k2 ^= (UInt32)remainder[1] << 8; goto case 1;
                        case 1:
                            k2 ^= (UInt32)remainder[0];
                            break;
                    }

                    k2 *= c1_32;
                    k2 = RotateLeft(k2, 15);
                    k2 *= c2_32;
                    tempHashValue ^= k2;

                    tempBytesProcessed += remainderCount;
                }


                tempHashValue ^= (UInt32) tempBytesProcessed;
                Mix(ref tempHashValue);

                return new HashValue(
                    BitConverter.GetBytes(tempHashValue),
                    32);
            }

            private static void Mix(ref UInt32 h)
            {
                h ^= h >> 16;
                h *= 0x85ebca6b;
                h ^= h >> 13;
                h *= 0xc2b2ae35;
                h ^= h >> 16;
            }

            private static UInt32 RotateLeft(UInt32 operand, int shiftCount)
            {
                shiftCount &= 0x1f;

                return
                    (operand << shiftCount) |
                    (operand >> (32 - shiftCount));
            }
        }

        private class BlockTransformer128
            : HashFunctionBlockTransformerBase<BlockTransformer128>
        {
            private UInt64 _hashValue1;
            private UInt64 _hashValue2;

            private int _bytesProcessed = 0;

            public BlockTransformer128()
                : base(inputBlockSize: 16)
            {

            }

            public BlockTransformer128(UInt64 seed)
                : this()
            {
                _hashValue1 = seed;
                _hashValue2 = seed;
            }

            protected override void CopyStateTo(BlockTransformer128 other)
            {
                base.CopyStateTo(other);

                other._hashValue1 = _hashValue1;
                other._hashValue2 = _hashValue2;

                other._bytesProcessed = _bytesProcessed;
            }

            protected override void TransformByteGroupsInternal(ArraySegment<byte> data)
            {
                var dataArray = data.Array;
                var dataOffset = data.Offset;
                var dataCount = data.Count;

                var endOffset = dataOffset + dataCount;

                var tempHashValue1 = _hashValue1;
                var tempHashValue2 = _hashValue2;

                for (var currentOffset = dataOffset; currentOffset < endOffset; currentOffset += 16)
                {
                    UInt64 k1 = BitConverter.ToUInt64(dataArray, currentOffset);
                    UInt64 k2 = BitConverter.ToUInt64(dataArray, currentOffset + 8);

                    k1 *= c1_128;
                    k1 = RotateLeft(k1, 31);
                    k1 *= c2_128;
                    tempHashValue1 ^= k1;

                    tempHashValue1 = RotateLeft(tempHashValue1, 27);
                    tempHashValue1 += tempHashValue2;
                    tempHashValue1 = (tempHashValue1 * 5) + 0x52dce729;

                    k2 *= c2_128;
                    k2 = RotateLeft(k2, 33);
                    k2 *= c1_128;
                    tempHashValue2 ^= k2;

                    tempHashValue2 = RotateLeft(tempHashValue2, 31);
                    tempHashValue2 += tempHashValue1;
                    tempHashValue2 = (tempHashValue2 * 5) + 0x38495ab5;
                }

                _hashValue1 = tempHashValue1;
                _hashValue2 = tempHashValue2;

                _bytesProcessed += dataCount;
            }

            protected override IHashValue FinalizeHashValueInternal(CancellationToken cancellationToken)
            {
                var remainder = FinalizeInputBuffer;
                var remainderCount = (remainder?.Length).GetValueOrDefault();

                var tempHashValue1 = _hashValue1;
                var tempHashValue2 = _hashValue2;

                var tempBytesProcessed = _bytesProcessed;

                if (remainderCount > 0)
                {
                    UInt64 k1 = 0;
                    UInt64 k2 = 0;

                    switch(remainderCount)
                    {
                        case 15: k2 ^= (UInt64) remainder[14] << 48;   goto case 14;
                        case 14: k2 ^= (UInt64) remainder[13] << 40;   goto case 13;
                        case 13: k2 ^= (UInt64) remainder[12] << 32;   goto case 12;
                        case 12: k2 ^= (UInt64) remainder[11] << 24;   goto case 11;
                        case 11: k2 ^= (UInt64) remainder[10] << 16;   goto case 10;
                        case 10: k2 ^= (UInt64) remainder[ 9] <<  8;   goto case 9;
                        case  9:
                            k2 ^= ((UInt64) remainder[8]);
                            k2 *= c2_128; 
                            k2  = RotateLeft(k2, 33); 
                            k2 *= c1_128;
                            tempHashValue2 ^= k2;

                            goto case 8;

                        case  8:
                            k1 ^= BitConverter.ToUInt64(remainder, 0);
                            break;

                        case  7: k1 ^= (UInt64) remainder[6] << 48;    goto case 6;
                        case  6: k1 ^= (UInt64) remainder[5] << 40;    goto case 5;
                        case  5: k1 ^= (UInt64) remainder[4] << 32;    goto case 4;
                        case  4: k1 ^= (UInt64) remainder[3] << 24;    goto case 3;
                        case  3: k1 ^= (UInt64) remainder[2] << 16;    goto case 2;
                        case  2: k1 ^= (UInt64) remainder[1] <<  8;    goto case 1;
                        case  1: 
                            k1 ^= (UInt64) remainder[0];
                            break;
                    }

                    k1 *= c1_128;
                    k1  = RotateLeft(k1, 31);
                    k1 *= c2_128;
                    tempHashValue1 ^= k1;

                    tempBytesProcessed += remainderCount;
                }


                tempHashValue1 ^= (UInt64) tempBytesProcessed;
                tempHashValue2 ^= (UInt64) tempBytesProcessed;

                tempHashValue1 += tempHashValue2;
                tempHashValue2 += tempHashValue1;

                Mix(ref tempHashValue1);
                Mix(ref tempHashValue2);

                tempHashValue1 += tempHashValue2;
                tempHashValue2 += tempHashValue1;

                var hashValueBytes = BitConverter.GetBytes(tempHashValue1)
                    .Concat(BitConverter.GetBytes(tempHashValue2))
                    .ToArray();

                return new HashValue(hashValueBytes, 128);
            }

            private static void Mix(ref UInt64 k)
            {
                k ^= k >> 33;
                k *= 0xff51afd7ed558ccd;
                k ^= k >> 33;
                k *= 0xc4ceb9fe1a85ec53;
                k ^= k >> 33;
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
