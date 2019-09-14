using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.SpookyHash
{
    /// <summary>
    /// Defines a configuration for a <see cref="ISpookyHash"/> implementation.
    /// </summary>
    public class SpookyHashConfig
        : ISpookyHashConfig
    {
        /// <summary>
        /// Gets the desired hash size, in bits.
        /// </summary>
        /// <value>
        /// The desired hash size, in bits.
        /// </value>
        public int HashSizeInBits { get; set; } = 128;


        /// <summary>
        /// Gets the seed.
        /// </summary>
        /// <value>
        /// The seed.
        /// </value>
        public UInt64 Seed { get; set; } = 0UL;

        /// <summary>
        /// Gets the second seed.
        /// </summary>
        /// <value>
        /// The second seed.
        /// </value>
        public UInt64 Seed2 { get; set; } = 0UL;



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        public ISpookyHashConfig Clone() =>
            new SpookyHashConfig() {
                HashSizeInBits = HashSizeInBits,
                Seed = Seed,
                Seed2 = Seed2
            };
    }
}
