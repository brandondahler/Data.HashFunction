using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.MetroHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IMetroHash64"/>.
    /// </summary>
    public sealed class MetroHash64Factory
        : IMetroHash64Factory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IMetroHash64Factory Instance { get; } = new MetroHash64Factory();


        private MetroHash64Factory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IMetroHash64"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IMetroHash64"/> instance.</returns>
        public IMetroHash64 Create() => Create(new MetroHashConfig());


        /// <summary>
        /// Creates a new <see cref="IMetroHash64"/> instance with given configuration.
        /// </summary>
        /// <param name="config">The configuration to use.</param>
        /// <returns>
        /// A <see cref="IMetroHash64" /> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="config"/></exception>
        public IMetroHash64 Create(IMetroHashConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new MetroHash64_Implementation(config);
        }
    }
}
