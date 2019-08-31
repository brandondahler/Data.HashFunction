using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.Jenkins
{
    /// <summary>
    /// Defines a configuration for a <see cref="IJenkinsLookup2"/> implementation.
    /// </summary>
    public interface IJenkinsLookup2Config
    {

        /// <summary>
        /// Gets the seed.
        /// </summary>
        /// <value>
        /// The seed.
        /// </value>
        UInt32 Seed { get; }



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        IJenkinsLookup2Config Clone();
    }
}
