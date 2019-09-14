using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenSource.Data.HashFunction.Pearson
{
    /// <summary>
    /// Defines a configuration for a <see cref="IPearson"/> implementation.
    /// </summary>
    public class PearsonConfig
        : IPearsonConfig
    {
        /// <summary>
        /// A 256-length lookup table of a permutation of [0, 255].
        /// </summary>
        /// <value>
        /// The table.
        /// </value>
        public IReadOnlyList<byte> Table { get; set; } = null;

        /// <summary>
        /// Gets the desired hash size, in bits.
        /// </summary>
        /// <value>
        /// The desired hash size, in bits.
        /// </value>
        public int HashSizeInBits { get; set; } = 8;



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        public IPearsonConfig Clone() => 
            new PearsonConfig() {
                Table = Table?.ToArray(),
                HashSizeInBits = HashSizeInBits
            };
    }
}
