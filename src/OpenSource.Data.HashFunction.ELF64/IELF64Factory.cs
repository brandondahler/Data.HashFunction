using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.ELF64
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IELF64"/>.
    /// </summary>
    public interface IELF64Factory
    {
        /// <summary>
        /// Creates a new <see cref="IELF64"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IELF64"/> instance.</returns>
        IELF64 Create();
    }
}
