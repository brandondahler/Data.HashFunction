using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.SpookyHash
{
    /// <summary>
    /// Defines a configuration for a <see cref="ISpookyHash"/> implementation.
    /// </summary>
    public interface ISpookyHashConfig
    {
        /// <summary>
        /// Gets the desired hash size, in bits.
        /// </summary>
        /// <value>
        /// The desired hash size, in bits.
        /// </value>
        int HashSizeInBits { get; }


        /// <summary>
        /// Gets the seed.
        /// </summary>
        /// <value>
        /// The seed.
        /// </value>
        UInt64 Seed { get; }


        /// <summary>
        /// Gets the second seed.
        /// </summary>
        /// <value>
        /// The second seed.
        /// </value>
        UInt64 Seed2 { get; }



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        ISpookyHashConfig Clone();
    }
}
