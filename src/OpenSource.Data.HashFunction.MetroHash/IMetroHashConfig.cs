using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.MetroHash
{
    /// <summary>
    /// Defines a configuration for a <see cref="IMetroHash"/> implementation.
    /// </summary>
    public interface IMetroHashConfig
    {
        /// <summary>
        /// Gets the seed value.
        /// </summary>
        /// <value>
        /// The seed value.
        /// </value>
        UInt64 Seed { get; }

        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        IMetroHashConfig Clone();
    }
}
