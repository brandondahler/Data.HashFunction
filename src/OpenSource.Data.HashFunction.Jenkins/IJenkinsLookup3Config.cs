using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.Jenkins
{
    /// <summary>
    /// Defines a configuration for a <see cref="IJenkinsLookup3"/> implementation.
    /// </summary>
    public interface IJenkinsLookup3Config
    {

        /// <summary>
        /// Gets the desired hash size, in bits.
        /// </summary>
        /// <value>
        /// The desired hash size, in bits.
        /// </value>
        int HashSizeInBits { get; }


        /// <summary>
        /// Gets the seed.
        /// </summary>
        /// <value>
        /// The seed.
        /// </value>
        UInt32 Seed { get; }

        /// <summary>
        /// Gets the second seed.
        /// </summary>
        /// <value>
        /// The second seed.
        /// </value>
        UInt32 Seed2 { get; }



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        IJenkinsLookup3Config Clone();
    }
}
