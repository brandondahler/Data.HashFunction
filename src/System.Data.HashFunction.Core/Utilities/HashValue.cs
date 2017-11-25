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
    public sealed class HashValue
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

            Hash = CoerceToArray(hash, bitLength);
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
        public string AsHexString() => AsHexString(false);

        /// <summary>
        /// Converts the hash value to a hexadecimal string.
        /// </summary>
        /// <param name="uppercase"><c>true</c> if the result should use uppercase hex values; otherwise <c>false</c>.</param>
        /// <returns>
        /// A hex string representing this hash value.
        /// </returns>
        public string AsHexString(bool uppercase)
        {
            var stringBuilder = new StringBuilder(Hash.Length);
            var formatString = uppercase ? "X2" : "x2";

            foreach (var byteValue in Hash)
                stringBuilder.Append(byteValue.ToString(formatString));

            return stringBuilder.ToString();
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
                    hashCode *= 31;
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

            return Hash.SequenceEqual(other.Hash);
        }


        private static byte[] CoerceToArray(IEnumerable<byte> hash, int bitLength)
        {
            var byteLength = (bitLength + 7) / 8;

            if ((bitLength % 8) == 0)
            {
                if (hash is IReadOnlyCollection<byte> hashByteCollection)
                {
                    if (hashByteCollection.Count == byteLength)
                        return hash.ToArray();
                }

                if (hash is byte[] hashByteArray)
                {
                    var newHashArray = new byte[byteLength];
                    {
                        Array.Copy(hashByteArray, newHashArray, Math.Min(byteLength, hashByteArray.Length));
                    }

                    return newHashArray;
                }
            }


            byte finalByteMask = (byte)((1 << (bitLength % 8)) - 1);
            {
                if (finalByteMask == 0)
                    finalByteMask = 255;
            }


            var coercedArray = new byte[byteLength];

            var currentIndex = 0;
            var hashEnumerator = hash.GetEnumerator();

            while (currentIndex < byteLength && hashEnumerator.MoveNext())
            {
                if (currentIndex == (byteLength - 1))
                    coercedArray[currentIndex] = (byte) (hashEnumerator.Current & finalByteMask);
                else
                    coercedArray[currentIndex] = hashEnumerator.Current;


                currentIndex += 1;
            }

            return coercedArray;
        }

    }
}
