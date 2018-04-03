using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashFingerprint32"/>.
    /// </summary>
    public interface IFarmHashFingerprint32Factory
        : IFarmHash32Factory,
            IFarmHashMkHash32Factory,
            IFarmHashFingerprintFactory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashFingerprint32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashFingerprint32"/> instance.</returns>
        new IFarmHashFingerprint32 Create();
    }
}
