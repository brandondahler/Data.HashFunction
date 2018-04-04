using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Base implementation to provide instances of implementations of <typeparamref name="TFarmHash"/>.
    /// </summary>
    public abstract class FarmHashFactoryBase<TFarmHash>
        : IFarmHashFactory
        where TFarmHash : IFarmHash
    {

        /// <summary>
        /// Creates a new <typeparamref name="TFarmHash"/> instance.
        /// </summary>
        /// <returns>A <typeparamref name="TFarmHash"/> instance.</returns>
        public abstract TFarmHash Create();

        /// <summary>
        /// Creates a new <see cref="IFarmHash"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHash"/> instance.</returns>
        IFarmHash IFarmHashFactory.Create() => Create();
    }
}
