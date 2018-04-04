using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Base implementation to provide instances of implementations of <typeparamref name="TFarmHashSaHash32"/>.
    /// </summary>
    public abstract class FarmHashSaHash32FactoryBase<TFarmHashSaHash32>
        : FarmHashHash32FactoryBase<TFarmHashSaHash32>,
            IFarmHashSaHash32Factory
        where TFarmHashSaHash32 : IFarmHashSaHash32
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashSaHash32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashSaHash32"/> instance.</returns>
        IFarmHashSaHash32 IFarmHashSaHash32Factory.Create() => Create();
    }
}
