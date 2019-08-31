using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashCcHash32"/>.
    /// </summary>
    public interface IFarmHashCcHash32Factory
        : IFarmHashHash32Factory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashCcHash32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashCcHash32"/> instance.</returns>
        new IFarmHashCcHash32 Create();
    }
}
