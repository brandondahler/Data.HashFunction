using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHash64"/>.
    /// </summary>
    public sealed class FarmHash64Factory
        : FarmHashFactoryBase<IFarmHash64>,
            IFarmHash64Factory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IFarmHash64Factory Instance { get; } = new FarmHash64Factory();


        private FarmHash64Factory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IFarmHash64"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHash64"/> instance.</returns>
        public override IFarmHash64 Create() =>
            new FarmHash64_Implementation();
    }
}
