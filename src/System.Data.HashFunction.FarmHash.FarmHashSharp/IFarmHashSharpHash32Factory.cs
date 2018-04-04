using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash.FarmHashSharp
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashSharpHash32"/>.
    /// </summary>
    public interface IFarmHashSharpHash32Factory
        : IFarmHashFingerprint32Factory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashSharpHash32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashSharpHash32"/> instance.</returns>
        new IFarmHashSharpHash32 Create();
    }
}
