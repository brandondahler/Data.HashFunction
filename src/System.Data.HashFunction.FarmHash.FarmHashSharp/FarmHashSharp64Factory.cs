using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash.FarmHashSharp
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashSharp64"/>.
    /// </summary>
    public sealed class FarmHashSharp64Factory
        : FarmHashFactoryBase<IFarmHashSharp64>,
            IFarmHashSharp64Factory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IFarmHashSharp64Factory Instance { get; } = new FarmHashSharp64Factory();


        private FarmHashSharp64Factory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IFarmHashSharp64"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashSharp64"/> instance.</returns>
        public override IFarmHashSharp64 Create() =>
            new FarmHashSharp64_Implementation();

        /// <summary>
        /// Creates a new <see cref="IFarmHash64"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHash64"/> instance.</returns>
        IFarmHash64 IFarmHash64Factory.Create() => Create();
    }
}
