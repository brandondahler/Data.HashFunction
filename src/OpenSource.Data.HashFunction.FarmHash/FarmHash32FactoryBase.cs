using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Base implementation to provide instances of implementations of <typeparamref name="TFarmHash32"/>.
    /// </summary>
    public abstract class FarmHash32FactoryBase<TFarmHash32>
        : FarmHashFactoryBase<TFarmHash32>,
            IFarmHash32Factory
        where TFarmHash32 : IFarmHash32
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHash32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHash32"/> instance.</returns>
        IFarmHash32 IFarmHash32Factory.Create() => Create();
    }
}
