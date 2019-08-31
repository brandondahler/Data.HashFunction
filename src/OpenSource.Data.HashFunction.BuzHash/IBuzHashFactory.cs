using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.BuzHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IBuzHash"/>.
    /// </summary>
    public interface IBuzHashFactory
    {
        /// <summary>
        /// Creates a new <see cref="IBuzHash"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IBuzHash"/> instance.</returns>
        IBuzHash Create();


        /// <summary>
        /// Creates a new <see cref="IBuzHash"/> instance with given configuration.
        /// </summary>
        /// <param name="config">The configuration to use.</param>
        /// <returns>A <see cref="IBuzHash"/> instance.</returns>
        IBuzHash Create(IBuzHashConfig config);
    }
}
