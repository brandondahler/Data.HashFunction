using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashHash32"/>.
    /// </summary>
    public interface IFarmHashHashFactory
        : IFarmHashFactory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashHash"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashHash"/> instance.</returns>
        new IFarmHashHash Create();
    }
}
