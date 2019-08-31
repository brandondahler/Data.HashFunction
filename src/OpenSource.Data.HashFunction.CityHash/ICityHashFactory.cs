using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.CityHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="ICityHash"/>.
    /// </summary>
    public interface ICityHashFactory
    {
        /// <summary>
        /// Creates a new <see cref="ICityHash"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="ICityHash"/> instance.</returns>
        ICityHash Create();


        /// <summary>
        /// Creates a new <see cref="ICityHash"/> instance with given configuration.
        /// </summary>
        /// <param name="config">The configuration to use.</param>
        /// <returns>
        /// A <see cref="ICityHash" /> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="config"/></exception>
        ICityHash Create(ICityHashConfig config);
    }
}
