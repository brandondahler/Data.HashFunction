using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.MetroHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IMetroHash128"/>.
    /// </summary>
    public sealed class MetroHash128Factory
        : IMetroHash128Factory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IMetroHash128Factory Instance { get; } = new MetroHash128Factory();


        private MetroHash128Factory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IMetroHash128"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IMetroHash128"/> instance.</returns>
        public IMetroHash128 Create() => Create(new MetroHashConfig());


        /// <summary>
        /// Creates a new <see cref="IMetroHash128"/> instance with given configuration.
        /// </summary>
        /// <param name="config">The configuration to use.</param>
        /// <returns>
        /// A <see cref="IMetroHash128" /> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="config"/></exception>
        public IMetroHash128 Create(IMetroHashConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new MetroHash128_Implementation(config);
        }
    }
}
