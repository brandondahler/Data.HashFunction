using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Core.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Data.HashFunction.IHashValue" />
    public class HashValue
        : IHashValue
    {
        /// <inheritdoc />
        public byte[] Hash { get; }

        /// <inheritdoc />
        public int BitLength { get; }



        /// <summary>
        /// Initializes a new instance of the <see cref="HashValue"/> class.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <param name="bitLength">Length of the hash in bits.</param>
        public HashValue(IEnumerable<byte> hash, int bitLength)
        {
            if (hash == null)
                throw new ArgumentNullException(nameof(hash));

            if (bitLength < 1)
                throw new ArgumentOutOfRangeException(nameof(bitLength), "bitLength must be greater than or equal to 1.");


            Hash = hash.ToArray();
            BitLength = bitLength;
        }


        /// <summary>
        /// Converts the hash value to a the base64 string.
        /// </summary>
        /// <returns>
        /// A base64 string representing this hash value.
        /// </returns>
        public string AsBase64String()
        {
            return Convert.ToBase64String(Hash);
        }

        /// <summary>
        /// Converts the hash value to a bit array.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.BitArray" /> instance to represent this hash value.
        /// </returns>
        public BitArray AsBitArray()
        {
            return new BitArray(Hash) {
                Length = BitLength
            };
        }

        /// <summary>
        /// Converts the hash value to a hexadecimal string.
        /// </summary>
        /// <returns>
        /// A hex string representing this hash value.
        /// </returns>
        public string AsHexString()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 17;
                hashCode = (hashCode * 31) ^ BitLength.GetHashCode();

                if (Hash != null)
                {
                    foreach (var value in Hash)
                        hashCode = (hashCode * 31) ^ value.GetHashCode();
                } else {
                    hashCode = (hashCode * 31) ^ 0;
                }

                return hashCode;
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as IHashValue);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(IHashValue other)
        {
            if (other.BitLength != BitLength)
                return false;

            for (var x = 0; x < Math.Min(Hash.Length, other.Hash.Length); ++x)
            {
                if (other.Hash[x] != Hash[x])
                    return false;
            }

            return true;
        }


    }
}
