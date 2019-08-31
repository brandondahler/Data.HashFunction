using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Base implementation to provide instances of implementations of <typeparamref name="TFarmHashFingerprint32"/>.
    /// </summary>
    public abstract class FarmHashFingerprint32FactoryBase<TFarmHashFingerprint32>
        : FarmHashMkHash32FactoryBase<TFarmHashFingerprint32>,
            IFarmHashFingerprint32Factory
        where TFarmHashFingerprint32 : IFarmHashFingerprint32
    {

        /// <summary>
        /// Creates a new <see cref="IFarmHashFingerprint"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashFingerprint"/> instance.</returns>
        IFarmHashFingerprint IFarmHashFingerprintFactory.Create() => Create();

        /// <summary>
        /// Creates a new <see cref="IFarmHashFingerprint32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashFingerprint32"/> instance.</returns>
        IFarmHashFingerprint32 IFarmHashFingerprint32Factory.Create() => Create();
    }
}
