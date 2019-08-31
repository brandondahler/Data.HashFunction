using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Base implementation to provide instances of implementations of <typeparamref name="TFarmHashUoHash64"/>.
    /// </summary>
    public abstract class FarmHashUoHash64FactoryBase<TFarmHashUoHash64>
        : FarmHashHash64FactoryBase<TFarmHashUoHash64>,
            IFarmHashUoHash64Factory
        where TFarmHashUoHash64 : IFarmHashUoHash64
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashUoHash64"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashUoHash64"/> instance.</returns>
        IFarmHashUoHash64 IFarmHashUoHash64Factory.Create() => Create();
    }
}
