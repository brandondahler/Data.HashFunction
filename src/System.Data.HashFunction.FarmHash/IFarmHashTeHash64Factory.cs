using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashTeHash64"/>.
    /// </summary>
    public interface IFarmHashTeHash64Factory
        : IFarmHashHash64Factory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashTeHash64"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashTeHash64"/> instance.</returns>
        new IFarmHashTeHash64 Create();
    }
}
