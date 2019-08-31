using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Base implementation to provide instances of implementations of <typeparamref name="TFarmHashTeHash64"/>.
    /// </summary>
    public abstract class FarmHashTeHash64FactoryBase<TFarmHashTeHash64>
        : FarmHashHash64FactoryBase<TFarmHashTeHash64>,
            IFarmHashTeHash64Factory
        where TFarmHashTeHash64 : IFarmHashTeHash64
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashTeHash64"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashTeHash64"/> instance.</returns>
        IFarmHashTeHash64 IFarmHashTeHash64Factory.Create() => Create();
    }
}
