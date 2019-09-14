using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.Pearson
{
    /// <summary>
    /// Defines a configuration for a <see cref="IPearson"/> implementation.
    /// </summary>
    public interface IPearsonConfig
    {
        /// <summary>
        /// A 256-length lookup table that is a defined permutation of [0, 255].
        /// </summary>
        /// <value>
        /// The table.
        /// </value>
        IReadOnlyList<byte> Table { get; }

        /// <summary>
        /// Gets the desired hash size, in bits.
        /// </summary>
        /// <value>
        /// The desired hash size, in bits.
        /// </value>
        int HashSizeInBits { get; }



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        IPearsonConfig Clone();
    }
}
