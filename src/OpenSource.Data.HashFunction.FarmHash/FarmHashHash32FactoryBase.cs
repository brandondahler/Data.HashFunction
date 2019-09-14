using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Base implementation to provide instances of implementations of <typeparamref name="TFarmHashHash32"/>.
    /// </summary>
    public abstract class FarmHashHash32FactoryBase<TFarmHashHash32>
        : FarmHash32FactoryBase<TFarmHashHash32>,
            IFarmHashHash32Factory
        where TFarmHashHash32 : IFarmHashHash32
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashHash"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashHash"/> instance.</returns>
        IFarmHashHash IFarmHashHashFactory.Create() => Create();

        /// <summary>
        /// Creates a new <see cref="IFarmHashHash32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashHash32"/> instance.</returns>
        IFarmHashHash32 IFarmHashHash32Factory.Create() => Create();
    }
}
