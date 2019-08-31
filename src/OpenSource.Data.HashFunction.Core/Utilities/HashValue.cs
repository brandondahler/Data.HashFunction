using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Core.Utilities
{
    /// <summary>
    /// Implementation of <see cref="IHashValue"/>
    /// </summary>
    public sealed class HashValue
        : IHashValue
    {
        /// <summary>
        /// Gets the length of the hash value in bits.
        /// </summary>
        /// <value>
        /// The length of the hash value bit.
        /// </value>
        public byte[] Hash { get; }

        /// <summary>
        /// Gets resulting byte array.
        /// </summary>
        /// <value>
        /// The hash value.
        /// </value>
        /// <remarks>
        /// Implementations should coerce the input hash value to be <see cref="BitLength"/> size in bits.
        /// </remarks>
        public int BitLength { get; }



        /// <summary>
        /// Initializes a new instance of <see cref="HashValue"/>.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <param name="bitLength">Length of the hash, in bits.</param>
        /// <exception cref="ArgumentNullException"><paramref name="hash"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="bitLength"/>;bitLength must be greater than or equal to 1.</exception>
        public HashValue(IEnumerable<byte> hash, int bitLength)
        {
            if (hash == null)
                throw new ArgumentNullException(nameof(hash));

            if (bitLength < 1)
                throw new ArgumentOutOfRangeException(nameof(bitLength), $"{nameof(bitLength)} must be greater than or equal to 1.");

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

                foreach (var value in Hash)
                    hashCode = (hashCode * 31) ^ value.GetHashCode();

                return hashCode;
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="Object" /> is equal to this instance; otherwise, <c>false</c>.
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
        /// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(IHashValue other)
        {
            if (other == null || other.BitLength != BitLength)
                return false;

            return Hash.SequenceEqual(other.Hash);
        }


        /// <summary>
        /// Coerces the given <paramref name="hash"/> to a byte array with <paramref name="bitLength"/> significant bits.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <param name="bitLength">Length of the hash, in bits.</param>
        /// <returns>A byte array that has been coerced to the proper length.</returns>
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
