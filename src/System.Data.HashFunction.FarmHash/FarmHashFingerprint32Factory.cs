using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashFingerprint32"/>.
    /// </summary>
    public sealed class FarmHashFingerprint32Factory
        : FarmHashFingerprintFactoryBase<IFarmHashFingerprint32>,
            IFarmHashFingerprint32Factory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IFarmHashFingerprint32Factory Instance { get; } = new FarmHashFingerprint32Factory();


        private FarmHashFingerprint32Factory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IFarmHashFingerprint32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashFingerprint32"/> instance.</returns>
        public override IFarmHashFingerprint32 Create() =>
            new FarmHashFingerprint32_Implementation();
    }
}
