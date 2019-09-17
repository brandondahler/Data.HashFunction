using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.CityHash
{
    /// <summary>
    /// Defines a configuration for a <see cref="ICityHash"/> implementation.
    /// </summary>
    public class CityHashConfig
        : ICityHashConfig
    {
        /// <summary>
        /// Gets the desired hash size, in bits.
        /// </summary>
        /// <value>
        /// The desired hash size, in bits.
        /// </value>
        /// <remarks>Defaults to <c>32</c>.</remarks>
        public int HashSizeInBits { get; set; } = 32;



        /// <summary>
        /// Makes a deep clone of current instance.
        /// </summary>
        /// <returns>A deep clone of the current instance.</returns>
        public ICityHashConfig Clone() =>
            new CityHashConfig() {
                HashSizeInBits = HashSizeInBits
            };
    }
}
