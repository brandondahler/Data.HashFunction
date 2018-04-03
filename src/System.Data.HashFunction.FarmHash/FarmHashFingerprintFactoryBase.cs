using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Base implementation to provide instances of implementations of <typeparamref name="TFarmHashFingerprint"/>.
    /// </summary>
    public abstract class FarmHashFingerprintFactoryBase<TFarmHashFingerprint>
        : FarmHashFactoryBase<TFarmHashFingerprint>,
            IFarmHashFingerprintFactory
        where TFarmHashFingerprint : IFarmHashFingerprint
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashFingerprint"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashFingerprint"/> instance.</returns>
        IFarmHashFingerprint IFarmHashFingerprintFactory.Create() => Create();
    }
}
