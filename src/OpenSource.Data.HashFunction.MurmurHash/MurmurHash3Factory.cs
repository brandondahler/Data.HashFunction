using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.MurmurHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IMurmurHash3"/>.
    /// </summary>
    public class MurmurHash3Factory
        : IMurmurHash3Factory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IMurmurHash3Factory Instance { get; } = new MurmurHash3Factory();


        private MurmurHash3Factory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IMurmurHash3"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IMurmurHash3"/> instance.</returns>
        public IMurmurHash3 Create() =>
            Create(new MurmurHash3Config());

        /// <summary>
        /// Creates a new <see cref="IMurmurHash3"/> instance with the specified configuration.
        /// </summary>
        /// <param name="config">Configuration to use when constructing the instance.</param>
        /// <returns>A <see cref="IMurmurHash3"/> instance.</returns>
        public IMurmurHash3 Create(IMurmurHash3Config config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new MurmurHash3_Implementation(config);
        }
    }
}
