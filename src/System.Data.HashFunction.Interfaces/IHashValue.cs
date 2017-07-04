using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Common interface to represent a hash value. 
    /// </summary>
    public interface IHashValue
        : IEquatable<IHashValue>
    {
        /// <summary>
        /// Gets the length of the hash value in bits.
        /// </summary>
        /// <value>
        /// The length of the hash value bit.
        /// </value>
        int BitLength { get; }

        /// <summary>
        /// Gets resulting byte array.
        /// </summary>
        /// <value>
        /// The hash value.
        /// </value>
        byte[] Hash { get; }


        /// <summary>
        /// Converts the hash value to a bit array.
        /// </summary>
        /// <returns>A <see cref="BitArray"/> instance to represent this hash value.</returns>
        BitArray AsBitArray();
        
        /// <summary>
        /// Converts the hash value to a hexadecimal string.
        /// </summary>
        /// <returns>A hex string representing this hash value.</returns>
        string AsHexString();

        /// <summary>
        /// Converts the hash value to a the base64 string.
        /// </summary>
        /// <returns>A base64 string representing this hash value.</returns>
        string AsBase64String();

    }
}
