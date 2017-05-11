using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Utilities.IntegerManipulation
{
    /// <summary>
    /// Static class to provide ReverseByteOrder extension functions.
    /// </summary>
    internal static class ReverseByteOrderExtensions
    {
        /// <summary>
        /// Reverses byte order of operand provided.
        /// </summary>
        /// <param name="operand">16-bit integer that will have its byte order reversed.</param>
        /// <returns>
        /// Resulting 16-bit integer after reversing the operand's byte order.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 ReverseByteOrder(this UInt16 operand)
        {
            return (UInt16) (
                (operand >> 8) | 
                (operand << 8));
        }

        /// <summary>
        /// Reverses byte order of operand provided.
        /// </summary>
        /// <param name="operand">32-bit integer that will have its byte order reversed.</param>
        /// <returns>
        /// Resulting 32-bit integer after reversing the parameter's byte order.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 ReverseByteOrder(this UInt32 operand)
        {
            return 
                ( operand >> 24) |
                ((operand & 0x00ff0000) >> 8) |
                ((operand & 0x0000ff00) << 8) |
                ( operand << 24);
        }

        /// <summary>
        /// Reverses byte order of operand provided.
        /// </summary>
        /// <param name="operand">64-bit integer that will have its byte order reversed.</param>
        /// <returns>
        /// Resulting 64-bit integer after reversing the parameter's byte order.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ReverseByteOrder(this UInt64 operand)
        {
            return 
                ( operand >> 56) |
                ((operand & 0x00ff000000000000) >> 40) |
                ((operand & 0x0000ff0000000000) >> 24) |
                ((operand & 0x000000ff00000000) >> 8) |
                ((operand & 0x00000000ff000000) << 8) |
                ((operand & 0x0000000000ff0000) << 24) |
                ((operand & 0x000000000000ff00) << 40) |
                ( operand << 56);
        }
    }
}
