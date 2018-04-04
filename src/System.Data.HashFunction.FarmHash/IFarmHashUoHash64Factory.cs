using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashUoHash64"/>.
    /// </summary>
    public interface IFarmHashUoHash64Factory
        : IFarmHashHash64Factory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashUoHash64"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashUoHash64"/> instance.</returns>
        new IFarmHashUoHash64 Create();
    }
}
