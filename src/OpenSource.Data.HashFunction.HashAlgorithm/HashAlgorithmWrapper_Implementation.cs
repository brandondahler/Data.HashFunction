using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core.Utilities;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenSource.Data.HashFunction.Core;

namespace OpenSource.Data.HashFunction.HashAlgorithm
{
    using HashAlgorithm = System.Security.Cryptography.HashAlgorithm;

    internal class HashAlgorithmWrapper_Implementation
        : HashFunctionBase, 
            IHashAlgorithmWrapper
    {
        public override int HashSizeInBits { get; }

        public IHashAlgorithmWrapperConfig Config => _config.Clone();



        private readonly IHashAlgorithmWrapperConfig _config;



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

        public IHashValue ComputeHash(Stream data)
        {
            using (var hashAlgorithm = _config.InstanceFactory())
            {
                return new HashValue(
                    hashAlgorithm.ComputeHash(data),
                    HashSizeInBits);
            }
        }

        protected override IHashValue ComputeHashInternal(ArraySegment<byte> data, CancellationToken cancellationToken)
        {
            using (var hashAlgorithm = _config.InstanceFactory())
            {
                return new HashValue(
                    hashAlgorithm.ComputeHash(data.Array, data.Offset, data.Count),
                    HashSizeInBits);
            }
        }
    }
}
