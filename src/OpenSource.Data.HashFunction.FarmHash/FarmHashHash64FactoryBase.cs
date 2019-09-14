using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Base implementation to provide instances of implementations of <typeparamref name="TFarmHashHash64"/>.
    /// </summary>
    public abstract class FarmHashHash64FactoryBase<TFarmHashHash64>
        : FarmHash64FactoryBase<TFarmHashHash64>,
            IFarmHashHash64Factory
        where TFarmHashHash64 : IFarmHashHash64
    {

        /// <summary>
        /// Creates a new <see cref="IFarmHashHash"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashHash"/> instance.</returns>
        IFarmHashHash IFarmHashHashFactory.Create() => Create();

        /// <summary>
        /// Creates a new <see cref="IFarmHashHash64"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashHash64"/> instance.</returns>
        IFarmHashHash64 IFarmHashHash64Factory.Create() => Create();
    }
}
