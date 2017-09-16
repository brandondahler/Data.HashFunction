using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.CityHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="ICityHash"/>.
    /// </summary>
    /// <seealso cref="ICityHashFactory" />
    public class CityHashFactory
        : ICityHashFactory
    {
        /// <summary>
        /// Creates a new <see cref="ICityHash" /> instance with the default configuration.
        /// </summary>
        /// <returns>
        /// A <see cref="ICityHash" /> instance.
        /// </returns>
        public ICityHash Create()
        {
            return Create(new CityHashConfig());
        }

        /// <summary>
        /// Creates a new <see cref="ICityHash" /> instance with given configuration.
        /// </summary>
        /// <param name="config">The configuration to use.</param>
        /// <returns>
        /// A <see cref="ICityHash" /> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">config</exception>
        public ICityHash Create(ICityHashConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new CityHash_Implementation(config.HashSizeInBits);
        }
    }
}
