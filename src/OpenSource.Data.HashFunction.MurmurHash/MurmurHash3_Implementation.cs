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
    /// Implementation of MurmurHash3 as specified at https://github.com/aappleby/smhasher/blob/master/src/MurmurHash3.cpp 
    ///   and https://github.com/aappleby/smhasher/wiki/MurmurHash3.
    /// </summary>
    internal class MurmurHash3_Implementation
        : HashFunctionAsyncBase,
            IMurmurHash3
    {

        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        public IMurmurHash3Config Config => _config.Clone();

        public override int HashSizeInBits => _config.HashSizeInBits;


        /// <summary>
        /// Constant c1 for 32-bit calculation as defined by MurmurHash3 specification.
        /// </summary>
        private const UInt32 c1_32 = 0xcc9e2d51;

        /// <summary>
        /// Constant c2 for 32-bit calculation as defined by MurmurHash3 specification.
        /// </summary>
        private const UInt32 c2_32 = 0x1b873593;


        /// <summary>
        /// Constant c1 for 128-bit calculation as defined by MurMurHash3 specification.
        /// </summary>
        private const UInt64 c1_128 = 0x87c37b91114253d5;

        /// <summary>
        /// Constant c2 for 128-bit calculation as defined by MurMurHash3 specification.
        /// </summary>
        private const UInt64 c2_128 = 0x4cf5ad432745937f;


        private readonly IMurmurHash3Config _config;

        private static readonly IEnumerable<int> _validHashSizes = new HashSet<int>() { 32, 128 };



        /// <summary>
        /// Initializes a new instance of the <see cref="MurmurHash3_Implementation"/> class.
        /// </summary>
        /// <param name="config">The configuration to use for this instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="config"/></exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="config"/>.<see cref="IMurmurHash3Config.HashSizeInBits"/>;<paramref name="config"/>.<see cref="IMurmurHash3Config.HashSizeInBits"/> must be contained within MurmurHash3.ValidHashSizes.</exception>
        public MurmurHash3_Implementation(IMurmurHash3Config config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Clone();


            if (!_validHashSizes.Contains(_config.HashSizeInBits))
                throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.HashSizeInBits)}", _config.HashSizeInBits, $"{nameof(config)}.{nameof(config.HashSizeInBits)} must be contained within MurmurHash3.ValidHashSizes.");
        }



        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(IUnifiedData data, CancellationToken cancellationToken)
        {
            byte[] hash;

            switch (_config.HashSizeInBits)
            {
                case 32:
                {
                    UInt32 h1 = _config.Seed;
                    int dataCount = 0;


                    data.ForEachGroup(4, 
                        (dataGroup, position, length) => {
                            ProcessGroup(ref h1, dataGroup, position, length);

                            dataCount += length;
                        },
                        (remainder, position, length) => {
                            ProcessRemainder(ref h1, remainder, position, length);

                            dataCount += length;
                        },
                        cancellationToken);
            

                    h1 ^= (UInt32) dataCount;
                    Mix(ref h1);

                    hash = BitConverter.GetBytes(h1);
                    break;
                }

                case 128:
                {
                    UInt64 h1 = (UInt64) _config.Seed;
                    UInt64 h2 = (UInt64) _config.Seed;

                    int dataCount = 0;

            
                    data.ForEachGroup(16, 
                        (dataGroup, position, length) => {
                            ProcessGroup(ref h1, ref h2, dataGroup, position, length);

                            dataCount += length;
                        },
                        (remainder, position, length) => {
                            ProcessRemainder(ref h1, ref h2, remainder, position, length);

                            dataCount += length;
                        },
                        cancellationToken);


                    h1 ^= (UInt64) dataCount; 
                    h2 ^= (UInt64) dataCount;

                    h1 += h2;
                    h2 += h1;

                    Mix(ref h1);
                    Mix(ref h2);

                    h1 += h2;
                    h2 += h1;


                    var hashBytes = new byte[16];

                    BitConverter.GetBytes(h1)
                        .CopyTo(hashBytes, 0);

                    BitConverter.GetBytes(h2)
                        .CopyTo(hashBytes, 8);

                    hash = hashBytes;
                    break;
                }

                default:
                    throw new NotImplementedException();
            }

            return hash;
        }

        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override async Task<byte[]> ComputeHashAsyncInternal(IUnifiedDataAsync data, CancellationToken cancellationToken)
        {
            byte[] hash;

            switch (_config.HashSizeInBits)
            {
                case 32:
                {
                    UInt32 h1 = _config.Seed;
                    int dataCount = 0;


                    await data.ForEachGroupAsync(4, 
                            (dataGroup, position, length) => {
                                ProcessGroup(ref h1, dataGroup, position, length);

                                dataCount += length;
                            },
                            (remainder, position, length) => {
                                ProcessRemainder(ref h1, remainder, position, length);

                                dataCount += length;
                            },
                            cancellationToken)
                        .ConfigureAwait(false);
            

                    h1 ^= (UInt32) dataCount;
                    Mix(ref h1);

                    hash = BitConverter.GetBytes(h1);
                    break;
                }

                case 128:
                {
                    UInt64 h1 = (UInt64) _config.Seed;
                    UInt64 h2 = (UInt64) _config.Seed;

                    int dataCount = 0;

            
                    await data.ForEachGroupAsync(16, 
                            (dataGroup, position, length) => {
                                ProcessGroup(ref h1, ref h2, dataGroup, position, length);

                                dataCount += length;
                            },
                            (remainder, position, length) => {
                                ProcessRemainder(ref h1, ref h2, remainder, position, length);

                                dataCount += length;
                            },
                            cancellationToken)
                        .ConfigureAwait(false);


                    h1 ^= (UInt64) dataCount; 
                    h2 ^= (UInt64) dataCount;

                    h1 += h2;
                    h2 += h1;

                    Mix(ref h1);
                    Mix(ref h2);

                    h1 += h2;
                    h2 += h1;


                    hash = new byte[16];

                    BitConverter.GetBytes(h1)
                        .CopyTo(hash, 0);

                    BitConverter.GetBytes(h2)
                        .CopyTo(hash, 8);

                    break;
                }
        
                default:
                    throw new NotImplementedException();
            }

            return hash;
        }


        private void ProcessGroup(ref UInt32 h1, byte[] dataGroup, int position, int length)
        {
            for (var x = position; x < position + length; x += 4)
            {
                UInt32 k1 = BitConverter.ToUInt32(dataGroup, x);

                k1 *= c1_32;
                k1 = RotateLeft(k1, 15);
                k1 *= c2_32;

                h1 ^= k1;
                h1 = RotateLeft(h1, 13);
                h1 = (h1 * 5) + 0xe6546b64;
            }
        }

        private void ProcessGroup(ref UInt64 h1, ref UInt64 h2, byte[] dataGroup, int position, int length)
        {
            for (var x = position; x < position + length; x += 16)
            {
                UInt64 k1 = BitConverter.ToUInt64(dataGroup, x);
                UInt64 k2 = BitConverter.ToUInt64(dataGroup, x + 8);

                k1 *= c1_128;
                k1 = RotateLeft(k1, 31);
                k1 *= c2_128;
                h1 ^= k1;

                h1 = RotateLeft(h1, 27);
                h1 += h2;
                h1 = (h1 * 5) + 0x52dce729;

                k2 *= c2_128;
                k2 = RotateLeft(k2, 33);
                k2 *= c1_128;
                h2 ^= k2;

                h2 = RotateLeft(h2, 31);
                h2 += h1;
                h2 = (h2 * 5) + 0x38495ab5;
            }
        }


        private void ProcessRemainder(ref UInt32 h1, byte[] remainder, int position, int length)
        {
            Debug.Assert(length > 0);
            Debug.Assert(length < 4);

            UInt32 k2 = 0;

            switch (length)
            {
                case 3: k2 ^= (UInt32) remainder[position + 2] << 16; goto case 2;
                case 2: k2 ^= (UInt32) remainder[position + 1] <<  8; goto case 1;
                case 1:
                    k2 ^= (UInt32) remainder[position];        
                    break;
            }

            k2 *= c1_32;
            k2 = RotateLeft(k2, 15);
            k2 *= c2_32;
            h1 ^= k2;
        }

        private void ProcessRemainder(ref UInt64 h1, ref UInt64 h2, byte[] remainder, int position, int length)
        {
            Debug.Assert(length > 0);
            Debug.Assert(length < 16);


            UInt64 k1 = 0;
            UInt64 k2 = 0;

            switch(length)
            {
                case 15: k2 ^= (UInt64) remainder[position + 14] << 48;   goto case 14;
                case 14: k2 ^= (UInt64) remainder[position + 13] << 40;   goto case 13;
                case 13: k2 ^= (UInt64) remainder[position + 12] << 32;   goto case 12;
                case 12: k2 ^= (UInt64) remainder[position + 11] << 24;   goto case 11;
                case 11: k2 ^= (UInt64) remainder[position + 10] << 16;   goto case 10;
                case 10: k2 ^= (UInt64) remainder[position +  9] <<  8;   goto case 9;
                case  9: 
                    k2 ^= ((UInt64) remainder[position + 8]) <<  0;
                    k2 *= c2_128; 
                    k2  = RotateLeft(k2, 33); 
                    k2 *= c1_128; h2 ^= k2;

                    goto case 8;

                case  8:
                    k1 ^= BitConverter.ToUInt64(remainder, position);
                    break;

                case  7: k1 ^= (UInt64) remainder[position + 6] << 48;    goto case 6;
                case  6: k1 ^= (UInt64) remainder[position + 5] << 40;    goto case 5;
                case  5: k1 ^= (UInt64) remainder[position + 4] << 32;    goto case 4;
                case  4: k1 ^= (UInt64) remainder[position + 3] << 24;    goto case 3;
                case  3: k1 ^= (UInt64) remainder[position + 2] << 16;    goto case 2;
                case  2: k1 ^= (UInt64) remainder[position + 1] <<  8;    goto case 1;
                case  1: 
                    k1 ^= (UInt64) remainder[position] << 0;
                    break;
            }

            k1 *= c1_128;
            k1  = RotateLeft(k1, 31);
            k1 *= c2_128;
            h1 ^= k1;
        }


        private static void Mix(ref UInt32 h)
        {
            h ^= h >> 16;
            h *= 0x85ebca6b;
            h ^= h >> 13;
            h *= 0xc2b2ae35;
            h ^= h >> 16;
        }

        private static void Mix(ref UInt64 k)
        {
            k ^= k >> 33;
            k *= 0xff51afd7ed558ccd;
            k ^= k >> 33;
            k *= 0xc4ceb9fe1a85ec53;
            k ^= k >> 33;
        }


        private static UInt32 RotateLeft(UInt32 operand, int shiftCount)
        {
            shiftCount &= 0x1f;

            return
                (operand << shiftCount) |
                (operand >> (32 - shiftCount));
        }

        private static UInt64 RotateLeft(UInt64 operand, int shiftCount)
        {
            shiftCount &= 0x3f;

            return
                (operand << shiftCount) |
                (operand >> (64 - shiftCount));
        }
    }
}
