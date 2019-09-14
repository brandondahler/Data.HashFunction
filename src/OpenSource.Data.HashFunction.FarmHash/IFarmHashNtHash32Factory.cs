using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashNtHash32"/>.
    /// </summary>
    public interface IFarmHashNtHash32Factory
        : IFarmHashHash32Factory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashNtHash32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashNtHash32"/> instance.</returns>
        new IFarmHashNtHash32 Create();
    }
}
