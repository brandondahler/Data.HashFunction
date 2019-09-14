using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashHash32"/>.
    /// </summary>
    public interface IFarmHashHash32Factory
        : IFarmHash32Factory,
            IFarmHashHashFactory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashHash32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashHash32"/> instance.</returns>
        new IFarmHashHash32 Create();
    }
}
