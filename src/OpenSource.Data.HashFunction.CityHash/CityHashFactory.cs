using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.CityHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="ICityHash"/>.
    /// </summary>
    public sealed class CityHashFactory
        : ICityHashFactory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static ICityHashFactory Instance { get; } = new CityHashFactory();


        private CityHashFactory()
        {

        }

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
        /// <exception cref="ArgumentNullException"><paramref name="config"/></exception>
        public ICityHash Create(ICityHashConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new CityHash_Implementation(config);
        }
    }
}
