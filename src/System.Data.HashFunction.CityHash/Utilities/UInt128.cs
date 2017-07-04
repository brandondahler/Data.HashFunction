using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Utilities
{
    internal class UInt128
        : IComparable,
            IComparable<UInt128>,
            IEquatable<UInt128>
    {
        internal static UInt128 Zero { get => new UInt128(0, 0); }
        internal static UInt128 One { get => new UInt128(1, 0); }


        /// <summary>Low-order 64-bits.</summary>
        public UInt64 Low { get; set; }

        /// <summary>High-order 64-bits.</summary>
        public UInt64 High { get; set; }


        internal UInt128()
            : this(0, 0)
        {

        }

        internal UInt128(UInt64 low, UInt64 high)
        {
            Low = low;
            High = high;
        }


        #region Object overrides

        /// <summary>Determines whether the specified <see cref="UInt128"/> is equal to the current <see cref="UInt128"/>.</summary>
        /// <param name="obj">The value to compare with the current value.</param>
        /// <returns>true if the specified value is equal to the current value; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is UInt128 && this == (UInt128)obj;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Low.GetHashCode() ^
                High.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{{ Low = {Low}, High = {High} }}";
        }

        #endregion


        #region IComparable

        /// <summary>
        /// Compares this instance to a specified object and returns an indication of their relative values.
        /// </summary>
        /// <param name="value">An object to compare, or null.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="value" />.
        /// 
        /// Return Value Description 
        /// Less than zero 
        /// This instance is less than <paramref name="value" />. 
        /// 
        /// Zero 
        /// This instance is equal to <paramref name="value" />. 
        /// 
        /// Greater than zero 
        /// This instance is greater than <paramref name="value" />.-or- <paramref name="value" /> is null.
        /// </returns>
        /// <exception cref="System.ArgumentException"><paramref name="value" /> is not a <see cref="UInt128" />.;value</exception>
        public int CompareTo(object value)
        {
            if (value == null)
                return 1;

            if (!(value is UInt128))
                throw new ArgumentException("value is not a UInt128.", nameof(value));

            return CompareTo((UInt128)value);
        }

        #endregion

        #region IComparable<UInt128>

        /// <summary>
        /// Compares this instance to a specified 128-bit unsigned integer and returns an indication of their relative values.
        /// </summary>
        /// <param name="value">An object to compare with this object.</param>
        /// <returns>
        /// A signed number indicating the relative values of this instance and <paramref name="value" />.
        ///
        /// Return Value Description 
        /// Less than zero 
        /// This instance is less than <paramref name="value" />. 
        /// 
        /// Zero 
        /// This instance is equal to <paramref name="value" />. 
        /// 
        /// Greater than zero 
        /// This instance is greater than <paramref name="value" />.
        /// </returns>
        public int CompareTo(UInt128 value)
        {
            if (value < this)
                return -1;

            if (value > this)
                return 1;

            return 0;
        }

        #endregion

        #region IEquatable<UInt128>

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(UInt128 other)
        {
            return this == other;
        }

        #endregion


        #region Static Operators

        /// <summary>
        /// Implements the increment operator.
        /// </summary>
        /// <param name="value">The instance to increment.</param>
        /// <returns>A new instance representing the value incremented by 1.</returns>
        public static UInt128 operator ++(UInt128 value)
        {
            return value + One;
        }

        /// <summary>
        /// Implements the increment operator.
        /// </summary>
        /// <param name="value">The instance to increment.</param>
        /// <returns>A new instance representing the value incremented by 1.</returns>
        public static UInt128 Increment(UInt128 value)
        {
            return ++value;
        }

        /// <summary>
        /// Implements the decrement operator.
        /// </summary>
        /// <param name="value">The instance to decrement.</param>
        /// <returns>A new instance representing the value decremented by 1.</returns>
        public static UInt128 operator --(UInt128 value)
        {
            return value - One;
        }

        /// <summary>
        /// Implements the decrement operator.
        /// </summary>
        /// <param name="value">The instance to decrement.</param>
        /// <returns>A new instance representing the value decremented by 1.</returns>
        public static UInt128 Decrement(UInt128 value)
        {
            return --value;
        }

        /// <summary>Determines whether the second <see cref="UInt128"/> is equal to the first <see cref="UInt128"/>.</summary>
        /// <param name="a">The first value to compare.</param>
        /// <param name="b">The second value to compare.</param>
        /// <returns>true if the specified value is equal to the current value; otherwise, false.</returns>
        public static bool operator ==(UInt128 a, UInt128 b)
        {
            return a.High == b.High && a.Low == b.Low;
        }

        /// <summary>Determines whether the second <see cref="UInt128"/> is not equal to the first <see cref="UInt128"/>.</summary>
        /// <param name="a">The first object to compare.</param>
        /// <param name="b">The second object to compare.</param>
        /// <returns>true if the specified value is not equal to the current value; otherwise, false.</returns>
        public static bool operator !=(UInt128 a, UInt128 b)
        {
            return !(a == b);
        }

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

        /// <summary>
        /// Implements the add operator.
        /// </summary>
        /// <param name="a">The instance to add to.</param>
        /// <param name="b">The instance to add.</param>
        /// <returns>A new instance representing the first parameter plus the second parameter.</returns>
        public static UInt128 Add(UInt128 a, UInt128 b)
        {
            return a + b;
        }

        /// <summary>
        /// Implements the subtraction operator.
        /// </summary>
        /// <param name="a">The instance to subtract from.</param>
        /// <param name="b">The instance to subtract.</param>
        /// <returns>A new instance representing the first parameter minus the second parameter.</returns>
        public static UInt128 operator -(UInt128 a, UInt128 b)
        {
            var borrow = 0UL;
            var lowResult = a.Low - b.Low;

            if (lowResult > a.Low)
                borrow = 1UL;


            return new UInt128(lowResult, a.High - b.High - borrow);
        }

        /// <summary>
        /// Implements the subtraction operator.
        /// </summary>
        /// <param name="a">The instance to subtract from.</param>
        /// <param name="b">The instance to subtract.</param>
        /// <returns>A new instance representing the first parameter minus the second parameter.</returns>
        public static UInt128 Subtract(UInt128 a, UInt128 b)
        {
            return a - b;
        }


        /// <summary>
        /// Implements the greater than operator.
        /// </summary>
        /// <param name="a">The instance to compare.</param>
        /// <param name="b">The instance to compare with.</param>
        /// <returns><c>true</c> if <paramref name="a"/> is greater than <paramref name="b"/>; otherwise <c>false</c>.</returns>
        public static bool operator >(UInt128 a, UInt128 b)
        {
            return a.High > b.High ||
                (a.High == b.High && a.Low > b.Low);
        }

        /// <summary>
        /// Implements the greater or equal to than operator.
        /// </summary>
        /// <param name="a">The instance to compare.</param>
        /// <param name="b">The instance to compare with.</param>
        /// <returns><c>true</c> if <paramref name="a"/> is greater than or equal to <paramref name="b"/>; otherwise <c>false</c>.</returns>
        public static bool operator >=(UInt128 a, UInt128 b)
        {
            return a.High > b.High ||
                (a.High == b.High && a.Low >= b.Low);
        }

        /// <summary>
        /// Implements the less than operator.
        /// </summary>
        /// <param name="a">The instance to compare.</param>
        /// <param name="b">The instance to compare with.</param>
        /// <returns><c>true</c> if <paramref name="a"/> is less than <paramref name="b"/>; otherwise <c>false</c>.</returns>
        public static bool operator <(UInt128 a, UInt128 b)
        {
            return !(a >= b);
        }

        /// <summary>
        /// Implements the less than or equal to operator.
        /// </summary>
        /// <param name="a">The instance to compare.</param>
        /// <param name="b">The instance to compare with.</param>
        /// <returns><c>true</c> if <paramref name="a"/> is less than or equal to <paramref name="b"/>; otherwise <c>false</c>.</returns>
        public static bool operator <=(UInt128 a, UInt128 b)
        {
            return !(a > b);
        }

        #endregion

    }
}
