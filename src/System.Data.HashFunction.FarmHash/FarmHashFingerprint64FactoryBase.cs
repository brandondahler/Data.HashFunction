using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Base implementation to provide instances of implementations of <typeparamref name="TFarmHashFingerprint64"/>.
    /// </summary>
    public abstract class FarmHashFingerprint64FactoryBase<TFarmHashFingerprint64>
        : FarmHashNaHash64FactoryBase<TFarmHashFingerprint64>,
            IFarmHashFingerprint64Factory
        where TFarmHashFingerprint64 : IFarmHashFingerprint64
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashFingerprint"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashFingerprint"/> instance.</returns>
        IFarmHashFingerprint IFarmHashFingerprintFactory.Create() => Create();

        /// <summary>
        /// Creates a new <see cref="IFarmHashFingerprint64"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashFingerprint64"/> instance.</returns>
        IFarmHashFingerprint64 IFarmHashFingerprint64Factory.Create() => Create();

    }
}
