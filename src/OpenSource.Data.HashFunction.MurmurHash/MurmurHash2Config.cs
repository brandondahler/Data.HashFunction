using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.MurmurHash
{
    /// <summary>
    /// Defines a configuration for a <see cref="IMurmurHash2"/> implementation.
    /// </summary>
    public class MurmurHash2Config
        : IMurmurHash2Config
    {
        /// <summary>
        /// Gets the desired hash size, in bits.
        /// </summary>
        /// <value>
        /// The desired hash size, in bits.
        /// </value>
        public int HashSizeInBits { get; set; } = 64;

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
        public IMurmurHash2Config Clone() => 
            new MurmurHash2Config() {
                HashSizeInBits = HashSizeInBits,
                Seed = Seed
            };
    }
}
