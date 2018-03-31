using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MetroHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IMetroHash128"/>.
    /// </summary>
    public interface IMetroHash128Factory
        : IMetroHashFactory
    {
        /// <summary>
        /// Creates a new <see cref="IMetroHash128"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IMetroHash128"/> instance.</returns>
        new IMetroHash128 Create();


        /// <summary>
        /// Creates a new <see cref="IMetroHash128"/> instance with given configuration.
        /// </summary>
        /// <param name="config">The configuration to use.</param>
        /// <returns>
        /// A <see cref="IMetroHash128" /> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="config"/></exception>
        new IMetroHash128 Create(IMetroHashConfig config);
    }
}
