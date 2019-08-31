using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.ELF64
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IELF64"/>.
    /// </summary>
    public class ELF64Factory
        : IELF64Factory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IELF64Factory Instance { get; } = new ELF64Factory();


        private ELF64Factory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IELF64"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IELF64"/> instance.</returns>
        public IELF64 Create() => new ELF64_Implementation();
    }
}
