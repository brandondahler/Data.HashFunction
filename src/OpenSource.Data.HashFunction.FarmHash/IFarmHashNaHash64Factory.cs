using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashNaHash64"/>.
    /// </summary>
    public interface IFarmHashNaHash64Factory
        : IFarmHashHash64Factory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashNaHash64"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashNaHash64"/> instance.</returns>
        new IFarmHashNaHash64 Create();
    }
}
