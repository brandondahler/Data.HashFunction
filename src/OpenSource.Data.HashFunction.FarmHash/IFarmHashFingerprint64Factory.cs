using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashFingerprint64"/>.
    /// </summary>
    public interface IFarmHashFingerprint64Factory
        : IFarmHash64Factory,  
            IFarmHashFingerprintFactory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashFingerprint64"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashFingerprint64"/> instance.</returns>
        new IFarmHashFingerprint64 Create();
    }
}
