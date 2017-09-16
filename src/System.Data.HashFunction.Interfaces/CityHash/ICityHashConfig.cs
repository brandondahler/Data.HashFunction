using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.CityHash
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
    }
}
