using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Base implementation to provide instances of implementations of <typeparamref name="TFarmHashHash128"/>.
    /// </summary>
    public abstract class FarmHashHash128FactoryBase<TFarmHashHash128>
        : FarmHashFingerprint128FactoryBase<TFarmHashHash128>,
            IFarmHashHash128Factory
        where TFarmHashHash128 : IFarmHashHash128
    {

        /// <summary>
        /// Creates a new <see cref="IFarmHashHash"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashHash"/> instance.</returns>
        IFarmHashHash IFarmHashHashFactory.Create() => Create();

        /// <summary>
        /// Creates a new <see cref="IFarmHashHash128"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashHash128"/> instance.</returns>
        IFarmHashHash128 IFarmHashHash128Factory.Create() => Create();
    }
}
