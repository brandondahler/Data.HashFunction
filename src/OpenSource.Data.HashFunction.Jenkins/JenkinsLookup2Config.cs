using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.Jenkins
{
    /// <summary>
    /// Defines a configuration for a <see cref="IJenkinsLookup2"/> implementation.
    /// </summary>
    public class JenkinsLookup2Config
        : IJenkinsLookup2Config
    {
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
        public IJenkinsLookup2Config Clone() => 
            new JenkinsLookup2Config() {
                Seed = Seed
            };
    }
}
