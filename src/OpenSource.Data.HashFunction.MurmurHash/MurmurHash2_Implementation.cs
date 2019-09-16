using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core;
using OpenSource.Data.HashFunction.Core.Utilities;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSource.Data.HashFunction.MurmurHash
{
    /// <summary>
    /// Implementation of MurmurHash2 as specified at https://github.com/aappleby/smhasher/blob/master/src/MurmurHash2.cpp 
    ///   and https://github.com/aappleby/smhasher/wiki/MurmurHash2.
    /// 
    /// This hash function has been superseded by <seealso cref="IMurmurHash3">MurmurHash3</seealso>.
    /// </summary>
    internal class MurmurHash2_Implementation
        : HashFunctionBase,
            IMurmurHash2
    {

        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        public IMurmurHash2Config Config => _config.Clone();


        public override int HashSizeInBits => _config.HashSizeInBits;



        /// <summary>
        /// Constant as defined by MurmurHash2 specification.
        /// </summary>
        private const UInt32 _mixConstant32 = 0x5bd1e995;
        private const UInt64 _mixConstant64 = 0xc6a4a7935bd1e995;


        private readonly IMurmurHash2Config _config;


        private static readonly IEnumerable<int> _validHashSizes = new HashSet<int>() { 32, 64 };


        /// <summary>
        /// Initializes a new instance of the <see cref="MurmurHash2_Implementation"/> class.
        /// </summary>
        /// <param name="config">The configuration to use for this instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="config"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="config"/>.<see cref="IMurmurHash2Config.HashSizeInBits"/>;<paramref name="config"/>.<see cref="IMurmurHash2Config.HashSizeInBits"/> must be contained within MurmurHash2.ValidHashSizes.</exception>
        public MurmurHash2_Implementation(IMurmurHash2Config config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Clone();


            if (!_validHashSizes.Contains(_config.HashSizeInBits))
                throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.HashSizeInBits)}", _config.HashSizeInBits, $"{nameof(config)}.{nameof(config.HashSizeInBits)} must be contained within MurmurHash2.ValidHashSizes.");

        }


        protected override IHashValue ComputeHashInternal(ArraySegment<byte> data, CancellationToken cancellationToken)
        {
            switch (_config.HashSizeInBits)
            {
                case 32:
                    return ComputeHash32(data, cancellationToken);

                case 64:
                    return ComputeHash64(data, cancellationToken);

                default:
                    throw new NotImplementedException();
            }
        }



        protected IHashValue ComputeHash32(ArraySegment<byte> data, CancellationToken cancellationToken)
        {
            var dataArray = data.Array;
            var dataOffset = data.Offset;
            var dataCount = data.Count;

            var endOffset = dataOffset + dataCount;
            var remainderCount = dataCount % 4;

            UInt32 hashValue = (UInt32) _config.Seed ^ (UInt32) dataCount;

            // Process 4-byte groups
            {
                var groupEndOffset = endOffset - remainderCount;

                for (var currentOffset = dataOffset; currentOffset < groupEndOffset; currentOffset += 4)
                {
                    UInt32 k = BitConverter.ToUInt32(dataArray, currentOffset);

                    k *= _mixConstant32;
                    k ^= k >> 24;
                    k *= _mixConstant32;

                    hashValue *= _mixConstant32;
                    hashValue ^= k;
                }
            }

            // Process remainder
            if (remainderCount > 0)
            {
                var remainderOffset = endOffset - remainderCount;

                switch (remainderCount)
                {
                    case 3: hashValue ^= (UInt32) dataArray[remainderOffset + 2] << 16; goto case 2;
                    case 2: hashValue ^= (UInt32) dataArray[remainderOffset + 1] << 8; goto case 1;
                    case 1:
                        hashValue ^= (UInt32) dataArray[remainderOffset];
                        break;
                };

                hashValue *= _mixConstant32;
            }


            hashValue ^= hashValue >> 13;
            hashValue *= _mixConstant32;
            hashValue ^= hashValue >> 15;

            return new HashValue(
                BitConverter.GetBytes(hashValue),
                32);
        }
        
        protected IHashValue ComputeHash64(ArraySegment<byte> data, CancellationToken cancellationToken)
        {
            var dataArray = data.Array;
            var dataOffset = data.Offset;
            var dataCount = data.Count;

            var endOffset = dataOffset + dataCount;
            var remainderCount = dataCount % 8;

            UInt64 hashValue = _config.Seed ^ ((UInt64) dataCount * _mixConstant64);

            // Process 8-byte groups
            {
                var groupEndOffset = endOffset - remainderCount;

                for (var currentOffset = dataOffset; currentOffset < groupEndOffset; currentOffset += 8)
                {
                    UInt64 k = BitConverter.ToUInt64(dataArray, currentOffset);

                    k *= _mixConstant64;
                    k ^= k >> 47;
                    k *= _mixConstant64;

                    hashValue ^= k;
                    hashValue *= _mixConstant64;
                }
            }

            // Process remainder
            if (remainderCount > 0)
            {
                var remainderOffset = endOffset - remainderCount;

                switch (remainderCount)
                {
                    case 7: hashValue ^= (UInt64) dataArray[remainderOffset + 6] << 48; goto case 6;
                    case 6: hashValue ^= (UInt64) dataArray[remainderOffset + 5] << 40; goto case 5;
                    case 5: hashValue ^= (UInt64) dataArray[remainderOffset + 4] << 32; goto case 4;
                    case 4:
                        hashValue ^= (UInt64) BitConverter.ToUInt32(dataArray, remainderOffset);
                        break;

                    case 3: hashValue ^= (UInt64) dataArray[remainderOffset + 2] << 16; goto case 2;
                    case 2: hashValue ^= (UInt64) dataArray[remainderOffset + 1] << 8; goto case 1;
                    case 1:
                        hashValue ^= (UInt64) dataArray[remainderOffset];
                        break;
                };

                hashValue *= _mixConstant64;
            }


            hashValue ^= hashValue >> 47;
            hashValue *= _mixConstant64;
            hashValue ^= hashValue >> 47;

            return new HashValue(
                BitConverter.GetBytes(hashValue),
                64);
        }
    }
}
