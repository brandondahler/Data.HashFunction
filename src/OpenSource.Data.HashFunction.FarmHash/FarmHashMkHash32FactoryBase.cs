using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Base implementation to provide instances of implementations of <typeparamref name="TFarmHashMkHash32"/>.
    /// </summary>
    public abstract class FarmHashMkHash32FactoryBase<TFarmHashMkHash32>
        : FarmHashHash32FactoryBase<TFarmHashMkHash32>,
            IFarmHashMkHash32Factory
        where TFarmHashMkHash32 : IFarmHashMkHash32
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashMkHash32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashMkHash32"/> instance.</returns>
        IFarmHashMkHash32 IFarmHashMkHash32Factory.Create() => Create();
    }
}
