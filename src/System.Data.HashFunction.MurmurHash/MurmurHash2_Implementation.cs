using System;
using System.Collections.Generic;
using System.Data.HashFunction.Core;
using System.Data.HashFunction.Core.Utilities;
using System.Data.HashFunction.Core.Utilities.UnifiedData;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.HashFunction.MurmurHash
{
    /// <summary>
    /// Implementation of MurmurHash2 as specified at https://code.google.com/p/smhasher/source/browse/trunk/MurmurHash2.cpp 
    ///   and https://code.google.com/p/smhasher/wiki/MurmurHash2.
    /// 
    /// This hash function has been superseded by MurmurHash3.
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

        
        /// <summary>
        /// Constant as defined by MurmurHash2 specification.
        /// </summary>
        private const UInt64 MixConstant = 0xc6a4a7935bd1e995;


        private readonly IMurmurHash2Config _config;


        private static readonly IEnumerable<int> _validHashSizes = new HashSet<int>() { 32, 64 };


        /// <summary>
        /// Initializes a new instance of the <see cref="MurmurHash2_Implementation"/> class.
        /// </summary>
        /// <param name="hashSize"><inheritdoc cref="HashFunctionBase(int)" /></param>
        /// <param name="seed"><inheritdoc cref="Seed" /></param>
        /// <exception cref="System.ArgumentNullException"><paramref name="config"/></exception>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="config"/>.<see cref="IMurmurHash2Config.HashSizeInBits"/>;<paramref name="config"/>.<see cref="IMurmurHash2Config.HashSizeInBits"/> must be contained within MurmurHash2.ValidHashSizes.</exception>
        /// <inheritdoc cref="HashFunctionBase(int)" />
        public MurmurHash2_Implementation(IMurmurHash2Config config)
            : base((config?.HashSizeInBits).GetValueOrDefault())
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Clone();


            if (!_validHashSizes.Contains(_config.HashSizeInBits))
                throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.HashSizeInBits)}", $"{nameof(config)}.{nameof(config.HashSizeInBits)} must be contained within MurmurHash2.ValidHashSizes.");

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
            switch (length)
            {
                case 3: h ^= (UInt32) remainder[position + 2] << 16;   goto case 2;
                case 2: h ^= (UInt32) remainder[position + 1] <<  8;   goto case 1;
                case 1:
                    h ^= remainder[position];
                    break;

                default:
                    throw new NotImplementedException();
            };

            h *= m;
        }


        private static void ProcessRemainder(ref UInt64 h, UInt64 m, byte[] remainder, int position, int length)
        {
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

                default:
                    throw new NotImplementedException();
            };

            h *= m;
        }
    }
}
