using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.BernsteinHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IBernsteinHash"/>.
    /// </summary>
    public interface IBernsteinHashFactory
    {
        /// <summary>
        /// Creates a new <see cref="IBernsteinHash"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IBernsteinHash"/> instance.</returns>
        IBernsteinHash Create();
    }
}
