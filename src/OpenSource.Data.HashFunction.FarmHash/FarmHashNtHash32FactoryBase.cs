using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Base implementation to provide instances of implementations of <typeparamref name="TFarmHashNtHash32"/>.
    /// </summary>
    public abstract class FarmHashNtHash32FactoryBase<TFarmHashNtHash32>
        : FarmHashHash32FactoryBase<TFarmHashNtHash32>,
            IFarmHashNtHash32Factory
        where TFarmHashNtHash32 : IFarmHashNtHash32
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashNtHash32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashNtHash32"/> instance.</returns>
        IFarmHashNtHash32 IFarmHashNtHash32Factory.Create() => Create();
    }
}
