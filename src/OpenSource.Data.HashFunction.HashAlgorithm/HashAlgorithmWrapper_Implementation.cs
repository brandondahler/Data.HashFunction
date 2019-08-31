using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core.Utilities;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSource.Data.HashFunction.HashAlgorithm
{
    using HashAlgorithm = System.Security.Cryptography.HashAlgorithm;

    /// <summary>
    /// Implementation of <see cref="IHashFunction" /> that wraps cryptographic hash functions known as <see cref="HashAlgorithm" />.
    /// </summary>
    internal class HashAlgorithmWrapper_Implementation
        : IHashAlgorithmWrapper
    {

        /// <summary>
        /// Size of produced hash, in bits.
        /// </summary>
        /// <value>
        /// The size of the hash, in bits.
        /// </value>
        public int HashSizeInBits { get; }

        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        public IHashAlgorithmWrapperConfig Config => _config.Clone();



        private readonly IHashAlgorithmWrapperConfig _config;



        /// <summary>
        /// Initializes a new instance of the <see cref="HashAlgorithmWrapper_Implementation"/> class.
        /// </summary>
        /// <param name="config">The configuration to use for this instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="config"/></exception>
        /// <exception cref="ArgumentException"><paramref name="config"/>.<see cref="IHashAlgorithmWrapperConfig.InstanceFactory"/> has not been set.;<paramref name="config"/>.<see cref="IHashAlgorithmWrapperConfig.InstanceFactory"/></exception>
        public HashAlgorithmWrapper_Implementation(IHashAlgorithmWrapperConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Clone();


            if (_config.InstanceFactory == null)
                throw new ArgumentException($"{nameof(config)}.{nameof(config.InstanceFactory)} has not been set.", $"{nameof(config)}.{nameof(config.InstanceFactory)}");



            using (var hashAlgorithm = _config.InstanceFactory())
                HashSizeInBits = hashAlgorithm.HashSize;
        }


        /// <inheritdoc />
        public IHashValue ComputeHash(byte[] data) => ComputeHash(data, CancellationToken.None);

        /// <inheritdoc />
        public IHashValue ComputeHash(byte[] data, CancellationToken cancellationToken)
        {
            using (var hashAlgorithm = _config.InstanceFactory())
            {
                return new HashValue(
                    hashAlgorithm.ComputeHash(data),
                    HashSizeInBits);
            }
        }


        /// <inheritdoc />
        public IHashValue ComputeHash(Stream data) => 
            ComputeHash(data, CancellationToken.None);

        /// <inheritdoc />
        public IHashValue ComputeHash(Stream data, CancellationToken cancellationToken)
        {
            using (var hashAlgorithm = _config.InstanceFactory())
            {
                return new HashValue(
                    hashAlgorithm.ComputeHash(data),
                    HashSizeInBits);
            }
        }
    }
}
