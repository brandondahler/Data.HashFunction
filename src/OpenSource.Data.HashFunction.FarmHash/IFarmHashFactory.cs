using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHash"/>.
    /// </summary>
    public interface IFarmHashFactory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHash"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHash"/> instance.</returns>
        IFarmHash Create();
    }
}
