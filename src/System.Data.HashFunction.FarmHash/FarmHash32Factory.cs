using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHash32"/>.
    /// </summary>
    public sealed class FarmHash32Factory
        : FarmHashFactoryBase<IFarmHash32>,
            IFarmHash32Factory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IFarmHash32Factory Instance { get; } = new FarmHash32Factory();


        private FarmHash32Factory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IFarmHash32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHash32"/> instance.</returns>
        public override IFarmHash32 Create() =>
            new FarmHash32_Implementation();
    }
}
