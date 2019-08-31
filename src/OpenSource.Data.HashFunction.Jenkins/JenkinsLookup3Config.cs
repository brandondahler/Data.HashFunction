using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.Jenkins
{
    /// <summary>
    /// Defines a configuration for a <see cref="IJenkinsLookup2"/> implementation.
    /// </summary>
    public class JenkinsLookup3Config
        : IJenkinsLookup3Config
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
        /// Gets the second seed.
        /// </summary>
        /// <value>
        /// The second seed.
        /// </value>
        public UInt32 Seed2 { get; set; } = 0U;



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        public IJenkinsLookup3Config Clone() =>
            new JenkinsLookup3Config() {
                HashSizeInBits = HashSizeInBits,
                Seed = Seed,
                Seed2 = Seed2
            };
    }
}
