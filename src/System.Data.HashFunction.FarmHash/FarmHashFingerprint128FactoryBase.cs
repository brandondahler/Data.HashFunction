using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Base implementation to provide instances of implementations of <typeparamref name="TFarmHashFingerprint128"/>.
    /// </summary>
    public abstract class FarmHashFingerprint128FactoryBase<TFarmHashFingerprint128>
        : FarmHash128FactoryBase<TFarmHashFingerprint128>,
            IFarmHashFingerprint128Factory
        where TFarmHashFingerprint128 : IFarmHashFingerprint128
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
        IFarmHashFingerprint128 IFarmHashFingerprint128Factory.Create() => Create();
    }
}
