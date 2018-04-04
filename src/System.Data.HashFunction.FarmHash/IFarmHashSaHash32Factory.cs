using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashSaHash32"/>.
    /// </summary>
    public interface IFarmHashSaHash32Factory
        : IFarmHashHash32Factory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashSaHash32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashSaHash32"/> instance.</returns>
        new IFarmHashSaHash32 Create();
    }
}
