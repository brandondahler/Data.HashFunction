using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashSuHash32"/>.
    /// </summary>
    public interface IFarmHashSuHash32Factory
        : IFarmHashHash32Factory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashSuHash32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashSuHash32"/> instance.</returns>
        new IFarmHashSuHash32 Create();
    }
}
