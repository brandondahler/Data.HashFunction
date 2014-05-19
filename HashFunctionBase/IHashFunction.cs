

using System.Collections.Generic;
using System.IO;
namespace System.Data.HashFunction
{
    public interface IHashFunction
    {
        /// <summary>
        /// Size of resulting hash in bits.
        /// </summary>
        int HashSize { get; set; }

        /// <summary>
        /// Valid sizes of <see cref="HashSize"/> in bits.
        /// </summary>
        IEnumerable<int> ValidHashSizes { get; }


        byte[] ComputeHash(byte[] data);
    }
}
