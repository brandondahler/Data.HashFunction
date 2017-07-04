using System;
using System.Collections.Generic;
using System.Data.HashFunction.Core.Utilities;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{


    /// <summary>
    /// Implementation of <see cref="IHashFunction" /> that wraps cryptographic hash functions known as <see cref="HashAlgorithm" />.
    /// </summary>
    public sealed class HashAlgorithmWrapper
        : IHashFunction
    {

        /// <summary>
        /// Size of produced hash, in bits.
        /// </summary>
        /// <value>
        /// The size of the hash, in bits.
        /// </value>
        public int HashSize { get; }


        private readonly Func<HashAlgorithm> _hashAlgorithmFactory;



        /// <summary>
        /// Initializes a new instance of the <see cref="HashAlgorithmWrapper"/> class.
        /// </summary>
        /// <param name="hashAlgorithmFactory">Function that produces new instances of a <see cref="HashAlgorithm" /> to use for hashing.</param>
        public HashAlgorithmWrapper(Func<HashAlgorithm> hashAlgorithmFactory)
        {
            _hashAlgorithmFactory = hashAlgorithmFactory;

            using (var hashAlgorithm = _hashAlgorithmFactory.Invoke())
                HashSize = hashAlgorithm.HashSize;
        }


        /// <inheritdoc />
        public IHashValue ComputeHash(byte[] data) => ComputeHash(data, CancellationToken.None);

        /// <inheritdoc />
        public IHashValue ComputeHash(byte[] data, CancellationToken cancellationToken)
        {
            using (var hashAlgorithm = _hashAlgorithmFactory.Invoke())
            {
                return new HashValue(
                    hashAlgorithm.ComputeHash(data),
                    HashSize);
            }
        }


        /// <inheritdoc />
        public IHashValue ComputeHash(Stream data) => ComputeHash(data, CancellationToken.None);

        /// <inheritdoc />
        public IHashValue ComputeHash(Stream data, CancellationToken cancellationToken)
        {
            using (var hashAlgorithm = _hashAlgorithmFactory.Invoke())
            {
                return new HashValue(
                    hashAlgorithm.ComputeHash(data),
                    HashSize);
            }
        }
    }
}
