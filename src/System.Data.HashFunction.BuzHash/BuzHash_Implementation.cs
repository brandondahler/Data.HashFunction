using System;
using System.Collections.Generic;
using System.Data.HashFunction.Core;
using System.Data.HashFunction.Core.Utilities;
using System.Data.HashFunction.Core.Utilities.UnifiedData;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.HashFunction.BuzHash
{
    /// <summary>
    /// Implementation of BuzHash as specified at http://www.serve.net/buz/hash.adt/java.002.html.
    /// 
    /// Relies on a random table of 256 (preferably distinct) 64-bit integers.
    /// Also can be set to use left or right rotation when running the rotate step.
    /// </summary>
    /// <seealso cref="HashFunctionAsyncBase" />
    /// <seealso cref="IBuzHash" />
    internal class BuzHash_Implementation
        : HashFunctionAsyncBase,
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
        /// <exception cref="System.ArgumentOutOfRangeException">hashSize;hashSize must be contained within <see cref="ValidHashSizes" />.</exception>
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



        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(IUnifiedData data, CancellationToken cancellationToken)
        {
            byte[] hash = null;

            switch (HashSizeInBits)
            {
                case 8:
                    {
                        byte h = (byte) _config.Seed;

                        data.ForEachRead(
                            (dataBytes, position, length) =>
                            {
                                ProcessBytes(ref h, dataBytes, position, length);
                            },
                            cancellationToken);

                        hash = new byte[] { h };
                        break;
                    }

                case 16:
                    {
                        UInt16 h = (UInt16) _config.Seed;

                        data.ForEachRead(
                            (dataBytes, position, length) =>
                            {
                                ProcessBytes(ref h, dataBytes, position, length);
                            },
                            cancellationToken);

                        hash = BitConverter.GetBytes(h);
                        break;
                    }

                case 32:
                    {
                        UInt32 h = (UInt32) _config.Seed;

                        data.ForEachRead(
                            (dataBytes, position, length) =>
                            {
                                ProcessBytes(ref h, dataBytes, position, length);
                            },
                            cancellationToken);

                        hash = BitConverter.GetBytes(h);
                        break;
                    }

                case 64:
                    {
                        UInt64 h = _config.Seed;

                        data.ForEachRead(
                            (dataBytes, position, length) =>
                            {
                                ProcessBytes(ref h, dataBytes, position, length);
                            },
                            cancellationToken);

                        hash = BitConverter.GetBytes(h);
                        break;
                    }

                default:
                    throw new NotImplementedException();
            }

            return hash;
        }

        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override async Task<byte[]> ComputeHashAsyncInternal(IUnifiedDataAsync data, CancellationToken cancellationToken)
        {
            byte[] hash = null;

            switch (HashSizeInBits)
            {
                case 8:
                    {
                        byte h = (byte) _config.Seed;

                        await data.ForEachReadAsync(
                                (dataBytes, position, length) =>
                                {
                                    ProcessBytes(ref h, dataBytes, position, length);
                                },
                                cancellationToken)
                            .ConfigureAwait(false);

                        hash = new byte[] { h };
                        break;
                    }

                case 16:
                    {
                        UInt16 h = (UInt16) _config.Seed;

                        await data.ForEachReadAsync(
                                (dataBytes, position, length) =>
                                {
                                    ProcessBytes(ref h, dataBytes, position, length);
                                },
                                cancellationToken)
                            .ConfigureAwait(false);

                        hash = BitConverter.GetBytes(h);
                        break;
                    }

                case 32:
                    {
                        UInt32 h = (UInt32) _config.Seed;

                        await data.ForEachReadAsync(
                                (dataBytes, position, length) =>
                                {
                                    ProcessBytes(ref h, dataBytes, position, length);
                                },
                                cancellationToken)
                            .ConfigureAwait(false);

                        hash = BitConverter.GetBytes(h);
                        break;
                    }

                case 64:
                    {
                        UInt64 h = _config.Seed;

                        await data.ForEachReadAsync(
                                (dataBytes, position, length) =>
                                {
                                    ProcessBytes(ref h, dataBytes, position, length);
                                },
                                cancellationToken)
                            .ConfigureAwait(false);

                        hash = BitConverter.GetBytes(h);
                        break;
                    }

                default:
                    throw new NotImplementedException();
            }

            return hash;
        }



        private void ProcessBytes(ref byte h, byte[] dataBytes, int position, int length)
        {
            for (var x = position; x < position + length; ++x)
                h = (byte)(CShift(h, 1) ^ (byte) _config.Rtab[dataBytes[x]]);
        }

        private void ProcessBytes(ref UInt16 h, byte[] dataBytes, int position, int length)
        {
            for (var x = position; x < position + length; ++x)
                h = (UInt16)(CShift(h, 1) ^ (UInt16) _config.Rtab[dataBytes[x]]);
        }

        private void ProcessBytes(ref UInt32 h, byte[] dataBytes, int position, int length)
        {
            for (var x = position; x < position + length; ++x)
                h = CShift(h, 1) ^ (UInt32) _config.Rtab[dataBytes[x]];
        }

        private void ProcessBytes(ref UInt64 h, byte[] dataBytes, int position, int length)
        {
            for (var x = position; x < position + length; ++x)
                h = CShift(h, 1) ^ _config.Rtab[dataBytes[x]];
        }


        #region CShift

        /// <summary>
        /// Rotate bits of input byte by amount specified.  Shifts left or right based on ShiftDirection parameter.
        /// </summary>
        /// <param name="n">Byte value to shift.</param>
        /// <param name="shiftCount">Number of bits to shift the integer by.</param>
        /// <returns>
        /// Byte value after rotating by the specified amount of bits.
        /// </returns>
        private byte CShift(byte n, int shiftCount)
        {
            if (_config.ShiftDirection == CircularShiftDirection.Right)
                return RotateRight(n, shiftCount);

            return RotateLeft(n, shiftCount);
        }

        /// <summary>
        /// Rotate bits of input integer by amount specified.  Shifts left or right based on ShiftDirection parameter.
        /// </summary>
        /// <param name="n">UInt16 value to shift.</param>
        /// <param name="shiftCount">Number of bits to shift the integer by.</param>
        /// <returns>
        /// UInt16 value after rotating by the specified amount of bits.
        /// </returns>
        private UInt16 CShift(UInt16 n, int shiftCount)
        {
            if (_config.ShiftDirection == CircularShiftDirection.Right)
                return RotateRight(n, shiftCount);

            return RotateLeft(n, shiftCount);
        }

        /// <summary>
        /// Rotate bits of input integer by amount specified.  Shifts left or right based on ShiftDirection parameter.
        /// </summary>
        /// <param name="n">UInt32 value to shift.</param>
        /// <param name="shiftCount">Number of bits to shift the integer by.</param>
        /// <returns>
        /// UInt32 value after rotating by the specified amount of bits.
        /// </returns>
        private UInt32 CShift(UInt32 n, int shiftCount)
        {
            if (_config.ShiftDirection == CircularShiftDirection.Right)
                return RotateRight(n, shiftCount);

            return RotateLeft(n, shiftCount);
        }

        /// <summary>
        /// Rotate bits of input integer by amount specified.  Shifts left or right based on ShiftDirection parameter.
        /// </summary>
        /// <param name="n">UInt64 value to shift.</param>
        /// <param name="shiftCount">Number of bits to shift the integer by.</param>
        /// <returns>
        /// UInt64 value after rotating by the specified amount of bits.
        /// </returns>
        private UInt64 CShift(UInt64 n, int shiftCount)
        {
            if (_config.ShiftDirection == CircularShiftDirection.Right)
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
