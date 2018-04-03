using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashFingerprint"/>.
    /// </summary>
    public interface IFarmHashFingerprintFactory
        : IFarmHashFactory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashFingerprint"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashFingerprint"/> instance.</returns>
        new IFarmHashFingerprint Create();
    }
}
