using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.MurmurHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IMurmurHash3"/>.
    /// </summary>
    public interface IMurmurHash2Factory
    {
        /// <summary>
        /// Creates a new <see cref="IMurmurHash2"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IMurmurHash2"/> instance.</returns>
        IMurmurHash2 Create();

        /// <summary>
        /// Creates a new <see cref="IMurmurHash2"/> instance with the specified configuration.
        /// </summary>
        /// <param name="config">Configuration to use when constructing the instance.</param>
        /// <returns>A <see cref="IMurmurHash2"/> instance.</returns>
        IMurmurHash2 Create(IMurmurHash2Config config);
    }
}
