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

namespace OpenSource.Data.HashFunction.SpookyHash
{
    /// <summary>
    /// Implements SpookyHash V2 as specified at http://burtleburtle.net/bob/hash/spooky.html.
    /// </summary>
    internal class SpookyHashV2_Implementation
        : StreamableHashFunctionBase,
            ISpookyHashV2
    {

        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        public ISpookyHashConfig Config => _config.Clone();

        public override int HashSizeInBits => _config.HashSizeInBits;


        private readonly ISpookyHashConfig _config;
        private readonly UInt64 _seed1;
        private readonly UInt64 _seed2;


        private static readonly IEnumerable<int> _validHashSizes = new HashSet<int>() { 32, 64, 128 };



        /// <summary>
        /// Initializes a new instance of the <see cref="SpookyHashV2_Implementation"/> class.
        /// </summary>
        /// <param name="config">The configuration to use for this instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="config"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="config"/>.<see cref="ISpookyHashConfig.HashSizeInBits"/>;<paramref name="config"/>.<see cref="ISpookyHashConfig.HashSizeInBits"/> must be contained within SpookyHashV1.ValidHashSizes.</exception>
        public SpookyHashV2_Implementation(ISpookyHashConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Clone();


            if (!_validHashSizes.Contains(_config.HashSizeInBits))
                throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.HashSizeInBits)}", _config.HashSizeInBits, $"{nameof(config)}.{nameof(config.HashSizeInBits)} must be contained within SpookyHashV2.ValidHashSizes.");


