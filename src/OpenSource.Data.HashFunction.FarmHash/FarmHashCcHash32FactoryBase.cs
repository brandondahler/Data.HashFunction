using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Base implementation to provide instances of implementations of <typeparamref name="TFarmHashCcHash32"/>.
    /// </summary>
    public abstract class FarmHashCcHash32FactoryBase<TFarmHashCcHash32>
        : FarmHashHash32FactoryBase<TFarmHashCcHash32>,
            IFarmHashCcHash32Factory
        where TFarmHashCcHash32 : IFarmHashCcHash32
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashCcHash32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashCcHash32"/> instance.</returns>
        IFarmHashCcHash32 IFarmHashCcHash32Factory.Create() => Create();
    }
}
