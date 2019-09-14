using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashFingerprint128"/>.
    /// </summary>
    public interface IFarmHashFingerprint128Factory
        : IFarmHash128Factory,
            IFarmHashFingerprintFactory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashFingerprint128"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashFingerprint128"/> instance.</returns>
        new IFarmHashFingerprint128 Create();
    }
}
