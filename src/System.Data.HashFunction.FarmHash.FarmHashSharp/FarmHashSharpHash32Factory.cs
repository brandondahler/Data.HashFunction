using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash.FarmHashSharp
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashSharpHash32"/>.
    /// </summary>
    public sealed class FarmHashSharpHash32Factory
        : FarmHashFingerprint32FactoryBase<IFarmHashSharpHash32>,
            IFarmHashSharpHash32Factory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IFarmHashSharpHash32Factory Instance { get; } = new FarmHashSharpHash32Factory();


        private FarmHashSharpHash32Factory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IFarmHashSharpHash32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashSharpHash32"/> instance.</returns>
        public override IFarmHashSharpHash32 Create() =>
            new FarmHashSharpHash32_Implementation();
    }
}
