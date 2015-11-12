using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Implementation of <see cref="IObjectHashFunction" /> that uses the object's <see cref="object.GetHashCode()"/> implementation 
    /// to calculate a 32-bit hash.
    /// </summary>
    public class GetHashCodeWrapper
        : IObjectHashFunction
    {
        /// <summary>
        /// Size of produced hash, in bits.
        /// </summary>
        /// <value>
        /// The size of the hash, in bits.
        /// </value>
        /// <remarks>
        /// Always returns <c>32</c>.
        /// </remarks>
        public int HashSize { get { return 32; } }

        /// <summary>
        /// Computes hash value for given object.
        /// </summary>
        /// <param name="object">Object to hash.</param>
        /// <returns>
        /// Hash value of the data as byte array.
        /// </returns>
        public byte[] CalculateHash(object @object)
        {
            if (@object == null)
                throw new ArgumentNullException("object");

            return BitConverter.GetBytes(@object.GetHashCode());
        }

    }
}
