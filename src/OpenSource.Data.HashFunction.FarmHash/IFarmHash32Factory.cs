using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHash32"/>.
    /// </summary>
    public interface IFarmHash32Factory
        : IFarmHashFactory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHash32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHash32"/> instance.</returns>
        new IFarmHash32 Create();
    }
}
