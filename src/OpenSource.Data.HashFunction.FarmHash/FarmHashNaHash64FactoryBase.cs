using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Base implementation to provide instances of implementations of <typeparamref name="TFarmHashNaHash64"/>.
    /// </summary>
    public abstract class FarmHashNaHash64FactoryBase<TFarmHashNaHash64>
        : FarmHashHash64FactoryBase<TFarmHashNaHash64>,
            IFarmHashNaHash64Factory
        where TFarmHashNaHash64 : IFarmHashNaHash64
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashNaHash64"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashNaHash64"/> instance.</returns>
        IFarmHashNaHash64 IFarmHashNaHash64Factory.Create() => Create();
    }
}
