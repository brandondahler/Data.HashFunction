using System;
using System.Collections.Generic;

namespace OpenSource.Data.HashFunction.BuzHash
{
    /// <summary>
    /// Defines a configuration for a <see cref="IBuzHash"/> implementation.
    /// </summary>
    public interface IBuzHashConfig
    {
        /// <summary>
        /// Gets a list of <c>256</c> (preferably random and distinct) <see cref="UInt64"/> values.
        /// </summary>
        /// <value>
        /// List of 256 <see cref="UInt64"/> values.
        /// </value>
        IReadOnlyList<UInt64> Rtab { get; }


        /// <summary>
        /// Gets the desired hash size, in bits.
        /// 
        /// Implementations are expected to support sizes of <c>8</c>, <c>16</c>, <c>32</c>, and <c>64</c>.
        /// </summary>
        /// <value>
        /// The desired hash size, in bits.
        /// </value>
        int HashSizeInBits { get; }


        /// <summary>
        /// Gets the seed value.
        /// </summary>
        /// <value>
        /// The seed value.
        /// </value>
        /// <remarks>
        /// Only the bottom <see cref="HashSizeInBits"/> bits shoudl be used for a given configuration.
        /// </remarks>
        UInt64 Seed { get; }

        /// <summary>
        /// Gets the shift direction.
        /// </summary>
        /// <value>
        /// The shift direction.
        /// </value>
        CircularShiftDirection ShiftDirection { get; }



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        IBuzHashConfig Clone();
    }
}