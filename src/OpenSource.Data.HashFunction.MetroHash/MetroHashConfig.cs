using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.MetroHash
{
    /// <summary>
    /// Defines a configuration for a MetroHash implementation.
    /// </summary>
    public class MetroHashConfig
        : IMetroHashConfig
    {
        /// <summary>
        /// Gets the seed value.
        /// </summary>
        /// <value>
        /// The seed value.
        /// </value>
        public UInt64 Seed { get; set; } = 0;


        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        public IMetroHashConfig Clone() => 
            new MetroHashConfig() {
                Seed = Seed
            };
    }
}
