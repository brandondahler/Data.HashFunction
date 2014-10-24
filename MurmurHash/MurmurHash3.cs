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
    /// Implementation of MurmurHash3 as specified at https://code.google.com/p/smhasher/source/browse/trunk/MurmurHash3.cpp 
    ///   and https://code.google.com/p/smhasher/wiki/MurmurHash3.
    /// </summary>
    public class MurmurHash3
        : HashFunctionAsyncBase
    {
        /// <summary>
        /// Seed value for hash calculation.
        /// </summary>
        /// <value>
        /// The seed value for hash calculation.
        /// </value>
        public UInt32 Seed { get { return _Seed; } }


        /// <summary>
        /// The list of possible hash sizes that can be provided to the <see cref="MurmurHash3" /> constructor.
        /// </summary>
        /// <value>
        /// The list of valid hash sizes.
        /// </value>
        public static IEnumerable<int> ValidHashSizes { get { return _ValidHashSizes; } }


        /// <summary>
        /// Constant c1 for 32-bit calculation as defined by MurmurHash3 specification.
        /// </summary>
        protected const UInt32 c1_32 = 0xcc9e2d51;

        /// <summary>
        /// Constant c2 for 32-bit calculation as defined by MurmurHash3 specification.
        /// </summary>
        protected const UInt32 c2_32 = 0x1b873593;


        /// <summary>
        /// Constant c1 for 128-bit calculation as defined by MurMurHash3 specification.
        /// </summary>
        protected const UInt64 c1_128 = 0x87c37b91114253d5;

        /// <summary>
        /// Constant c2 for 128-bit calculation as defined by MurMurHash3 specification.
        /// </summary>
        protected const UInt64 c2_128 = 0x4cf5ad432745937f;


        private readonly UInt32 _Seed;

        private static readonly IEnumerable<int> _ValidHashSizes = new[] { 32, 128 };



        /// <remarks>
        /// Defaults <see cref="Seed" /> to 0. <inheritdoc cref="MurmurHash3(UInt32)" />
        /// </remarks>
        /// <inheritdoc cref="MurmurHash3(UInt32)" />
        public MurmurHash3()
            : this(0U)
        {

        }

        /// <remarks>
        /// Defaults <see cref="Seed" /> to 0.
        /// </remarks>
        /// <inheritdoc cref="MurmurHash3(int, UInt32)" />
        public MurmurHash3(int hashSize)
            : this(hashSize, 0U)
        {

        }

        /// <remarks>
        /// Defaults <see cref="HashFunctionBase.HashSize" /> to 32.
        /// </remarks>
        /// <inheritdoc cref="MurmurHash3(int, UInt32)" />
        public MurmurHash3(UInt32 seed)
            : this(32, seed)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MurmurHash3"/> class.
        /// </summary>
        /// <param name="hashSize"><inheritdoc cref="HashFunctionBase(int)" /></param>
        /// <param name="seed"><inheritdoc cref="Seed" /></param>
        /// <exception cref="System.ArgumentOutOfRangeException">hashSize;hashSize must be contained within MurmurHash3.ValidHashSizes.</exception>
        /// <inheritdoc cref="HashFunctionBase(int)" />
        public MurmurHash3(int hashSize, UInt32 seed)
            : base(hashSize)
        {
            if (!ValidHashSizes.Contains(hashSize))
                throw new ArgumentOutOfRangeException("hashSize", "hashSize must be contained within MurmurHash3.ValidHashSizes.");

            _Seed = seed;
        }



        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(UnifiedData data)
        {
            switch (HashSize)
            {
                case 32:
                {
                    UInt32 h1 = Seed;
                    int dataCount = 0;


                    data.ForEachGroup(4, 
                        (dataGroup, position, length) => {
                            ProcessGroup(ref h1, dataGroup, position, length);

                            dataCount += length;
                        },
                        (remainder, position, length) => {
                            ProcessRemainder(ref h1, remainder, position, length);

                            dataCount += length;
                        });
            

                    h1 ^= (UInt32) dataCount;
                    Mix(ref h1);

                    return BitConverter.GetBytes(h1);
                }

                case 128:
                {
                    UInt64 h1 = (UInt64) Seed;
                    UInt64 h2 = (UInt64) Seed;

                    int dataCount = 0;

            
                    data.ForEachGroup(16, 
                        (dataGroup, position, length) => {
                            ProcessGroup(ref h1, ref h2, dataGroup, position, length);

                            dataCount += length;
                        },
                        (remainder, position, length) => {
                            ProcessRemainder(ref h1, ref h2, remainder, position, length);

                            dataCount += length;
                        });


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

                    return hashBytes;
                }

                default:
                    throw new InvalidOperationException("HashSize set to an invalid value.");
            }
        }

        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override async Task<byte[]> ComputeHashAsyncInternal(UnifiedData data)
        {
            switch (HashSize)
            {
                case 32:
                {
                    UInt32 h1 = Seed;
                    int dataCount = 0;


                    await data.ForEachGroupAsync(4, 
                        (dataGroup, position, length) => {
                            ProcessGroup(ref h1, dataGroup, position, length);

                            dataCount += length;
                        },
                        (remainder, position, length) => {
                            ProcessRemainder(ref h1, remainder, position, length);

                            dataCount += length;
                        }).ConfigureAwait(false);
            

                    h1 ^= (UInt32) dataCount;
                    Mix(ref h1);

                    return BitConverter.GetBytes(h1);
                }

                case 128:
                {
                    UInt64 h1 = (UInt64) Seed;
                    UInt64 h2 = (UInt64) Seed;

                    int dataCount = 0;

            
                    await data.ForEachGroupAsync(16, 
                        (dataGroup, position, length) => {
                            ProcessGroup(ref h1, ref h2, dataGroup, position, length);

                            dataCount += length;
                        },
                        (remainder, position, length) => {
                            ProcessRemainder(ref h1, ref h2, remainder, position, length);

                            dataCount += length;
                        }).ConfigureAwait(false);


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

                    return hashBytes;
                }

                default:
                    throw new InvalidOperationException("HashSize set to an invalid value.");
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ProcessGroup(ref UInt32 h1, byte[] dataGroup, int position, int length)
        {
            for (var x = position; x < position + length; x += 4)
            {
                UInt32 k1 = BitConverter.ToUInt32(dataGroup, x);

                k1 *= c1_32;
                k1 = k1.RotateLeft(15);
                k1 *= c2_32;

                h1 ^= k1;
                h1 = h1.RotateLeft(13);
                h1 = (h1 * 5) + 0xe6546b64;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ProcessGroup(ref UInt64 h1, ref UInt64 h2, byte[] dataGroup, int position, int length)
        {
            for (var x = position; x < position + length; x += 16)
            {
                UInt64 k1 = BitConverter.ToUInt64(dataGroup, 0);
                UInt64 k2 = BitConverter.ToUInt64(dataGroup, 8);

                k1 *= c1_128;
                k1 = k1.RotateLeft(31);
                k1 *= c2_128;
                h1 ^= k1;

                h1 = h1.RotateLeft(27);
                h1 += h2;
                h1 = (h1 * 5) + 0x52dce729;

                k2 *= c2_128;
                k2 = k2.RotateLeft(33);
                k2 *= c1_128;
                h2 ^= k2;

                h2 = h2.RotateLeft(31);
                h2 += h1;
                h2 = (h2 * 5) + 0x38495ab5;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ProcessRemainder(ref UInt32 h1, byte[] remainder, int position, int length)
        {
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
            k2 = k2.RotateLeft(15);
            k2 *= c2_32;
            h1 ^= k2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ProcessRemainder(ref UInt64 h1, ref UInt64 h2, byte[] remainder, int position, int length)
        {
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
                    k2  = k2.RotateLeft(33); 
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
            k1  = k1.RotateLeft(31);
            k1 *= c2_128;
            h1 ^= k1;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Mix(ref UInt32 h)
        {
            h ^= h >> 16;
            h *= 0x85ebca6b;
            h ^= h >> 13;
            h *= 0xc2b2ae35;
            h ^= h >> 16;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Mix(ref UInt64 k)
        {
            k ^= k >> 33;
            k *= 0xff51afd7ed558ccd;
            k ^= k >> 33;
            k *= 0xc4ceb9fe1a85ec53;
            k ^= k >> 33;
        }
    }
}
