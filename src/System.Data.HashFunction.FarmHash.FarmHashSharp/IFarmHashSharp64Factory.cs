using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash.FarmHashSharp
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashSharp64"/>.
    /// </summary>
    public interface IFarmHashSharp64Factory
        : IFarmHash64Factory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashSharp64"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashSharp64"/> instance.</returns>
        new IFarmHashSharp64 Create();
    }
}
