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
    /// Implements xxHash as specified at https://code.google.com/p/xxhash/source/browse/trunk/xxhash.c and 
    ///   https://code.google.com/p/xxhash/.
    /// </summary>
    public class xxHash
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
        public UInt64 InitVal { get; set; }

        /// <summary>
        /// The list of possible hash sizes that can be provided to the <see cref="xxHash" /> constructor.
        /// </summary>
        /// <value>
        /// The list of valid hash sizes.
        /// </value>
        public static IEnumerable<int> ValidHashSizes { get { return _validHashSizes; } }


#if !NET40
        private static readonly IReadOnlyList<UInt32> _primes32 = 
#else
        private static readonly IList<UInt32> _primes32 = 
#endif
            new[] {
                2654435761U,
                2246822519U,
                3266489917U,
                 668265263U,
                 374761393U
            };

#if !NET40
        private static readonly IReadOnlyList<UInt64> _primes64 = 
#else
        private static readonly IList<UInt64> _primes64 = 
#endif
            new[] {
                11400714785074694791UL,
                14029467366897019727UL,
                 1609587929392839161UL,
                 9650029242287828579UL,
                 2870177450012600261UL
            };

        private static readonly IEnumerable<int> _validHashSizes = new[] { 32, 64 };



        /// <remarks>
        /// Defaults <see cref="HashFunctionBase.HashSize" /> to 32.  <inheritdoc cref="xxHash(int)" />
        /// </remarks>
        /// <inheritdoc cref="xxHash(int)" />
        public xxHash()
            : this(32)
        {

        }

        /// <remarks>
        /// Defaults <see cref="InitVal" /> to 0.
        /// </remarks>
        /// <inheritdoc cref="xxHash(int, UInt64)" />
        public xxHash(int hashSize)
            :this (hashSize, 0U)
        {

        }
        

        /// <summary>
        /// Initializes a new instance of the <see cref="xxHash" /> class.
        /// </summary>
        /// <param name="hashSize"><inheritdoc cref="HashFunctionBase.HashSize" /></param>
        /// <param name="initVal"><inheritdoc cref="InitVal" /></param>
        /// <exception cref="System.ArgumentOutOfRangeException">hashSize;hashSize must be contained within xxHash.ValidHashSizes</exception>
        /// <inheritdoc cref="HashFunctionBase(int)" />
        public xxHash(int hashSize, UInt64 initVal)
            : base(hashSize)
        {
            if (!ValidHashSizes.Contains(hashSize))
                throw new ArgumentOutOfRangeException("hashSize", "hashSize must be contained within xxHash.ValidHashSizes");

            InitVal = initVal;
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
                    var h = ((UInt32) InitVal) + _primes32[4];

                    ulong dataCount = 0;
                    byte[] remainder = null;


                    var initValues = new[] {
                        ((UInt32) InitVal) + _primes32[0] + _primes32[1],
                        ((UInt32) InitVal) + _primes32[1],
                        ((UInt32) InitVal),
                        ((UInt32) InitVal) - _primes32[0]
                    };

                    data.ForEachGroup(16, 
                        (dataGroup, position, length) => {
                            for (int x = position; x < position + length; x += 16)
                            {
                                for (var y = 0; y < 4; ++y)
                                {
                                    initValues[y] += BitConverter.ToUInt32(dataGroup, x + (y * 4)) * _primes32[1];
                                    initValues[y] = initValues[y].RotateLeft(13);
                                    initValues[y] *= _primes32[0];
                                }
                            }

                            dataCount += (ulong)length;
                        },
                        (remainderData, position, length) => {
                            remainder = new byte[length];
                            Array.Copy(remainderData, position, remainder, 0, length);

                            dataCount += (ulong)length;
                        });


                    PostProcess(ref h, initValues, dataCount, remainder);

                    hash = BitConverter.GetBytes(h);
                    break;
                }

                case 64:
                {
                     var h = InitVal + _primes64[4];

                    ulong dataCount = 0;
                    byte[] remainder = null;

                    var initValues = new[] {
                        InitVal + _primes64[0] + _primes64[1],
                        InitVal + _primes64[1],
                        InitVal,
                        InitVal - _primes64[0]
                    };


                    data.ForEachGroup(32, 
                        (dataGroup, position, length) => {

                            for (var x = position; x < position + length; x += 32)
                            {
                                for (var y = 0; y < 4; ++y)
                                {
                                    initValues[y] += BitConverter.ToUInt64(dataGroup, x + (y * 8)) * _primes64[1];
                                    initValues[y] = initValues[y].RotateLeft(31);
                                    initValues[y] *= _primes64[0];
                                }
                            }

                            dataCount += (ulong) length;
                        },
                        (remainderData, position, length) => {
                            remainder = new byte[length];
                            Array.Copy(remainderData, position, remainder, 0, length);

                            dataCount += (ulong) length;
                        });


                    PostProcess(ref h, initValues, dataCount, remainder);

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
                    var h = ((UInt32) InitVal) + _primes32[4];

                    ulong dataCount = 0;
                    byte[] remainder = null;


                    var initValues = new[] {
                        ((UInt32) InitVal) + _primes32[0] + _primes32[1],
                        ((UInt32) InitVal) + _primes32[1],
                        ((UInt32) InitVal),
                        ((UInt32) InitVal) - _primes32[0]
                    };

                    await data.ForEachGroupAsync(16, 
                        (dataGroup, position, length) => {
                            for (var x = position; x < position + length; x += 16)
                            {
                                for (var y = 0; y < 4; ++y)
                                {
                                    initValues[y] += BitConverter.ToUInt32(dataGroup, x + (y * 4)) * _primes32[1];
                                    initValues[y] = initValues[y].RotateLeft(13);
                                    initValues[y] *= _primes32[0];
                                }
                            }

                            dataCount += (ulong) length;
                        },
                        (remainderData, position, length) => {
                            remainder = new byte[length];
                            Array.Copy(remainderData, position, remainder, 0, length);

                            dataCount += (ulong) length;
                        }).ConfigureAwait(false);

                    PostProcess(ref h, initValues, dataCount, remainder);

                    hash = BitConverter.GetBytes(h);
                    break;
                }

                case 64:
                {
                     var h = InitVal + _primes64[4];

                    ulong dataCount = 0;
                    byte[] remainder = null;

                    var initValues = new[] {
                        InitVal + _primes64[0] + _primes64[1],
                        InitVal + _primes64[1],
                        InitVal,
                        InitVal - _primes64[0]
                    };


                    await data.ForEachGroupAsync(32, 
                        (dataGroup, position, length) => {
                            for (var x = position; x < position + length; x += 32)
                            {
                                for (var y = 0; y < 4; ++y)
                                {
                                    initValues[y] += BitConverter.ToUInt64(dataGroup, x + (y * 8)) * _primes64[1];
                                    initValues[y] = initValues[y].RotateLeft(31);
                                    initValues[y] *= _primes64[0];
                                }
                            }

                            dataCount += (ulong) length;
                        },
                        (remainderData, position, length) => {
                            remainder = new byte[length];
                            Array.Copy(remainderData, position, remainder, 0, length);

                            dataCount += (ulong) remainder.Length;
                        }).ConfigureAwait(false);


                    PostProcess(ref h, initValues, dataCount, remainder);

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
        private static void PostProcess(ref UInt32 h, UInt32[] initValues, ulong dataCount, byte[] remainder)
        {
            if (dataCount >= 16)
            {
                h = initValues[0].RotateLeft(1) + 
                    initValues[1].RotateLeft(7) + 
                    initValues[2].RotateLeft(12) + 
                    initValues[3].RotateLeft(18);
            }


            h += (UInt32) dataCount;

            if (remainder != null)
            {
                // In 4-byte chunks, process all process all full chunks
                for (int x = 0; x < remainder.Length / 4; ++x)
                {
                    h += BitConverter.ToUInt32(remainder, x * 4) * _primes32[2];
                    h  = h.RotateLeft(17) * _primes32[3];
                }


                // Process last 4 bytes in 1-byte chunks (only runs if data.Length % 4 != 0)
                for (int x = remainder.Length - (remainder.Length % 4); x < remainder.Length; ++x)
                {
                    h += (UInt32) remainder[x] * _primes32[4];
                    h  = h.RotateLeft(11) * _primes32[0];
                }
            }

            h ^= h >> 15;
            h *= _primes32[1];
            h ^= h >> 13;
            h *= _primes32[2];
            h ^= h >> 16;
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static void PostProcess(ref UInt64 h, UInt64[] initValues, ulong dataCount, byte[] remainder)
        {
            if (dataCount >= 32)
            {
                h = initValues[0].RotateLeft(1) +
                    initValues[1].RotateLeft(7) +
                    initValues[2].RotateLeft(12) +
                    initValues[3].RotateLeft(18);


                for (var x = 0; x < initValues.Length; ++x)
                {
                    initValues[x] *= _primes64[1];
                    initValues[x] = initValues[x].RotateLeft(31);
                    initValues[x] *= _primes64[0];

                    h ^= initValues[x];
                    h = (h * _primes64[0]) + _primes64[3];
                }
            }

            h += (UInt64) dataCount;

            if (remainder != null)
            { 
                // In 8-byte chunks, process all full chunks
                for (int x = 0; x < remainder.Length / 8; ++x)
                {
                    h ^= (BitConverter.ToUInt64(remainder, x * 8) * _primes64[1]).RotateLeft(31) * _primes64[0];
                    h  = (h.RotateLeft(27) * _primes64[0]) + _primes64[3];
                }


                // Process a 4-byte chunk if it exists
                if ((remainder.Length % 8) >= 4)
                {
                    h ^= ((UInt64) BitConverter.ToUInt32(remainder, remainder.Length - (remainder.Length % 8))) * _primes64[0];
                    h  = (h.RotateLeft(23) * _primes64[1]) + _primes64[2];
                }

                // Process last 4 bytes in 1-byte chunks (only runs if data.Length % 4 != 0)
                for (int x = remainder.Length - (remainder.Length % 4); x < remainder.Length; ++x)
                {
                    h ^= (UInt64) remainder[x] * _primes64[4];
                    h  = h.RotateLeft(11) * _primes64[0];
                }
            }


            h ^= h >> 33;
            h *= _primes64[1];
            h ^= h >> 29;
            h *= _primes64[2];
            h ^= h >> 32;
        }
    }
}
