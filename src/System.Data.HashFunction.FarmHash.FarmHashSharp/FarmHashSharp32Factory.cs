using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash.FarmHashSharp
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashSharp32"/>.
    /// </summary>
    public sealed class FarmHashSharp32Factory
        : FarmHashFactoryBase<IFarmHashSharp32>,
            IFarmHashSharp32Factory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IFarmHashSharp32Factory Instance { get; } = new FarmHashSharp32Factory();


        private FarmHashSharp32Factory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IFarmHashSharp32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashSharp32"/> instance.</returns>
        public override IFarmHashSharp32 Create() =>
            new FarmHashSharp32_Implementation();

        /// <summary>
        /// Creates a new <see cref="IFarmHash32"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHash32"/> instance.</returns>
        IFarmHash32 IFarmHash32Factory.Create() => Create();
    }
}
