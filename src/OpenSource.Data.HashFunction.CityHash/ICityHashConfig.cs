using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.CityHash
{
    /// <summary>
    /// Defines a configuration for a <see cref="ICityHash"/> implementation.
    /// </summary>
    public interface ICityHashConfig
    {
        /// <summary>
        /// Gets the desired hash size, in bits.
        /// </summary>
        /// <value>
        /// The desired hash size, in bits.
        /// </value>
        int HashSizeInBits { get; }


        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        ICityHashConfig Clone();
    }
}
