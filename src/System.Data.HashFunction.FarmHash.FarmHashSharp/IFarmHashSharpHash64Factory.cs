using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash.FarmHashSharp
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IFarmHashSharpHash64"/>.
    /// </summary>
    public interface IFarmHashSharpHash64Factory
        : IFarmHashXoHash64Factory
    {
        /// <summary>
        /// Creates a new <see cref="IFarmHashSharpHash64"/> instance.
        /// </summary>
        /// <returns>A <see cref="IFarmHashSharpHash64"/> instance.</returns>
        new IFarmHashSharpHash64 Create();
    }
}
