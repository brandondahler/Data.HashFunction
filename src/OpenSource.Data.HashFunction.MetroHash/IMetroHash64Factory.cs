using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.MetroHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IMetroHash64"/>.
    /// </summary>
    public interface IMetroHash64Factory
        : IMetroHashFactory
    {
        /// <summary>
        /// Creates a new <see cref="IMetroHash64"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IMetroHash64"/> instance.</returns>
        new IMetroHash64 Create();


        /// <summary>
        /// Creates a new <see cref="IMetroHash64"/> instance with given configuration.
        /// </summary>
        /// <param name="config">The configuration to use.</param>
        /// <returns>
        /// A <see cref="IMetroHash64" /> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="config"/></exception>
        new IMetroHash64 Create(IMetroHashConfig config);
    }
}
