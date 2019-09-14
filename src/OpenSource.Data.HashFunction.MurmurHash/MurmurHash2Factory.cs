using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.MurmurHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IMurmurHash2"/>.
    /// </summary>
    public class MurmurHash2Factory
        : IMurmurHash2Factory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IMurmurHash2Factory Instance { get; } = new MurmurHash2Factory();


        private MurmurHash2Factory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IMurmurHash2"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IMurmurHash2"/> instance.</returns>
        public IMurmurHash2 Create() =>
            Create(new MurmurHash2Config());

        /// <summary>
        /// Creates a new <see cref="IMurmurHash2"/> instance with the specified configuration.
        /// </summary>
        /// <param name="config">Configuration to use when constructing the instance.</param>
        /// <returns>A <see cref="IMurmurHash2"/> instance.</returns>
        public IMurmurHash2 Create(IMurmurHash2Config config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new MurmurHash2_Implementation(config);
        }

    }
}
