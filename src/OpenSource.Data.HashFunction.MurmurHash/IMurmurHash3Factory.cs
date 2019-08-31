using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.MurmurHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IMurmurHash3"/>.
    /// </summary>
    public interface IMurmurHash3Factory
    {
        /// <summary>
        /// Creates a new <see cref="IMurmurHash3"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IMurmurHash3"/> instance.</returns>
        IMurmurHash3 Create();

        /// <summary>
        /// Creates a new <see cref="IMurmurHash3"/> instance with the specified configuration.
        /// </summary>
        /// <param name="config">Configuration to use when constructing the instance.</param>
        /// <returns>A <see cref="IMurmurHash3"/> instance.</returns>
        IMurmurHash3 Create(IMurmurHash3Config config);
    }
}
