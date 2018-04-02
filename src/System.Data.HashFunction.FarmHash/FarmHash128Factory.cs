using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHash128"/>.
    /// </summary>
    public sealed class FarmHash128Factory
        : FarmHashFactoryBase<IFarmHash128>,
            IFarmHash128Factory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IFarmHash128Factory Instance { get; } = new FarmHash128Factory();


        private FarmHash128Factory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IFarmHash128"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHash128"/> instance.</returns>
        public override IFarmHash128 Create() =>
            new FarmHash128_Implementation();
    }
}
