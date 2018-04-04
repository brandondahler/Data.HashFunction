using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash.FarmHashSharp
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashSharpHash64"/>.
    /// </summary>
    public sealed class FarmHashSharpHash64Factory
        : FarmHashXoHash64FactoryBase<IFarmHashSharpHash64>,
            IFarmHashSharpHash64Factory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IFarmHashSharpHash64Factory Instance { get; } = new FarmHashSharpHash64Factory();


        private FarmHashSharpHash64Factory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IFarmHashSharpHash64"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashSharpHash64"/> instance.</returns>
        public override IFarmHashSharpHash64 Create() =>
            new FarmHashSharpHash64_Implementation();
    }
}
