using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Base implementation to provide instances of implementations of <typeparamref name="TFarmHashSuHash32"/>.
    /// </summary>
    public abstract class FarmHashSuHash32FactoryBase<TFarmHashSuHash32>
        : FarmHashHash32FactoryBase<TFarmHashSuHash32>,
            IFarmHashSuHash32Factory
        where TFarmHashSuHash32 : IFarmHashSuHash32
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashSuHash32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashSuHash32"/> instance.</returns>
        IFarmHashSuHash32 IFarmHashSuHash32Factory.Create() => Create();
    }
}
