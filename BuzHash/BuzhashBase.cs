using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Base implementation of BuzHash as specified at http://www.serve.net/buz/hash.adt/java.002.html.
    /// 
    /// Relies on a random table of 256 (preferrably distinct) 64-bit integers.
    /// Also can be set to use left or right rotation when running the rotate step.
    /// </summary>
    public abstract class BuzHashBase
        : HashFunctionBase
    {
        /// <summary>
        /// Table of 256 (preferrably random and distinct) UInt64 values.
        /// </summary>
        /// <remarks>
        /// It is strongly recommended to return a reference to a static readonly private variable, otherwise each hashing will 
        ///   construct the table again.
        /// </remarks>
        public abstract UInt64[] Rtab { get; }

        /// <summary>
        /// Direction that the circular shift step should use.
        /// </summary>
        public abstract CircularShiftDirection ShiftDirection { get; }

        /// <summary>
        /// Initialization value to use for the hash.
        /// </summary>
        public virtual UInt64 InitVal { get { return 0; } }

        /// <inheritdoc/>
        public override IEnumerable<int> ValidHashSizes
        {
            get { return new[] { 8, 16, 32, 64 }; }
        }

        /// <summary>
        /// Enumeration of possible directions a circular shift can be defined for.
        /// </summary>
        public enum CircularShiftDirection
        {
            /// <summary>Shift bits left.</summary>
            Left,
            /// <summary>Shift bits right.</summary>
            Right
        }


        /// <summary>
        /// Creates new instance of <see cref="BuzHashBase"/>.
        /// </summary>
        /// <param name="defaultHashSize">Hash size to pass down to <see cref="HashFunctionBase" />.</param>
        protected BuzHashBase(int defaultHashSize = 64)
            : base(defaultHashSize)
        {

        }


        /// <inheritdoc/>
        public override byte[] ComputeHash(byte[] data)
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
                    throw new ArgumentOutOfRangeException("HashSize");
            }
        }


        /// <summary>
        /// 8-bit implementation of ComputeHash.
        /// </summary>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>1-byte array containing the hash value.</returns>
        protected byte[] ComputeHash8(byte[] data)
        {
            byte h = (byte) InitVal;

            foreach (byte b in data)
                h = (byte) (CShift(h, 1) ^ (byte) Rtab[(int) b]);

            return new byte[] { h };
        }

        /// <summary>
        /// 16-bit implementation of ComputeHash.
        /// </summary>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>2-byte array containing the hash value.</returns>
        protected byte[] ComputeHash16(byte[] data)
        {
            UInt16 h = (UInt16) InitVal;

            foreach (byte b in data)
                h = (UInt16) (CShift(h, 1) ^ (UInt32) Rtab[(int)b]);

            return BitConverter.GetBytes(h);
        }

        /// <summary>
        /// 32-bit implementation of ComputeHash.
        /// </summary>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>4-byte array containing the hash value.</returns>
        protected byte[] ComputeHash32(byte[] data)
        {
            UInt32 h = (UInt32) InitVal;

            foreach (byte b in data)
                h = CShift(h, 1) ^ (UInt32) Rtab[(int) b];
            
            return BitConverter.GetBytes(h);
        }

        /// <summary>
        /// 64-bit implementation of ComputeHash.
        /// </summary>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>8-byte array containing the hash value.</returns>
        protected byte[] ComputeHash64(byte[] data)
        {
            UInt64 h = InitVal;

            foreach (byte b in data)
                h = CShift(h, 1) ^ Rtab[(int)b];
            
            return BitConverter.GetBytes(h);
        }


        /// <summary>
        /// Rotate bits of input byte by amount specified.  Shifts left or right based on ShiftDirection parameter.
        /// </summary>
        /// <param name="n">Byte value to shift.</param>
        /// <param name="shiftBy">How many bits to shift by.</param>
        /// <returns>Byte value after rotating by the specified amount of bits.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected byte CShift(byte n, int shiftBy)
        {
            shiftBy %= 8;

            switch (ShiftDirection)
            {
                default:
                case CircularShiftDirection.Left:
                    return (byte) ((n << shiftBy) | (n >> (8 - shiftBy)));

                case CircularShiftDirection.Right:
                    return (byte) ((n >> shiftBy) | (n << (8 - shiftBy)));
            }
        }

        /// <summary>
        /// Rotate bits of input integer by amount specified.  Shifts left or right based on ShiftDirection parameter.
        /// </summary>
        /// <param name="n">UInt16 value to shift.</param>
        /// <param name="shiftBy">How many bits to shift by.</param>
        /// <returns>UInt16 value after rotating by the specified amount of bits.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected UInt16 CShift(UInt16 n, int shiftBy)
        {
            shiftBy %= 16;

            switch (ShiftDirection)
            {
                default:
                case CircularShiftDirection.Left:
                    return (UInt16) ((n << shiftBy) | (n >> (16 - shiftBy)));

                case CircularShiftDirection.Right:
                    return (UInt16) ((n >> shiftBy) | (n << (16 - shiftBy)));
            }
        }

        /// <summary>
        /// Rotate bits of input integer by amount specified.  Shifts left or right based on ShiftDirection parameter.
        /// </summary>
        /// <param name="n">UInt32 value to shift.</param>
        /// <param name="shiftBy">How many bits to shift by.</param>
        /// <returns>UInt32 value after rotating by the specified amount of bits.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected UInt32 CShift(UInt32 n, int shiftBy)
        {
            shiftBy %= 32;

            switch (ShiftDirection)
            {
                default:
                case CircularShiftDirection.Left:
                    return (n << shiftBy) | (n >> (32 - shiftBy));

                case CircularShiftDirection.Right:
                    return (n >> shiftBy) | (n << (32 - shiftBy));
            }
        }

        /// <summary>
        /// Rotate bits of input integer by amount specified.  Shifts left or right based on ShiftDirection parameter.
        /// </summary>
        /// <param name="n">UInt64 value to shift.</param>
        /// <param name="shiftBy">How many bits to shift by.</param>
        /// <returns>UInt64 value after rotating by the specified amount of bits.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected UInt64 CShift(UInt64 n, int shiftBy)
        {
            shiftBy %= 64;

            switch (ShiftDirection)
            {
                default:
                case CircularShiftDirection.Left:
                    return (n << shiftBy) | (n >> (64 - shiftBy));

                case CircularShiftDirection.Right:
                    return (n >> shiftBy) | (n << (64 - shiftBy));
            }
        }

    }
}
