using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MurmurHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IMurmurHash1"/>.
    /// </summary>
    public interface IMurmurHash1Factory
    {
        /// <summary>
        /// Creates a new <see cref="IMurmurHash1"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IMurmurHash1"/> instance.</returns>
        IMurmurHash1 Create();

        /// <summary>
        /// Creates a new <see cref="IMurmurHash1"/> instance with the specified configuration.
        /// </summary>
        /// <param name="config">Configuration to use when constructing the instance.</param>
        /// <returns>A <see cref="IMurmurHash1"/> instance.</returns>
        IMurmurHash1 Create(IMurmurHash1Config config);
    }
}
