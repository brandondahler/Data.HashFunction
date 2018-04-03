using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHash128"/>.
    /// </summary>
    public interface IFarmHash128Factory
        : IFarmHashFactory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHash128"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHash128"/> instance.</returns>
        new IFarmHash128 Create();
    }
}
