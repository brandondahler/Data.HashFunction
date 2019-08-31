using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashHash64"/>.
    /// </summary>
    public interface IFarmHashHash64Factory
        : IFarmHash64Factory,
            IFarmHashHashFactory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashHash64"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashHash64"/> instance.</returns>
        new IFarmHashHash64 Create();
    }
}
