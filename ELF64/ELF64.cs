using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Data.HashFunction.Utilities.IntegerManipulation;
using System.IO;
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
        /// <summary>
        /// Initializes a new instance of the <see cref="ELF64"/> class.
        /// </summary>
        /// <inheritdoc cref="HashFunctionBase(int)" />
        public ELF64()
            : base(32)
        {

        }


        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(Stream data)
        {
            if (HashSize != 32)
                throw new InvalidOperationException("HashSize set to an invalid value.");

            UInt32 hash = 0;

            foreach (byte dataByte in data.AsEnumerable())
            {
                hash <<= 4;
                hash += dataByte;

                var tmp = hash & 0xF0000000;

		        if (tmp != 0)
		            hash ^= tmp >> 24;
		        
                hash &= 0x0FFFFFFF;
            }

            return BitConverter.GetBytes(hash);
        }
    }
}
