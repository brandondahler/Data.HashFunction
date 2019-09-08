using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.ELF64
{
    /// <summary>
    /// Implementation of the hash function used in the elf64 object file format as specified at 
    ///   http://downloads.openwatcom.org/ftp/devel/docs/elf-64-gen.pdf on page 17.
    ///
    /// Contrary to the name, the hash algorithm is only designed for 32-bit output hash sizes.
    /// </summary>
    public interface IELF64
        : IStreamableHashFunction
    {

    }
}
