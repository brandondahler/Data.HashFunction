using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashXoHash64"/>.
    /// </summary>
    public interface IFarmHashXoHash64Factory
        : IFarmHashHash64Factory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashXoHash64"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashXoHash64"/> instance.</returns>
        new IFarmHashXoHash64 Create();
    }
}
