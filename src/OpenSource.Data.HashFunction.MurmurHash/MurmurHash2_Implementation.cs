using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core;
using OpenSource.Data.HashFunction.Core.Utilities;
using OpenSource.Data.HashFunction.Core.Utilities.UnifiedData;
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
        : HashFunctionAsyncBase,
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
        private const UInt64 MixConstant = 0xc6a4a7935bd1e995;


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


        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(IUnifiedData data, CancellationToken cancellationToken)
        {
            byte[] hash = null;

            switch (_config.HashSizeInBits)
            {
                case 32:
                {        
                    const UInt32 m = unchecked((UInt32) MixConstant);

                    UInt32 h = (UInt32) _config.Seed ^ (UInt32) data.Length;

                    data.ForEachGroup(4, 
                        (dataGroup, position, length) => {
                            ProcessGroup(ref h, m, dataGroup, position, length);
                        }, 
                        (remainder, position, length) => {
                            ProcessRemainder(ref h, m, remainder, position, length);
                        },
                        cancellationToken);

                    // Do a few final mixes of the hash to ensure the last few
                    // bytes are well-incorporated.

                    h ^= h >> 13;
                    h *= m;
                    h ^= h >> 15;

                    hash = BitConverter.GetBytes(h);
                    break;
                }

                case 64:
                { 
                    const UInt64 m = MixConstant;

                    UInt64 h = _config.Seed ^ ((UInt64) data.Length * m);
            
                    data.ForEachGroup(8, 
                        (dataGroup, position, length) => {
                            ProcessGroup(ref h, m, dataGroup, position, length);
                        },
                        (remainder, position, length) => {
                            ProcessRemainder(ref h, m, remainder, position, length);
                        },
                        cancellationToken);
 
                    h ^= h >> 47;
                    h *= m;
                    h ^= h >> 47;

                    hash = BitConverter.GetBytes(h);
                    break;
                }
                    
                default:
                {
                    throw new NotImplementedException();
                }
            }

            return hash;
        }

        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override async Task<byte[]> ComputeHashAsyncInternal(IUnifiedDataAsync data, CancellationToken cancellationToken)
        {
            byte[] hash = null;

            switch (_config.HashSizeInBits)
            {
                case 32:
                {        
                    const UInt32 m = unchecked((UInt32) MixConstant);

                    UInt32 h = (UInt32) _config.Seed ^ (UInt32) data.Length;

                    await data.ForEachGroupAsync(4, 
                            (dataGroup, position, length) => {
                                ProcessGroup(ref h, m, dataGroup, position, length);
                            }, 
                            (remainder, position, length) => {
                                ProcessRemainder(ref h, m, remainder, position, length);
                            },
                            cancellationToken)
                        .ConfigureAwait(false);

                    // Do a few final mixes of the hash to ensure the last few
                    // bytes are well-incorporated.

                    h ^= h >> 13;
                    h *= m;
                    h ^= h >> 15;

                    hash = BitConverter.GetBytes(h);
                    break;
                }

                case 64:
                { 
                    const UInt64 m = MixConstant;

                    UInt64 h = _config.Seed ^ ((UInt64) data.Length * m);
            
                    await data.ForEachGroupAsync(8, 
                            (dataGroup, position, length) => {
                                ProcessGroup(ref h, m, dataGroup, position, length);
                            },
                            (remainder, position, length) => {
                                ProcessRemainder(ref h, m, remainder, position, length);
                            },
                            cancellationToken)
                        .ConfigureAwait(false);
 
                    h ^= h >> 47;
                    h *= m;
                    h ^= h >> 47;

                    hash = BitConverter.GetBytes(h);
                    break;
                }
        
                default:
                {
                    throw new NotImplementedException();
                }
            }

            return hash;
        }


        private static void ProcessGroup(ref UInt32 h, UInt32 m, byte[] dataGroup, int position, int length)
        {
            for (var x = position; x < position + length; x += 4)
            {
                UInt32 k = BitConverter.ToUInt32(dataGroup, x);

                k *= m;
                k ^= k >> 24;
                k *= m;

                h *= m;
                h ^= k;
            }
        }

        private static void ProcessGroup(ref UInt64 h, UInt64 m, byte[] dataGroup, int position, int length)
        {
            for (var x = position; x < position + length; x += 8)
            {
                UInt64 k = BitConverter.ToUInt64(dataGroup, x);

                k *= m;
                k ^= k >> 47;
                k *= m;

                h ^= k;
                h *= m;
            }
        }

        private static void ProcessRemainder(ref UInt32 h, UInt32 m, byte[] remainder, int position, int length)
        {
            Debug.Assert(length > 0);
            Debug.Assert(length < 4);

            switch (length)
            {
                case 3: h ^= (UInt32) remainder[position + 2] << 16;   goto case 2;
                case 2: h ^= (UInt32) remainder[position + 1] <<  8;   goto case 1;
                case 1:
                    h ^= remainder[position];
                    break;
            };

            h *= m;
        }


        private static void ProcessRemainder(ref UInt64 h, UInt64 m, byte[] remainder, int position, int length)
        {
            Debug.Assert(length > 0);
            Debug.Assert(length < 8);

            switch (length)
            {
                case 7: h ^= (UInt64) remainder[position + 6] << 48;  goto case 6;
                case 6: h ^= (UInt64) remainder[position + 5] << 40;  goto case 5;
                case 5: h ^= (UInt64) remainder[position + 4] << 32;  goto case 4;
                case 4: 
                    h ^= (UInt64) BitConverter.ToUInt32(remainder, position);
                    break;

                case 3: h ^= (UInt64) remainder[position + 2] << 16;  goto case 2;
                case 2: h ^= (UInt64) remainder[position + 1] <<  8;  goto case 1;
                case 1: 
                    h ^= (UInt64) remainder[position];
                    break;
            };

            h *= m;
        }
    }
}
