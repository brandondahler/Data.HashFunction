using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHash64"/>.
    /// </summary>
    public interface IFarmHash64Factory
        : IFarmHashFactory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHash64"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHash64"/> instance.</returns>
        new IFarmHash64 Create();
    }
}
