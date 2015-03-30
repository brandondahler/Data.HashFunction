using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Data.HashFunction.Utilities.IntegerManipulation;
using System.Data.HashFunction.Utilities.UnifiedData;
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
#if NET45
        : HashFunctionAsyncBase
#else
        : HashFunctionBase
#endif
    {
        /// <summary>Table of 256 (preferably random and distinct) UInt64 values.</summary>
#if NET45
        public IReadOnlyList<UInt64> Rtab { get { return _Rtab; } }
#else
        public IList<UInt64> Rtab { get { return _Rtab; } }
#endif

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


#if NET45
        private readonly IReadOnlyList<UInt64> _Rtab;
#else
        private readonly IList<UInt64> _Rtab;
#endif
        private readonly CircularShiftDirection _ShiftDirection;
        private readonly UInt64 _InitVal;

        private static readonly IEnumerable<int> _ValidHashSizes = new[] { 8, 16, 32, 64 };



#if NET45
        /// <remarks>
        /// Defaults <see cref="HashFunctionBase.HashSize"/> to 64. <inheritdoc cref="BuzHashBase(IReadOnlyList{UInt64}, CircularShiftDirection, int)"/>
        /// </remarks>
        /// <inheritdoc cref="BuzHashBase(IReadOnlyList{UInt64}, CircularShiftDirection, int)"/>
        protected BuzHashBase(IReadOnlyList<UInt64> rtab, CircularShiftDirection shiftDirection)
#else
        /// <remarks>
        /// Defaults <see cref="HashFunctionBase.HashSize"/> to 64. <inheritdoc cref="BuzHashBase(IList{UInt64}, CircularShiftDirection, int)"/>
        /// </remarks>
        /// <inheritdoc cref="BuzHashBase(IList{UInt64}, CircularShiftDirection, int)"/>
        protected BuzHashBase(IList<UInt64> rtab, CircularShiftDirection shiftDirection)
#endif
            : this(rtab, shiftDirection, 64)
        {

        }

#if NET45
        /// <remarks>
        /// Defaults <see cref="InitVal"/> to 0. <inheritdoc cref="BuzHashBase(IReadOnlyList{UInt64}, CircularShiftDirection, UInt64, int)"/>
        /// </remarks>
        /// <inheritdoc cref="BuzHashBase(IReadOnlyList{UInt64}, CircularShiftDirection, UInt64, int)"/>
        protected BuzHashBase(IReadOnlyList<UInt64> rtab, CircularShiftDirection shiftDirection, int hashSize)
#else
        /// <remarks>
        /// Defaults <see cref="InitVal"/> to 0. <inheritdoc cref="BuzHashBase(IList{UInt64}, CircularShiftDirection, UInt64, int)"/>
        /// </remarks>
        /// <inheritdoc cref="BuzHashBase(IList{UInt64}, CircularShiftDirection, UInt64, int)"/>
        protected BuzHashBase(IList<UInt64> rtab, CircularShiftDirection shiftDirection, int hashSize)
#endif
            : this(rtab, shiftDirection, 0U, hashSize)
        {

        }

#if NET45
        /// <remarks>
        /// Defaults <see cref="HashFunctionBase.HashSize"/> to 64.
        /// </remarks>
        /// <inheritdoc cref="BuzHashBase(IReadOnlyList{UInt64}, CircularShiftDirection, UInt64, int)"/>
        protected BuzHashBase(IReadOnlyList<UInt64> rtab, CircularShiftDirection shiftDirection, UInt64 initVal)
#else
        /// <remarks>
        /// Defaults <see cref="HashFunctionBase.HashSize"/> to 64.
        /// </remarks>
        /// <inheritdoc cref="BuzHashBase(IList{UInt64}, CircularShiftDirection, UInt64, int)"/>
        protected BuzHashBase(IList<UInt64> rtab, CircularShiftDirection shiftDirection, UInt64 initVal)
#endif
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
#if NET45
        protected BuzHashBase(IReadOnlyList<UInt64> rtab, CircularShiftDirection shiftDirection, UInt64 initVal, int hashSize)
#else
        protected BuzHashBase(IList<UInt64> rtab, CircularShiftDirection shiftDirection, UInt64 initVal, int hashSize)
#endif
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
        protected override byte[] ComputeHashInternal(UnifiedData data)
        {
            byte[] hash = null;

            switch (HashSize)
            {
                case 8:
                {
                    byte h = (byte) InitVal;
            
                    data.ForEachRead((dataBytes, position, length) => {
                        ProcessBytes(ref h, dataBytes, position, length);
                    });
            
                    hash = new byte[] { h };
                    break;
                }

                case 16:
                { 
                    UInt16 h = (UInt16) InitVal;
            
                    data.ForEachRead((dataBytes, position, length) => {
                        ProcessBytes(ref h, dataBytes, position, length);
                    });

                    hash = BitConverter.GetBytes(h);
                    break;
                }

                case 32:
                {
                    UInt32 h = (UInt32) InitVal;
            
                    data.ForEachRead((dataBytes, position, length) => {
                        ProcessBytes(ref h, dataBytes, position, length);
                    });
            
                    hash = BitConverter.GetBytes(h);
                    break;
                }
                
                case 64:
                {
                    UInt64 h = InitVal;
            
                    data.ForEachRead((dataBytes, position, length) => {
                        ProcessBytes(ref h, dataBytes, position, length);
                    });
            
                    hash = BitConverter.GetBytes(h);
                    break;
                }
            }

            return hash;
        }
        
#if NET45
        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override async Task<byte[]> ComputeHashAsyncInternal(UnifiedData data)
        {
            byte[] hash = null;

            switch (HashSize)
            {
                case 8:
                {
                    byte h = (byte) InitVal;
            
                    await data.ForEachReadAsync((dataBytes, position, length) => {
                        ProcessBytes(ref h, dataBytes, position, length);
                    }).ConfigureAwait(false);

                    hash = new byte[] { h };
                    break;
                }

                case 16:
                { 
                    UInt16 h = (UInt16) InitVal;
            
                    await data.ForEachReadAsync((dataBytes, position, length) => {
                        ProcessBytes(ref h, dataBytes, position, length);
                    }).ConfigureAwait(false);

                    hash = BitConverter.GetBytes(h);
                    break;
                }

                case 32:
                {
                    UInt32 h = (UInt32) InitVal;
            
                    await data.ForEachReadAsync((dataBytes, position, length) => {
                        ProcessBytes(ref h, dataBytes, position, length);
                    }).ConfigureAwait(false);

                    hash = BitConverter.GetBytes(h);
                    break;
                }
                
                case 64:
                {
                    UInt64 h = InitVal;
            
                    await data.ForEachReadAsync((dataBytes, position, length) => {
                        ProcessBytes(ref h, dataBytes, position, length);
                    }).ConfigureAwait(false);
            
                    hash = BitConverter.GetBytes(h);
                    break;
                }
            }

            return hash;
        }
#endif



        private void ProcessBytes(ref byte h, byte[] dataBytes, int position, int length)
        {
            for (var x = position; x < position + length; ++x)
                h = (byte) (CShift(h, 1) ^ (byte) Rtab[dataBytes[x]]);
        }

        private void ProcessBytes(ref UInt16 h, byte[] dataBytes, int position, int length)
        {
            for (var x = position; x < position + length; ++x)
                h = (UInt16) (CShift(h, 1) ^ (UInt16) Rtab[dataBytes[x]]);
        }

        private void ProcessBytes(ref UInt32 h, byte[] dataBytes, int position, int length)
        {
            for (var x = position; x < position + length; ++x)
                h = CShift(h, 1) ^ (UInt32) Rtab[dataBytes[x]];
        }

        private void ProcessBytes(ref UInt64 h, byte[] dataBytes, int position, int length)
        {
            for (var x = position; x < position + length; ++x)
                h = CShift(h, 1) ^ Rtab[dataBytes[x]];
        }


        /// <summary>
        /// Rotate bits of input byte by amount specified.  Shifts left or right based on ShiftDirection parameter.
        /// </summary>
        /// <param name="n">Byte value to shift.</param>
        /// <param name="shiftCount">Number of bits to shift the integer by.</param>
        /// <returns>
        /// Byte value after rotating by the specified amount of bits.
        /// </returns>
#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private byte CShift(byte n, int shiftCount)
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
#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private UInt16 CShift(UInt16 n, int shiftCount)
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
#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private UInt32 CShift(UInt32 n, int shiftCount)
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
#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private UInt64 CShift(UInt64 n, int shiftCount)
        {
            if (ShiftDirection == CircularShiftDirection.Right)
                return n.RotateRight(shiftCount);

            return n.RotateLeft(shiftCount);
        }

    }
}
