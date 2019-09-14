using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashMkHash32"/>.
    /// </summary>
    public interface IFarmHashMkHash32Factory
        : IFarmHashHash32Factory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashMkHash32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashMkHash32"/> instance.</returns>
        new IFarmHashMkHash32 Create();
    }
}
