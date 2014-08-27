using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Data.HashFunction.Utilities.IntegerManipulation;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
        : HashFunctionBase
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
        protected override byte[] ComputeHashInternal(Stream data)
        {
            switch (HashSize)
            {
                case 8:
                    return ComputeHash8(data);

                case 16:
                    return ComputeHash16(data);

                case 32:
                    return ComputeHash32(data);
                
                case 64:
                    return ComputeHash64(data);

                default:
                    throw new InvalidOperationException("HashSize set to an invalid value.");
            }
        }


        /// <summary>
        /// 8-bit implementation of ComputeHash.
        /// </summary>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>
        /// 1-byte array containing the hash value.
        /// </returns>
        protected byte[] ComputeHash8(Stream data)
        {
            byte h = (byte) InitVal;
            
            
            foreach (byte dataByte in data.AsEnumerable())
                h = (byte) (CShift(h, 1) ^ (byte) Rtab[dataByte]);
            
            return new byte[] { h };
        }

        /// <summary>
        /// 16-bit implementation of ComputeHash.
        /// </summary>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>
        /// 2-byte array containing the hash value.
        /// </returns>
        protected byte[] ComputeHash16(Stream data)
        {
            UInt16 h = (UInt16) InitVal;
            
            foreach (byte dataByte in data.AsEnumerable())
                h = (UInt16) (CShift(h, 1) ^ (UInt16) Rtab[dataByte]);

            return BitConverter.GetBytes(h);
        }

        /// <summary>
        /// 32-bit implementation of ComputeHash.
        /// </summary>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>
        /// 4-byte array containing the hash value.
        /// </returns>
        protected byte[] ComputeHash32(Stream data)
        {
            UInt32 h = (UInt32) InitVal;
            
            foreach (byte dataByte in data.AsEnumerable())
                h = CShift(h, 1) ^ (UInt32) Rtab[dataByte];
            
            return BitConverter.GetBytes(h);
        }

        /// <summary>
        /// 64-bit implementation of ComputeHash.
        /// </summary>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>
        /// 8-byte array containing the hash value.
        /// </returns>
        protected byte[] ComputeHash64(Stream data)
        {
            UInt64 h = InitVal;
            
            foreach (byte dataByte in data.AsEnumerable())
                h = CShift(h, 1) ^ Rtab[dataByte];
            
            return BitConverter.GetBytes(h);
        }


        /// <summary>
        /// Rotate bits of input byte by amount specified.  Shifts left or right based on ShiftDirection parameter.
        /// </summary>
        /// <param name="n">Byte value to shift.</param>
        /// <param name="shiftCount">Number of bits to shift the integer by.</param>
        /// <returns>
        /// Byte value after rotating by the specified amount of bits.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected byte CShift(byte n, int shiftCount)
        {
            if (ShiftDirection == CircularShiftDirection.Right)
                return n.RotateRight(shiftCount);

            return n.RotateLeft(shiftCount);
        }

        /// <summary>
        /// Rotate bits of input integer by amount specified.  Shifts left or right based on ShiftDirection parameter.
        /// </summary>
        /// <param name="n">UInt16 value to shift.</param>
        /// <param name="shiftCount">Number of bits to shift the integer by.</param>
        /// <returns>
        /// UInt16 value after rotating by the specified amount of bits.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected UInt16 CShift(UInt16 n, int shiftCount)
        {
            if (ShiftDirection == CircularShiftDirection.Right)
                return n.RotateRight(shiftCount);

            return n.RotateLeft(shiftCount);
        }

        /// <summary>
        /// Rotate bits of input integer by amount specified.  Shifts left or right based on ShiftDirection parameter.
        /// </summary>
        /// <param name="n">UInt32 value to shift.</param>
        /// <param name="shiftCount">Number of bits to shift the integer by.</param>
        /// <returns>
        /// UInt32 value after rotating by the specified amount of bits.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected UInt32 CShift(UInt32 n, int shiftCount)
        {
            if (ShiftDirection == CircularShiftDirection.Right)
                return n.RotateRight(shiftCount);

            return n.RotateLeft(shiftCount);
        }

        /// <summary>
        /// Rotate bits of input integer by amount specified.  Shifts left or right based on ShiftDirection parameter.
        /// </summary>
        /// <param name="n">UInt64 value to shift.</param>
        /// <param name="shiftCount">Number of bits to shift the integer by.</param>
        /// <returns>
        /// UInt64 value after rotating by the specified amount of bits.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected UInt64 CShift(UInt64 n, int shiftCount)
        {
            if (ShiftDirection == CircularShiftDirection.Right)
                return n.RotateRight(shiftCount);

            return n.RotateLeft(shiftCount);
        }

    }
}
