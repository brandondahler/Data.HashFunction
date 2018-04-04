using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashFingerprint128"/>.
    /// </summary>
    public sealed class FarmHashFingerprint128Factory
        : FarmHashFingerprint128FactoryBase<IFarmHashFingerprint128>,
            IFarmHashFingerprint128Factory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IFarmHashFingerprint128Factory Instance { get; } = new FarmHashFingerprint128Factory();


        private FarmHashFingerprint128Factory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IFarmHashFingerprint128"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashFingerprint128"/> instance.</returns>
        public override IFarmHashFingerprint128 Create() =>
            new FarmHashFingerprint128_Implementation();
    }
}
