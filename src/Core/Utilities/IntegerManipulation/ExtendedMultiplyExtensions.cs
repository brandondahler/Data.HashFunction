using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Utilities.IntegerManipulation
{
    /// <summary>
    /// Static class to provide ExtendedMultiply extension functions.
    /// </summary>
    internal static class ExtendedMultiplyExtensions
    {
        /// <summary>
        /// Multiplies operand1 by operand2 as if both operand1 and operand2 were single large integers.
        /// </summary>
        /// <param name="operand1">Array of UInt32 values to be multiplied.</param>
        /// <param name="operand2">Array of UInt32 values to multiply by.</param>
        /// <returns></returns>
#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32[] ExtendedMultiply(this IReadOnlyList<UInt32> operand1, IReadOnlyList<UInt32> operand2)
#else
        public static UInt32[] ExtendedMultiply(this IList<UInt32> operand1, IList<UInt32> operand2)
#endif
        {
            // Temporary array to hold the results of 32-bit multiplication.
            var product = new UInt32[(operand1.Count >= operand2.Count ? operand1.Count : operand2.Count)];

            // Bottom of equation
            for (int y = 0; y < operand2.Count; ++y)
            {
                // Skip multiplying things by zero
                if (operand2[y] == 0)
                    continue;

                UInt32 carryOver = 0;

                // Top of equation
                for (int x = 0; x < operand2.Count; ++x)
                {
                    if (x + y >= product.Length)
                        break;

                    var productResult = product[x + y] + (((UInt64) operand2[y]) * operand1[x]) + carryOver;
                    product[x + y] = (UInt32) productResult;

                    carryOver = (UInt32) (productResult >> 32);
                }
            }

            return product;
        }
    }
}
