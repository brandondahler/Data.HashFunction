using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.xxHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IxxHash"/>.
    /// </summary>
    public interface IxxHashFactory
    {
        /// <summary>
        /// Creates a new <see cref="IxxHash"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IxxHash"/> instance.</returns>
        IxxHash Create();

        /// <summary>
        /// Creates a new <see cref="IxxHash"/> instance with the specified configuration.
        /// </summary>
        /// <param name="config">Configuration to use when constructing the instance.</param>
        /// <returns>A <see cref="IxxHash"/> instance.</returns>
        IxxHash Create(IxxHashConfig config);
    }
}