            switch (_config.HashSizeInBits)
            {
                case 32:
                    _seed1 = _config.Seed & 0xFFFFFFFF;
                    _seed2 = _seed1;
                    break;

                case 64:
                    _seed1 = _config.Seed;
                    _seed2 = _seed1;
                    break;

                case 128:
                    _seed1 = _config.Seed;
                    _seed2 = _config.Seed2;
                    break;
            }
        }


        
        public override IHashFunctionBlockTransformer CreateBlockTransformer() =>
            new BlockTransformer(HashSizeInBits, _seed1, _seed2);

        private class BlockTransformer
            : HashFunctionBlockTransformerBase<BlockTransformer>
        {
            private static readonly IReadOnlyList<int> _MixRotationParameters = 
                new[] {
                    11, 32, 43, 31, 17, 28, 39, 57, 55, 54, 22, 46
                };

            private static readonly IReadOnlyList<int> _EndPartialRotationParameters =
                new[] {
                    44, 15, 34, 21, 38, 33, 10, 13, 38, 53, 42, 54
                };

            private static readonly IReadOnlyList<int> _ShortMixRotationParameters =
                new[] {
                    50, 52, 30, 41, 54, 48, 38, 37, 62, 34, 5, 36
                };

            private static readonly IReadOnlyList<int> _ShortEndRotationParameters =
                new[] {
                    15, 52, 26, 51, 28, 9, 47, 54, 32, 25, 63
                };


            private int _hashSizeInBits;

            private UInt64[] _hashValue;

            private byte[] _shortHashBuffer;
            private int _bytesProcessed;

            public BlockTransformer()
                : base(inputBlockSize: 96)
            {

            }

            public BlockTransformer(int hashSizeInBits, UInt64 seed1, UInt64 seed2)
                : this()
            {
                _hashSizeInBits = hashSizeInBits;

                // _hashValue
                {
                    var tempHashValue = new UInt64[12];

                    tempHashValue[0] = tempHashValue[3] = tempHashValue[6] = tempHashValue[9] = seed1;
                    tempHashValue[1] = tempHashValue[4] = tempHashValue[7] = tempHashValue[10] = seed2;
                    tempHashValue[2] = tempHashValue[5] = tempHashValue[8] = tempHashValue[11] = 0XDEADBEEFDEADBEEF;

                    _hashValue = tempHashValue;
                }

                _shortHashBuffer = null;
                _bytesProcessed = 0;
            }

            protected override void CopyStateTo(BlockTransformer other)
            {
                base.CopyStateTo(other);

                other._hashSizeInBits = _hashSizeInBits;

                other._hashValue = _hashValue.ToArray();

                other._shortHashBuffer = _shortHashBuffer?.ToArray();
                other._bytesProcessed = _bytesProcessed;
            }

            protected override void TransformByteGroupsInternal(ArraySegment<byte> data)
            {
                // Handle buffering for short hash
                if (_bytesProcessed < 192)
                {
                    var dataArray = data.Array;
                    var dataCount = data.Count;

                    if (dataCount + _bytesProcessed < 192)
                    {
                        if (_shortHashBuffer == null)
                            _shortHashBuffer = new byte[192];

                        Array.Copy(dataArray, data.Offset, _shortHashBuffer, _bytesProcessed, dataCount);

                        _bytesProcessed += dataCount;
                        return;
                    }

                    if (_shortHashBuffer != null)
                    {
                        Debug.Assert(_bytesProcessed == 96 || _bytesProcessed == 192);

                        Mix(_hashValue, new ArraySegment<byte>(_shortHashBuffer, 0, _bytesProcessed));

                        _shortHashBuffer = null;
                    }
                }

                Mix(_hashValue, data);
                _bytesProcessed += data.Count;
            }

            protected override IHashValue FinalizeHashValueInternal(CancellationToken cancellationToken)
            {
                // ShortHash
                if (_bytesProcessed < 192)
                    return ComputeShortHashInternal(cancellationToken);


                var finalHashValue = _hashValue.ToArray();
                var finalMixBuffer = new byte[96];

                var remainder = FinalizeInputBuffer;

                if (remainder != null)
                {
                    var remainderCount = remainder.Length;

                    Array.Copy(remainder, 0, finalMixBuffer, 0, remainderCount);

                    finalMixBuffer[95] = (byte)remainderCount;
                }

                End(finalHashValue, finalMixBuffer);


                switch (_hashSizeInBits)
                {
                    case 32:
                        return new HashValue(
                            BitConverter.GetBytes((UInt32) finalHashValue[0]),
                            32);

                    case 64:
                        return new HashValue(
                            BitConverter.GetBytes(finalHashValue[0]),
                            64);

                    case 128:
                        var hashValueResult = new byte[16];

                        BitConverter.GetBytes(finalHashValue[0])
                            .CopyTo(hashValueResult, 0);

                        BitConverter.GetBytes(finalHashValue[1])
                            .CopyTo(hashValueResult, 8);


                        return new HashValue(hashValueResult, 128);

                    default:
                        throw new NotImplementedException();
                }
            }

            
            private IHashValue ComputeShortHashInternal(CancellationToken cancellationToken)
            {
                var tempHashValue = new UInt64[4] {
                    _hashValue[0],
                    _hashValue[1],
                    _hashValue[2],
                    _hashValue[2]
                };

                byte[] dataArray = null;
                int dataCount;
                {
                    var shortHashBufferLength = _bytesProcessed;
                    var finalizeInputBufferLength = (FinalizeInputBuffer?.Length).GetValueOrDefault();

                    dataCount = shortHashBufferLength + finalizeInputBufferLength;

                    if (dataCount > 0)
                    {
                        dataArray = new byte[dataCount];

                        if (shortHashBufferLength > 0)
                            Array.Copy(_shortHashBuffer, 0, dataArray, 0, shortHashBufferLength);

                        if (finalizeInputBufferLength > 0)
                            Array.Copy(FinalizeInputBuffer, 0, dataArray, shortHashBufferLength, finalizeInputBufferLength);
                    }
                }

                if (dataArray != null)
                {
                    var currentOffset = 0;

                    var remainderCount = dataCount % 32;

                    // Process 32-byte groups
                    {
                        var endOffset = currentOffset + dataCount - remainderCount;
                        
                        while (currentOffset < endOffset)
                        {
                            tempHashValue[2] += BitConverter.ToUInt64(dataArray, currentOffset);
                            tempHashValue[3] += BitConverter.ToUInt64(dataArray, currentOffset + 8);

                            ShortMix(tempHashValue);

                            tempHashValue[0] += BitConverter.ToUInt64(dataArray, currentOffset + 16);
                            tempHashValue[1] += BitConverter.ToUInt64(dataArray, currentOffset + 24);

                            currentOffset += 32;
                        }
                    }

                    // Process 16-byte group (if available)
                    if (remainderCount >= 16)
                    {
                        tempHashValue[2] += BitConverter.ToUInt64(dataArray, currentOffset);
                        tempHashValue[3] += BitConverter.ToUInt64(dataArray, currentOffset + 8);

                        ShortMix(tempHashValue);

                        currentOffset += 16;
                        remainderCount -= 16;
                    }

                    tempHashValue[3] += ((UInt64) dataCount) << 56;

                    if (remainderCount > 0)
                    {
                        var finalRemainderBuffer = new byte[16];
                        Array.Copy(dataArray, currentOffset, finalRemainderBuffer, 0, remainderCount);

                        tempHashValue[3] += BitConverter.ToUInt64(finalRemainderBuffer, 8);
                        tempHashValue[2] += BitConverter.ToUInt64(finalRemainderBuffer, 0);
                        
                    } else {
                        tempHashValue[3] += 0XDEADBEEFDEADBEEF;
                        tempHashValue[2] += 0XDEADBEEFDEADBEEF;
                    }

                } else {
                    tempHashValue[3] += 0XDEADBEEFDEADBEEF;
                    tempHashValue[2] += 0XDEADBEEFDEADBEEF;
                }


                ShortEnd(tempHashValue);


                switch (_hashSizeInBits)
                {
                    case 32:
                        return new HashValue(
                            BitConverter.GetBytes((UInt32) tempHashValue[0]),
                            32);

                    case 64:
                        return new HashValue(
                            BitConverter.GetBytes(tempHashValue[0]),
                            64);

                    case 128:
                        var finalHashValue = new byte[16];

                        BitConverter.GetBytes(tempHashValue[0])
                            .CopyTo(finalHashValue, 0);

                        BitConverter.GetBytes(tempHashValue[1])
                            .CopyTo(finalHashValue, 8);

                        return new HashValue(
                            finalHashValue,
                            128);

                    default:
                        throw new NotImplementedException();
                }
            }


            private static void Mix(UInt64[] hashValue, ArraySegment<byte> data)
            {
                Debug.Assert(data.Count % 96 == 0);

                var dataArray = data.Array;
                var dataCount = data.Count;
                var endOffset = data.Offset + dataCount;

                for (var currentOffset = data.Offset; currentOffset < endOffset; currentOffset += 96)
                {
                    for (var i = 0; i < 12; ++i)
                    {
                        hashValue[i] += BitConverter.ToUInt64(dataArray, currentOffset + (i * 8));
                        hashValue[(i + 2) % 12] ^= hashValue[(i + 10) % 12];
                        hashValue[(i + 11) % 12] ^= hashValue[i];
                        hashValue[i] = RotateLeft(hashValue[i], _MixRotationParameters[i]);
                        hashValue[(i + 11) % 12] += hashValue[(i + 1) % 12];
                    }
                }
            }

            private static void End(UInt64[] hashValue, byte[] dataArray)
            {
                for (int i = 0; i < 12; ++i)
                    hashValue[i] += BitConverter.ToUInt64(dataArray, i * 8);

                EndPartial(hashValue);
                EndPartial(hashValue);
                EndPartial(hashValue);
            }

            private static void EndPartial(UInt64[] hashValue)
            {
                for (int i = 0; i < 12; ++i)
                {
                    hashValue[(i + 11) % 12] += hashValue[(i + 1) % 12];
                    hashValue[(i + 2) % 12] ^= hashValue[(i + 11) % 12];
                    hashValue[(i + 1) % 12] = RotateLeft(hashValue[(i + 1) % 12], _EndPartialRotationParameters[i]);
                }
            }


            private static void ShortMix(UInt64[] hashValue)
            {
                for (var i = 0; i < 12; ++i)
                {

                    hashValue[(i + 2) % 4] = RotateLeft(hashValue[(i + 2) % 4], _ShortMixRotationParameters[i]);
                    hashValue[(i + 2) % 4] += hashValue[(i + 3) % 4];
                    hashValue[i % 4] ^= hashValue[(i + 2) % 4];
                }
            }

            private void ShortEnd(UInt64[] hashValue)
            {
                for (int i = 0; i < 11; ++i)
                {
                    hashValue[(i + 3) % 4] ^= hashValue[(i + 2) % 4];
                    hashValue[(i + 2) % 4] = RotateLeft(hashValue[(i + 2) % 4], _ShortEndRotationParameters[i]);
                    hashValue[(i + 3) % 4] += hashValue[(i + 2) % 4];
                }
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
