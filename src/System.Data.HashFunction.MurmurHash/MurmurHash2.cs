using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Data.HashFunction.Utilities.IntegerManipulation;
using System.Data.HashFunction.Utilities.UnifiedData;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Implementation of MurmurHash2 as specified at https://code.google.com/p/smhasher/source/browse/trunk/MurmurHash2.cpp 
    ///   and https://code.google.com/p/smhasher/wiki/MurmurHash2.
    /// 
    /// This hash function has been superseded by MurmurHash3.
    /// </summary>
    public class MurmurHash2
#if !NET40 || INCLUDE_ASYNC
        : HashFunctionAsyncBase
#else
        : HashFunctionBase
#endif
    {
        /// <summary>
        /// Seed value for hash calculation.
        /// </summary>
        /// <value>
        /// The seed value for hash calculation.
        /// </value>
        public UInt64 Seed { get { return _Seed; } }


        /// <summary>
        /// The list of possible hash sizes that can be provided to the <see cref="MurmurHash2" /> constructor.
        /// </summary>
        /// <value>
        /// The list of valid hash sizes.
        /// </value>
        public static IEnumerable<int> ValidHashSizes { get { return _ValidHashSizes; } }


        /// <inheritdoc/>
        protected override bool RequiresSeekableStream { get { return true; } }

        /// <summary>
        /// Constant as defined by MurmurHash2 specification.
        /// </summary>
        protected const UInt64 MixConstant = 0xc6a4a7935bd1e995;


        private readonly UInt64 _Seed;

        private static readonly IEnumerable<int> _ValidHashSizes = new[] { 32, 64 };


        /// <remarks>
        /// Defaults <see cref="Seed" /> to 0.  <inheritdoc cref="MurmurHash2(UInt64)" />
        /// </remarks>
        /// <inheritdoc cref="MurmurHash2(UInt64)" />
        public MurmurHash2()
            : this(0U)
        {
            
        }

        /// <remarks>
        /// Defaults <see cref="Seed" /> to 0.
        /// </remarks>
        /// <inheritdoc cref="MurmurHash2(int, UInt64)" />
        public MurmurHash2(int hashSize)
            : this(hashSize, 0U)
        {

        }

        /// <remarks>
        /// Defaults <see cref="HashFunctionBase.HashSize" /> to 64.
        /// </remarks>
        /// <inheritdoc cref="MurmurHash2(int, UInt64)" />
        public MurmurHash2(UInt64 seed)
            : this(64, seed)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MurmurHash2"/> class.
        /// </summary>
        /// <param name="hashSize"><inheritdoc cref="HashFunctionBase(int)" /></param>
        /// <param name="seed"><inheritdoc cref="Seed" /></param>
        /// <exception cref="System.ArgumentOutOfRangeException">hashSize;hashSize must be contained within MurmurHash2.ValidHashSizes.</exception>
        /// <inheritdoc cref="HashFunctionBase(int)" />
        public MurmurHash2(int hashSize, UInt64 seed)
            : base(hashSize)
        {
            if (!ValidHashSizes.Contains(hashSize))
                throw new ArgumentOutOfRangeException("hashSize", "hashSize must be contained within MurmurHash2.ValidHashSizes.");

            _Seed = seed;
        }


        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(UnifiedData data)
        {
            byte[] hash = null;

            switch (HashSize)
            {
                case 32:
                {        
                    const UInt32 m = unchecked((UInt32) MixConstant);

                    UInt32 h = (UInt32) Seed ^ (UInt32) data.Length;

                    data.ForEachGroup(4, 
                        (dataGroup, position, length) => {
                            ProcessGroup(ref h, m, dataGroup, position, length);
                        }, 
                        (remainder, position, length) => {
                            ProcessRemainder(ref h, m, remainder, position, length);
                        });

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

                    UInt64 h = Seed ^ ((UInt64) data.Length * m);
            
                    data.ForEachGroup(8, 
                        (dataGroup, position, length) => {
                            ProcessGroup(ref h, m, dataGroup, position, length);
                        },
                        (remainder, position, length) => {
                            ProcessRemainder(ref h, m, remainder, position, length);
                        });
 
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

#if !NET40 || INCLUDE_ASYNC
        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override async Task<byte[]> ComputeHashAsyncInternal(UnifiedData data)
        {
            byte[] hash = null;

            switch (HashSize)
            {
                case 32:
                {        
                    const UInt32 m = unchecked((UInt32) MixConstant);

                    UInt32 h = (UInt32) Seed ^ (UInt32) data.Length;

                    await data.ForEachGroupAsync(4, 
                        (dataGroup, position, length) => {
                            ProcessGroup(ref h, m, dataGroup, position, length);
                        }, 
                        (remainder, position, length) => {
                            ProcessRemainder(ref h, m, remainder, position, length);
                        }).ConfigureAwait(false);

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

                    UInt64 h = Seed ^ ((UInt64) data.Length * m);
            
                    await data.ForEachGroupAsync(8, 
                        (dataGroup, position, length) => {
                            ProcessGroup(ref h, m, dataGroup, position, length);
                        },
                        (remainder, position, length) => {
                            ProcessRemainder(ref h, m, remainder, position, length);
                        }).ConfigureAwait(false);
 
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
#endif


#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
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

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
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

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
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


#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
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
