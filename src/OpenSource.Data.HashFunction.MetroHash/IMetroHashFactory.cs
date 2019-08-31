using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.MetroHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IMetroHash"/>.
    /// </summary>
    public interface IMetroHashFactory
    {
        /// <summary>
        /// Creates a new <see cref="IMetroHash"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IMetroHash"/> instance.</returns>
        IMetroHash Create();


        /// <summary>
        /// Creates a new <see cref="IMetroHash"/> instance with given configuration.
        /// </summary>
        /// <param name="config">The configuration to use.</param>
        /// <returns>
        /// A <see cref="IMetroHash" /> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="config"/></exception>
        IMetroHash Create(IMetroHashConfig config);
    }
}
