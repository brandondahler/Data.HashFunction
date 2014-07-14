using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Implementation of the hash function used in the elf64 object file format as specified at 
    ///   http://downloads.openwatcom.org/ftp/devel/docs/elf-64-gen.pdf on page 17.
    ///
    /// Contrary to the name, the hash algorithm is only designed for 32-bit output hash sizes.
    /// </summary>
    public class ELF64
        : HashFunctionBase
    {
        /// <inheritdoc/>
        public override IEnumerable<int> ValidHashSizes { get { return new[] { 32 }; } }


        /// <summary>
        /// Creates new <see cref="ELF64" /> instance.
        /// </summary>
        /// <remarks>HashSize defaults to 32 bits.</remarks>
        public ELF64()
            : base(32)
        {

        }


        /// <inheritdoc/>
        public override byte[] ComputeHash(byte[] data)
        {
            if (HashSize != 32)
                throw new ArgumentOutOfRangeException("HashSize");

            UInt32 hash = 0;

            foreach (var b in data)
            {
                hash <<= 4;
                hash += b;

                var tmp = hash & 0xF0000000;

		        if (tmp != 0)
		            hash ^= tmp >> 24;
		        
                hash &= 0x0FFFFFFF;
            }

            return BitConverter.GetBytes(hash);
        }
    }
}
