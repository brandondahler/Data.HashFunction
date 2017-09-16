using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.CityHash
{
    /// <summary>
    /// Defines a configuration for a <see cref="ICityHash"/> implementation.
    /// </summary>
    /// <seealso cref="ICityHashConfig" />
    public class CityHashConfig
        : ICityHashConfig
    {
        /// <summary>
        /// Gets the desired hash size, in bits.
        /// </summary>
        /// <value>
        /// The desired hash size, in bits.
        /// </value>
        /// <remarks>Defaults to <c>64</c>.</remarks>
        public int HashSizeInBits { get; set; } = 32;
    }
}
