using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.MurmurHash
{
    /// <summary>
    /// Defines a configuration for a <see cref="IMurmurHash3"/> implementation.
    /// </summary>
    public class MurmurHash3Config
        : IMurmurHash3Config
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
        public UInt32 Seed { get; set; } = 0U;



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        public IMurmurHash3Config Clone() =>
            new MurmurHash3Config() { 
                HashSizeInBits = HashSizeInBits,
                Seed = Seed
            };
    }
}
