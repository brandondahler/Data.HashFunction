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

namespace System.Data.HashFunction
{
    /// <summary>
    /// Base implementation of BuzHash as specified at http://www.serve.net/buz/hash.adt/java.002.html.
    /// 
    /// Relies on a random table of 256 (preferably distinct) 64-bit integers.
    /// Also can be set to use left or right rotation when running the rotate step.
    /// </summary>
    public abstract class BuzHashBase
        : HashFunctionAsyncBase
    {
        /// <summary>Table of 256 (preferably random and distinct) UInt64 values.</summary>
        public IReadOnlyList<UInt64> Rtab { get { return _Rtab; } }

        /// <summary>Direction that the circular shift step should use.</summary>
        public CircularShiftDirection ShiftDirection { get { return _ShiftDirection; } }

        /// <summary>Initialization value to use for the hash.</summary>
        public UInt64 InitVal { get { return _InitVal; } }


        /// <summary>The list of possible hash sizes that can be provided to the <see cref="BuzHashBase"/> constructor.</summary>
        public static IEnumerable<int> ValidHashSizes { get { return _ValidHashSizes; } }


        /// <summary>Enumeration of possible directions a circular shift can be defined for.</summary>
        public enum CircularShiftDirection
        {
            /// <summary>Shift bits left.</summary>
            Left,
            /// <summary>Shift bits right.</summary>
            Right
        }


        private readonly IReadOnlyList<UInt64> _Rtab;
        private readonly CircularShiftDirection _ShiftDirection;
        private readonly UInt64 _InitVal;

        private static readonly IEnumerable<int> _ValidHashSizes = new[] { 8, 16, 32, 64 };



        /// <remarks>
        /// Defaults <see cref="HashFunctionBase.HashSize"/> to 64. <inheritdoc cref="BuzHashBase(IReadOnlyList{UInt64}, CircularShiftDirection, int)"/>
        /// </remarks>
        /// <inheritdoc cref="BuzHashBase(IReadOnlyList{UInt64}, CircularShiftDirection, int)"/>
        protected BuzHashBase(IReadOnlyList<UInt64> rtab, CircularShiftDirection shiftDirection)
            : this(rtab, shiftDirection, 64)
        {

        }

        /// <remarks>
        /// Defaults <see cref="InitVal"/> to 0. <inheritdoc cref="BuzHashBase(IReadOnlyList{UInt64}, CircularShiftDirection, UInt64, int)"/>
        /// </remarks>
        /// <inheritdoc cref="BuzHashBase(IReadOnlyList{UInt64}, CircularShiftDirection, UInt64, int)"/>
        protected BuzHashBase(IReadOnlyList<UInt64> rtab, CircularShiftDirection shiftDirection, int hashSize)
            : this(rtab, shiftDirection, 0U, hashSize)
        {

        }

        /// <remarks>
        /// Defaults <see cref="HashFunctionBase.HashSize"/> to 64.
        /// </remarks>
        /// <inheritdoc cref="BuzHashBase(IReadOnlyList{UInt64}, CircularShiftDirection, UInt64, int)"/>
        protected BuzHashBase(IReadOnlyList<UInt64> rtab, CircularShiftDirection shiftDirection, UInt64 initVal)
            : this(rtab, shiftDirection, initVal, 64)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuzHashBase" /> class.
        /// </summary>
        /// <param name="rtab"><inheritdoc cref="Rtab" /></param>
        /// <param name="shiftDirection"><inheritdoc cref="ShiftDirection" /></param>
        /// <param name="initVal"><inheritdoc cref="InitVal" /></param>
        /// <param name="hashSize"><inheritdoc cref="HashFunctionBase(int)" select="param[name=hashSize]" /></param>
        /// <exception cref="System.ArgumentOutOfRangeException">hashSize;hashSize must be contained within <see cref="ValidHashSizes" />.</exception>
        /// <inheritdoc cref="HashFunctionBase(int)" />
        protected BuzHashBase(IReadOnlyList<UInt64> rtab, CircularShiftDirection shiftDirection, UInt64 initVal, int hashSize)
            : base(hashSize)
        {
            if (!ValidHashSizes.Contains(hashSize))
                throw new ArgumentOutOfRangeException("hashSize", "hashSize must be contained within BuzHashBase.ValidHashSizes.");

            _Rtab = rtab;
            _ShiftDirection = shiftDirection;

            _InitVal = initVal;
        }



        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(IUnifiedData data, CancellationToken cancellationToken)
        {
            byte[] hash = null;

            switch (HashSize)
            {
                case 8:
                    {
                        byte h = (byte)InitVal;

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
                        UInt16 h = (UInt16)InitVal;

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
                        UInt32 h = (UInt32)InitVal;

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
                        UInt64 h = InitVal;

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
                    {
                        throw new NotImplementedException();
                    }
            }

            return hash;
        }

        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override async Task<byte[]> ComputeHashAsyncInternal(IUnifiedDataAsync data, CancellationToken cancellationToken)
        {
            byte[] hash = null;

            switch (HashSize)
            {
                case 8:
                    {
                        byte h = (byte)InitVal;

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
                        UInt16 h = (UInt16)InitVal;

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
                        UInt32 h = (UInt32)InitVal;

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
                        UInt64 h = InitVal;

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
                    {
                        throw new NotImplementedException();
                    }
            }

            return hash;
        }



        private void ProcessBytes(ref byte h, byte[] dataBytes, int position, int length)
        {
            for (var x = position; x < position + length; ++x)
                h = (byte)(CShift(h, 1) ^ (byte)Rtab[dataBytes[x]]);
        }

        private void ProcessBytes(ref UInt16 h, byte[] dataBytes, int position, int length)
        {
            for (var x = position; x < position + length; ++x)
                h = (UInt16)(CShift(h, 1) ^ (UInt16)Rtab[dataBytes[x]]);
        }

        private void ProcessBytes(ref UInt32 h, byte[] dataBytes, int position, int length)
        {
            for (var x = position; x < position + length; ++x)
                h = CShift(h, 1) ^ (UInt32)Rtab[dataBytes[x]];
        }

        private void ProcessBytes(ref UInt64 h, byte[] dataBytes, int position, int length)
        {
            for (var x = position; x < position + length; ++x)
                h = CShift(h, 1) ^ Rtab[dataBytes[x]];
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
            if (ShiftDirection == CircularShiftDirection.Right)
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
            if (ShiftDirection == CircularShiftDirection.Right)
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
            if (ShiftDirection == CircularShiftDirection.Right)
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
            if (ShiftDirection == CircularShiftDirection.Right)
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
