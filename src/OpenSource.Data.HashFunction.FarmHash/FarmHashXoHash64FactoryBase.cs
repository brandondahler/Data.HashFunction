using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Base implementation to provide instances of implementations of <typeparamref name="TFarmHashXoHash64"/>.
    /// </summary>
    public abstract class FarmHashXoHash64FactoryBase<TFarmHashXoHash64>
        : FarmHashHash64FactoryBase<TFarmHashXoHash64>,
            IFarmHashXoHash64Factory
        where TFarmHashXoHash64 : IFarmHashXoHash64
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashXoHash64"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashXoHash64"/> instance.</returns>
        IFarmHashXoHash64 IFarmHashXoHash64Factory.Create() => Create();
    }
}
