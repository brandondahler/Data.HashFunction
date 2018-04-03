using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashHash32"/>.
    /// </summary>
    public interface IFarmHashHash128Factory
        : IFarmHashFingerprint128Factory,
            IFarmHashHashFactory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashHash128"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashHash128"/> instance.</returns>
        new IFarmHashHash128 Create();
    }
}
