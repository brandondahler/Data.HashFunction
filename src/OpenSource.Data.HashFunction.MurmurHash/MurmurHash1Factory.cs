using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.MurmurHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IMurmurHash1"/>.
    /// </summary>
    public class MurmurHash1Factory
        : IMurmurHash1Factory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IMurmurHash1Factory Instance { get; } = new MurmurHash1Factory();


        private MurmurHash1Factory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IMurmurHash1"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IMurmurHash1"/> instance.</returns>
        public IMurmurHash1 Create() =>
            Create(new MurmurHash1Config());

        /// <summary>
        /// Creates a new <see cref="IMurmurHash1"/> instance with the specified configuration.
        /// </summary>
        /// <param name="config">Configuration to use when constructing the instance.</param>
        /// <returns>A <see cref="IMurmurHash1"/> instance.</returns>
        public IMurmurHash1 Create(IMurmurHash1Config config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new MurmurHash1_Implementation(config);
        }
    }
}
