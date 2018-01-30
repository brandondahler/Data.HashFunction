using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace System.Data.HashFunction.FNV
{
    /// <summary>
    /// Defines a configuration for a <see cref="IFNV"/> implementation.
    /// </summary>
    public interface IFNVConfig
    {
        /// <summary>
        /// Length of the produced hash, in bits.
        /// </summary>
        /// <value>
        /// The length of the produced hash, in bits
        /// </value>
        int HashSizeInBits { get; }

        /// <summary>
        /// The prime integer to use when calculating the FNV value.
        /// </summary>
        /// <value>
        /// The prime value.
        /// </value>
        BigInteger Prime { get; }

        /// <summary>
        /// The offset integer to use when calculating the FNV value.
        /// </summary>
        /// <value>
        /// The offset value.
        /// </value>
        BigInteger Offset { get; }



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        IFNVConfig Clone();
    }
}
