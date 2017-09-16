using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.BernsteinHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IModifiedBernsteinHash"/>.
    /// </summary>
    public interface IModifiedBernsteinHashFactory
    {
        /// <summary>
        /// Creates a new <see cref="IModifiedBernsteinHash"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IModifiedBernsteinHash"/> instance.</returns>
        IModifiedBernsteinHash Create();
    }
}
