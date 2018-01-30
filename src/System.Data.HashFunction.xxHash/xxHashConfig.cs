using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.xxHash
{
    /// <summary>
    /// Defines a configuration for a <see cref="IxxHash"/> implementation.
    /// </summary>
    public class xxHashConfig
        : IxxHashConfig
    {
        /// <summary>
        /// Gets the desired hash size, in bits.
        /// </summary>
        /// <value>
        /// The desired hash size, in bits.
        /// </value>
        public int HashSizeInBits { get; set; } = 32;

        /// <summary>
        /// Gets the seed.
        /// </summary>
        /// <value>
        /// The seed.
        /// </value>
        public UInt64 Seed { get; set; } = 0UL;



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        public IxxHashConfig Clone() =>
            new xxHashConfig() {
                HashSizeInBits = HashSizeInBits,
                Seed = Seed
            };
    }
}
