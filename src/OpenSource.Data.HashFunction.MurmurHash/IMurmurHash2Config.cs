using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.MurmurHash
{
    /// <summary>
    /// Defines a configuration for a <see cref="IMurmurHash2"/> implementation.
    /// </summary>
    public interface IMurmurHash2Config
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
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        IMurmurHash2Config Clone();
    }
}
