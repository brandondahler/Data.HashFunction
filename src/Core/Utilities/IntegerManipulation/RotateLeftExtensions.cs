using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Utilities.IntegerManipulation
{
    /// <summary>Static class to provide RotateLeft extension functions.</summary>
    internal static class RotateLeftExtensions
    {
        /// <summary>
        /// Rotate bits of integer left by specified amount.
        /// </summary>
        /// <param name="operand">8-bit integer that will have its bits rotated left.</param>
        /// <param name="shiftCount">Number of bits to shift the integer by.</param>
        /// <returns>
        /// Resulting 8-bit integer after rotating the operand integer's bits by the amount specified by the shiftCount parameter.
        /// </returns>
        /// <remarks>
        /// The shift count is given by the low-order three bits of the shiftCount parameter.
        /// That is, the actual shift count is 0 to 7 bits.
        /// </remarks>
#if !NET40
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
#endif
        public static byte RotateLeft(this byte operand, int shiftCount)
        {
            shiftCount &= 0x07;

            return (byte)(
                (operand << shiftCount) |
                (operand >> (8 - shiftCount)));
        }

        /// <summary>
        /// Rotate bits of integer left by specified amount.
        /// </summary>
        /// <param name="operand">16-bit integer that will have its bits rotated left.</param>
        /// <param name="shiftCount">Number of bits to shift the integer by.</param>
        /// <returns>
        /// Resulting 16-bit integer after rotating the operand integer's bits by the amount specified by the shiftCount parameter.
        /// </returns>
        /// <remarks>
        /// The shift count is given by the low-order four bits of the shiftCount parameter.
        /// That is, the actual shift count is 0 to 15 bits.
        /// </remarks>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static UInt16 RotateLeft(this UInt16 operand, int shiftCount)
        {
            shiftCount &= 0x0f;

            return (UInt16) (
                (operand << shiftCount) | 
                (operand >> (16 - shiftCount)));
        }

        /// <summary>
        /// Rotate bits of integer left by specified amount.
        /// </summary>
        /// <param name="operand">32-bit integer that will have its bits rotated left.</param>
        /// <param name="shiftCount">Number of bits to shift the integer by.</param>
        /// <returns>
        /// Resulting 32-bit integer after rotating the operand integer's bits by the amount specified by the shiftCount parameter.
        /// </returns>
        /// <remarks>
        /// The shift count is given by the low-order five bits of the shiftCount parameter.
        /// That is, the actual shift count is 0 to 31 bits.
        /// </remarks>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static UInt32 RotateLeft(this UInt32 operand, int shiftCount)
        {
            shiftCount &= 0x1f;

            return 
                (operand << shiftCount) | 
                (operand >> (32 - shiftCount));
        }

        /// <summary>
        /// Rotate bits of integer left by specified amount.
        /// </summary>
        /// <param name="operand">64-bit integer that will have its bits rotated left.</param>
        /// <param name="shiftCount">Number of bits to shift the integer by.</param>
        /// <returns>
        /// Resulting 64-bit integer after rotating the operand integer's bits by the amount specified by the shiftCount parameter.
        /// </returns>
        /// <remarks>
        /// The shift count is given by the low-order six bits of the shiftCount parameter.
        /// That is, the actual shift count is 0 to 63 bits.
        /// </remarks>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static UInt64 RotateLeft(this UInt64 operand, int shiftCount)
        {
            shiftCount &= 0x3f;

            return
                (operand << shiftCount) |
                (operand >> (64 - shiftCount));
        }

    }
}
