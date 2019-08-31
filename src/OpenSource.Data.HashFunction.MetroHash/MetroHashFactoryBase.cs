using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.MetroHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IMetroHash"/> or <typeparamref name="TMetroHash"/>.
    /// </summary>
    public abstract class MetroHashFactoryBase<TMetroHash>
        : IMetroHashFactory
        where TMetroHash : IMetroHash
    {
        /// <summary>
        /// Creates a new <typeparamref name="TMetroHash"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <typeparamref name="TMetroHash"/> instance.</returns>
        public abstract TMetroHash Create();


        /// <summary>
        /// Creates a new <typeparamref name="TMetroHash"/> instance with given configuration.
        /// </summary>
        /// <param name="config">The configuration to use.</param>
        /// <returns>
        /// A <see cref="IMetroHash" /> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="config"/></exception>
        public abstract TMetroHash Create(IMetroHashConfig config);



        /// <summary>
        /// Creates a new <see cref="IMetroHash"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IMetroHash"/> instance.</returns>
        IMetroHash IMetroHashFactory.Create() => Create();


        /// <summary>
        /// Creates a new <see cref="IMetroHash"/> instance with given configuration.
        /// </summary>
        /// <param name="config">The configuration to use.</param>
        /// <returns>
        /// A <see cref="IMetroHash" /> instance.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="config"/></exception>
        IMetroHash IMetroHashFactory.Create(IMetroHashConfig config) => Create(config);
    }
}
