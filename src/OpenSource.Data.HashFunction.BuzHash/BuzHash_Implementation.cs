using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core;
using OpenSource.Data.HashFunction.Core.Utilities;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSource.Data.HashFunction.BuzHash
{
    /// <summary>
    /// Implementation of BuzHash as specified at http://www.serve.net/buz/hash.adt/java.002.html.
    /// 
    /// Relies on a random table of 256 (preferably distinct) 64-bit integers.
    /// Also can be set to use left or right rotation when running the rotate step.
    /// </summary>
    internal class BuzHash_Implementation
        : StreamableHashFunctionBase,
            IBuzHash
    {
        public IBuzHashConfig Config => _config.Clone();

        public override int HashSizeInBits => _config.HashSizeInBits;


        private readonly IBuzHashConfig _config;


        private static readonly IEnumerable<int> _validHashSizes = 
            new HashSet<int>() { 8, 16, 32, 64 };



        /// <summary>
        /// Initializes a new instance of the <see cref="BuzHash_Implementation" /> class.
        /// </summary>
        /// <param name="config">The configuration to use for this instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="config"/></exception>
        /// <exception cref="ArgumentException"><paramref name="config"/>.<see cref="IBuzHashConfig.Rtab"/>;<paramref name="config"/>.<see cref="IBuzHashConfig.Rtab"/> must be non-null list of 256 <see cref="UInt64"/> values.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="config"/>.<see cref="IBuzHashConfig.HashSizeInBits"/>;<paramref name="config"/>.<see cref="IBuzHashConfig.HashSizeInBits"/> must be contained within BuzHashBase.ValidHashSizes</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="config"/>.<see cref="IBuzHashConfig.ShiftDirection"/>;<paramref name="config"/>.<see cref="IBuzHashConfig.ShiftDirection"/> must be a valid <see cref="CircularShiftDirection"/> value</exception>
        public BuzHash_Implementation(IBuzHashConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Clone();


            if (_config.Rtab == null || _config.Rtab.Count != 256)
                throw new ArgumentException($"{nameof(config.Rtab)} must be non-null list of 256 {nameof(UInt64)} values.", $"{nameof(config)}.{nameof(config.Rtab)}");

            if (!_validHashSizes.Contains(_config.HashSizeInBits))
                throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.HashSizeInBits)}", _config.HashSizeInBits, $"{nameof(config)}.{nameof(config.HashSizeInBits)} must be contained within BuzHashBase.ValidHashSizes.");

            if (_config.ShiftDirection != CircularShiftDirection.Left && _config.ShiftDirection != CircularShiftDirection.Right)
                throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.ShiftDirection)}", _config.ShiftDirection, $"{nameof(config)}.{nameof(config.ShiftDirection)} must be a valid {nameof(CircularShiftDirection)} value.");
        }


        public override IHashFunctionBlockTransformer CreateBlockTransformer()
        {

            switch (_config.HashSizeInBits)
            {
                case 8:
                    return new BlockTransformer_8Bit(_config);

                case 16:
                    return new BlockTransformer_16Bit(_config);

                case 32:
                    return new BlockTransformer_32Bit(_config);

                case 64:
                    return new BlockTransformer_64Bit(_config);

                default:
                    throw new NotImplementedException();
            }
        }


        private class BlockTransformer_8Bit
            : HashFunctionBlockTransformerBase<BlockTransformer_8Bit>
        {
            private IReadOnlyList<UInt64> _rtab;
            private CircularShiftDirection _shiftDirection;

            private byte _hashValue;

            public BlockTransformer_8Bit()
            {

            }

            public BlockTransformer_8Bit(IBuzHashConfig config)
                : this()
            {
                _rtab = config.Rtab;
                _shiftDirection = config.ShiftDirection;

                _hashValue = (byte) config.Seed;
            }

            protected override void CopyStateTo(BlockTransformer_8Bit other)
            {
                base.CopyStateTo(other);

                other._hashValue = _hashValue;
                other._rtab = _rtab;
            }

            protected override void TransformByteGroupsInternal(ArraySegment<byte> data)
            {
                var dataArray = data.Array;
                var dataCount = data.Count;
                var endOffset = data.Offset + dataCount;

                var tempHashValue = _hashValue;
                var tempRtab = _rtab;

                for (var currentOffset = data.Offset; currentOffset < endOffset; ++currentOffset)
                    tempHashValue = (byte) (CShift(tempHashValue, 1, _shiftDirection) ^ (byte) tempRtab[dataArray[currentOffset]]);

                _hashValue = tempHashValue;
            }

            protected override IHashValue FinalizeHashValueInternal(CancellationToken cancellationToken) =>
                new HashValue(new byte[] { _hashValue }, 8);
        }


        private class BlockTransformer_16Bit
            : HashFunctionBlockTransformerBase<BlockTransformer_16Bit>
        {
            
            private IReadOnlyList<UInt64> _rtab;
            private CircularShiftDirection _shiftDirection;

            private UInt16 _hashValue;

            public BlockTransformer_16Bit()
            {

            }

            public BlockTransformer_16Bit(IBuzHashConfig config)
                : this()
            {
                _rtab = config.Rtab;
                _shiftDirection = config.ShiftDirection;

                _hashValue = (UInt16) config.Seed;
            }

            protected override void CopyStateTo(BlockTransformer_16Bit other)
            {
                base.CopyStateTo(other);

                other._hashValue = _hashValue;
                other._rtab = _rtab;
            }

            protected override void TransformByteGroupsInternal(ArraySegment<byte> data)
            {
                var dataArray = data.Array;
                var dataCount = data.Count;
                var endOffset = data.Offset + dataCount;

                var tempHashValue = _hashValue;
                var tempRtab = _rtab;

                for (var currentOffset = data.Offset; currentOffset < endOffset; ++currentOffset)
                    tempHashValue = (UInt16) (CShift(tempHashValue, 1, _shiftDirection) ^ (UInt16) tempRtab[dataArray[currentOffset]]);

                _hashValue = tempHashValue;
            }

            protected override IHashValue FinalizeHashValueInternal(CancellationToken cancellationToken) =>
                new HashValue(BitConverter.GetBytes(_hashValue), 16);
        }

        private class BlockTransformer_32Bit
        : HashFunctionBlockTransformerBase<BlockTransformer_32Bit>
        {
            
            private IReadOnlyList<UInt64> _rtab;
            private CircularShiftDirection _shiftDirection;

            private UInt32 _hashValue;

            public BlockTransformer_32Bit()
            {

            }

            public BlockTransformer_32Bit(IBuzHashConfig config)
                : this()
            {
                _rtab = config.Rtab;
                _shiftDirection = config.ShiftDirection;

                _hashValue = (UInt32) config.Seed;
            }

            protected override void CopyStateTo(BlockTransformer_32Bit other)
            {
                base.CopyStateTo(other);

                other._hashValue = _hashValue;
                other._rtab = _rtab;
            }

            protected override void TransformByteGroupsInternal(ArraySegment<byte> data)
            {
                var dataArray = data.Array;
                var dataCount = data.Count;
                var endOffset = data.Offset + dataCount;

                var tempHashValue = _hashValue;
                var tempRtab = _rtab;

                for (var currentOffset = data.Offset; currentOffset < endOffset; ++currentOffset)
                    tempHashValue = (UInt32) (CShift(tempHashValue, 1, _shiftDirection) ^ (UInt32) tempRtab[dataArray[currentOffset]]);

                _hashValue = tempHashValue;
            }

            protected override IHashValue FinalizeHashValueInternal(CancellationToken cancellationToken) =>
                new HashValue(BitConverter.GetBytes(_hashValue), 32);
        }

        private class BlockTransformer_64Bit
            : HashFunctionBlockTransformerBase<BlockTransformer_64Bit>
        {
            private IReadOnlyList<UInt64> _rtab;
            private CircularShiftDirection _shiftDirection;

            private UInt64 _hashValue;

            public BlockTransformer_64Bit()
            {

            }

            public BlockTransformer_64Bit(IBuzHashConfig config)
                : this()
            {
                _rtab = config.Rtab;
                _shiftDirection = config.ShiftDirection;

                _hashValue = (UInt64) config.Seed;
            }

            protected override void CopyStateTo(BlockTransformer_64Bit other)
            {
                base.CopyStateTo(other);

                other._hashValue = _hashValue;
                other._rtab = _rtab;
            }

            protected override void TransformByteGroupsInternal(ArraySegment<byte> data)
            {
                var dataArray = data.Array;
                var dataCount = data.Count;
                var endOffset = data.Offset + dataCount;

                var tempHashValue = _hashValue;
                var tempRtab = _rtab;

                for (var currentOffset = data.Offset; currentOffset < endOffset; ++currentOffset)
                    tempHashValue = (UInt64) (CShift(tempHashValue, 1, _shiftDirection) ^ (UInt64) tempRtab[dataArray[currentOffset]]);

                _hashValue = tempHashValue;
            }

            protected override IHashValue FinalizeHashValueInternal(CancellationToken cancellationToken) =>
                new HashValue(BitConverter.GetBytes(_hashValue), 64);
        }

        #region CShift

        private static byte CShift(byte n, int shiftCount, CircularShiftDirection shiftDirection)
        {
            if (shiftDirection == CircularShiftDirection.Right)
                return RotateRight(n, shiftCount);

            return RotateLeft(n, shiftCount);
        }

        private static UInt16 CShift(UInt16 n, int shiftCount, CircularShiftDirection shiftDirection)
        {
            if (shiftDirection == CircularShiftDirection.Right)
                return RotateRight(n, shiftCount);

            return RotateLeft(n, shiftCount);
        }

        private static UInt32 CShift(UInt32 n, int shiftCount, CircularShiftDirection shiftDirection)
        {
            if (shiftDirection == CircularShiftDirection.Right)
                return RotateRight(n, shiftCount);

            return RotateLeft(n, shiftCount);
        }

        private static UInt64 CShift(UInt64 n, int shiftCount, CircularShiftDirection shiftDirection)
        {
            if (shiftDirection == CircularShiftDirection.Right)
                return RotateRight(n, shiftCount);

            return RotateLeft(n, shiftCount);
        }

        #endregion

        #region RotateLeft

        private static byte RotateLeft(byte operand, int shiftCount)
        {
            shiftCount &= 0x07;

            return (byte)(
                (operand << shiftCount) |
                (operand >> (8 - shiftCount)));
        }

        private static UInt16 RotateLeft(UInt16 operand, int shiftCount)
        {
            shiftCount &= 0x0f;

            return (UInt16)(
                (operand << shiftCount) |
                (operand >> (16 - shiftCount)));
        }

        private static UInt32 RotateLeft(UInt32 operand, int shiftCount)
        {
            shiftCount &= 0x1f;

            return
                (operand << shiftCount) |
                (operand >> (32 - shiftCount));
        }

        private static UInt64 RotateLeft(UInt64 operand, int shiftCount)
        {
            shiftCount &= 0x3f;

            return
                (operand << shiftCount) |
                (operand >> (64 - shiftCount));
        }

        #endregion

        #region RotateRight
        
        private static byte RotateRight(byte operand, int shiftCount)
        {
            shiftCount &= 0x07;

            return (byte)(
                (operand >> shiftCount) |
                (operand << (8 - shiftCount)));
        }

        private static UInt16 RotateRight(UInt16 operand, int shiftCount)
        {
            shiftCount &= 0x0f;

            return (UInt16)(
                (operand >> shiftCount) |
                (operand << (16 - shiftCount)));
        }

        private static UInt32 RotateRight(UInt32 operand, int shiftCount)
        {
            shiftCount &= 0x1f;

            return
                (operand >> shiftCount) |
                (operand << (32 - shiftCount));
        }

        private static UInt64 RotateRight(UInt64 operand, int shiftCount)
        {
            shiftCount &= 0x3f;

            return
                (operand >> shiftCount) |
                (operand << (64 - shiftCount));
        }

        #endregion

    }
}
