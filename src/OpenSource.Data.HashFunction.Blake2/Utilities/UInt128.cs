using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Blake2.Utilities
{

    /// <summary>Structure to store 128-bit integer as two 64-bit integers.</summary>
    internal struct UInt128
    {
        /// <summary>Low-order 64-bits.</summary>
        public UInt64 Low { get; }

        /// <summary>High-order 64-bits.</summary>
        public UInt64 High { get; }

        
        public UInt128(UInt64 low)
            : this(low, 0)
        {

        }

        public UInt128(UInt64 low, UInt64 high)
        {
            Low = low;
            High = high;
        }


        #region Static Operators
        
        /// <summary>
        /// Implements the add operator.
        /// </summary>
        /// <param name="a">The instance to add to.</param>
        /// <param name="b">The instance to add.</param>
        /// <returns>A new instance representing the first parameter plus the second parameter.</returns>
        public static UInt128 operator +(UInt128 a, UInt128 b)
        {
            var carryOver = 0UL;
            var lowResult = unchecked(a.Low + b.Low);

            if (lowResult < a.Low)
                carryOver = 1UL;


            return new UInt128(lowResult, a.High + b.High + carryOver);
        }
        
        #endregion

    }
}
