using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Base implementation to provide instances of implementations of <typeparamref name="TFarmHash128"/>.
    /// </summary>
    public abstract class FarmHash128FactoryBase<TFarmHash128>
        : FarmHashFactoryBase<TFarmHash128>,
            IFarmHash128Factory
        where TFarmHash128 : IFarmHash128
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHash128"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHash128"/> instance.</returns>
        IFarmHash128 IFarmHash128Factory.Create() => Create();
    }
}
