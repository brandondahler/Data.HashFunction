using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Base implementation to provide instances of implementations of <typeparamref name="TFarmHash64"/>.
    /// </summary>
    public abstract class FarmHash64FactoryBase<TFarmHash64>
        : FarmHashFactoryBase<TFarmHash64>,
            IFarmHash64Factory
        where TFarmHash64 : IFarmHash64
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHash64"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHash64"/> instance.</returns>
        IFarmHash64 IFarmHash64Factory.Create() => Create();
    }
}
