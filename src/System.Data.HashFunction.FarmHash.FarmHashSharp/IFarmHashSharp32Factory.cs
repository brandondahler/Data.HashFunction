using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash.FarmHashSharp
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashSharp32"/>.
    /// </summary>
    public interface IFarmHashSharp32Factory
        : IFarmHash32Factory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashSharp32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashSharp32"/> instance.</returns>
        new IFarmHashSharp32 Create();
    }
}
