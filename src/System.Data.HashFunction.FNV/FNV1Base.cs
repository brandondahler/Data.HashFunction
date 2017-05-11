using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Data.HashFunction.Utilities.IntegerManipulation;
using System.Data.HashFunction.Utilities.UnifiedData;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Abstract implementation of Fowler–Noll–Vo hash function (FNV-1 and FNV-1a) as specified at http://www.isthe.com/chongo/tech/comp/fnv/index.html.
    /// </summary>
    public abstract class FNV1Base
        : HashFunctionAsyncBase
    {
        /// <summary>
        /// The list of possible hash sizes that can be provided to the <see cref="FNV1Base" /> constructor.
        /// </summary>
        /// <value>
        /// The enumerable set of valid hash sizes.
        /// </value>
        public static IEnumerable<int> ValidHashSizes { get { return HashParameters.Keys; } }


        /// <summary>
        /// Dictionary with keys matching the possible hash sizes and values of FNV_prime and offset_basis.
        /// </summary>
        /// <value>
        /// The concurrent dictionary of hash parameters.
        /// </value>
        /// <remarks>
        /// <para>
        /// It is acceptable to add, remove, or change items contained within this dictionary.
        /// Changes will only apply to instances constructed after the change.
        /// </para>
        /// <para>
        /// Dictionary is guaranteed to be thread-safe.
        /// </para>
        /// </remarks>
        public static IDictionary<int, FNVPrimeOffset> HashParameters { get { return _HashParameters; }}


        /// <summary>
        /// Parameters as defined by the FNV specifications.
        /// </summary>
        private static readonly ConcurrentDictionary<int, FNVPrimeOffset> _HashParameters =
            new ConcurrentDictionary<int, FNVPrimeOffset>(
                new Dictionary<int, FNVPrimeOffset>() { 
                    { 
                        32, 
                        new FNVPrimeOffset(32,
                            new BigInteger(16777619), 
                            new BigInteger(2166136261))
                    },
                    { 
                        64, 
                        new FNVPrimeOffset(64,
                            new BigInteger(1099511628211), 
                            new BigInteger(14695981039346656037))
                    },
                    { 
                        128, 
                        new FNVPrimeOffset(128,
                            BigInteger.Parse("309485009821345068724781371"), 
                            BigInteger.Parse("144066263297769815596495629667062367629"))
                    },
                    { 
                        256, 
                        new FNVPrimeOffset(256,
                            BigInteger.Parse("374144419156711147060143317175368453031918731002211"), 
                            BigInteger.Parse("100029257958052580907070968620625704837092796014241193945225284501741471925557"))
                    },
                    { 
                        512, 
                        new FNVPrimeOffset(512,
                            BigInteger.Parse("35835915874844867368919076489095108449946327955754392558399825615420669938882575126094039892345713852759"), 
                            BigInteger.Parse("9659303129496669498009435400716310466090418745672637896108374329434462657994582932197716438449813051892206539805784495328239340083876191928701583869517785"))
                    },
                    { 
                        1024, 
                        new FNVPrimeOffset(1024,
                            BigInteger.Parse("5016456510113118655434598811035278955030765345404790744303017523831112055108147451509157692220295382716162651878526895249385292291816524375083746691371804094271873160484737966720260389217684476157468082573"), 
                            BigInteger.Parse("14197795064947621068722070641403218320880622795441933960878474914617582723252296732303717722150864096521202355549365628174669108571814760471015076148029755969804077320157692458563003215304957150157403644460363550505412711285966361610267868082893823963790439336411086884584107735010676915"))
                    }
                });


        /// <inheritdoc class="FNV1Base(int)" />
        protected FNV1Base()
            : this(64)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FNV1Base"/> class.
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">hashSize;hashSize must be contained within FNV1Base.ValidHashSizes, no hash parameters for that length specified.</exception>
        /// <exception cref="System.ArgumentException">
        /// </exception>
        /// <inheritdoc cref="HashFunctionBase(int)" />
        protected FNV1Base(int hashSize)
            : base(hashSize)
        {
            if (!ValidHashSizes.Contains(hashSize))
            {
                throw new ArgumentOutOfRangeException(
                    "hashSize", 
                    "hashSize must be contained within FNV1Base.ValidHashSizes, no hash parameters for that length specified.");
            }


            if (HashParameters[hashSize].Prime.Count != hashSize / 32)
            {
                throw new ArgumentException(
                    string.Format("HashParameters[{0}].Prime should contain exactly {1} items.", hashSize, hashSize / 32),
                    string.Format("HashParameters[{0}].Prime", hashSize));
            }
                    
            if (HashParameters[hashSize].Offset.Count != hashSize / 32)
            {
                throw new ArgumentException(
                    string.Format("HashParameters[{0}].Offset should contain exactly {1} items.", hashSize, hashSize / 32),
                    string.Format("HashParameters[{0}].Offset", hashSize));
            }
        }


        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(UnifiedData data)
        {
            var prime = HashParameters[HashSize].Prime;
            var offset = HashParameters[HashSize].Offset;

            // Handle 32-bit and 64-bit cases in a strongly-typed manner for performance
            if (HashSize == 32)
            {
                var hash = offset[0];

                data.ForEachRead((dataBytes, position, length) => {
                    ProcessBytes32(ref hash, prime[0], dataBytes, position, length);
                });

                return BitConverter.GetBytes(hash);

            } else if (HashSize == 64) {
                var hash = ((UInt64) offset[1] << 32) | offset[0];
                var prime64 = ((UInt64) prime[1] << 32) | prime[0];


                data.ForEachRead((dataBytes, position, length) => {
                    ProcessBytes64(ref hash, prime64, dataBytes, position, length);
                });

                return BitConverter.GetBytes(hash);
            }


            // Process extended-sized FNV.
            {
                var hash = offset.ToArray();


                data.ForEachRead((dataBytes, position, length) => {
                    ProcessBytes(ref hash, prime, dataBytes, position, length);
                });

                return hash.ToBytes()
                    .ToArray();
            }
        }
        
        /// <inheritdoc />
        protected override async Task<byte[]> ComputeHashAsyncInternal(UnifiedData data)
        {
            var prime = HashParameters[HashSize].Prime;
            var offset = HashParameters[HashSize].Offset;

            // Handle 32-bit and 64-bit cases in a strongly-typed manner for performance
            if (HashSize == 32)
            {
                var hash = offset[0];

                await data.ForEachReadAsync(
                    (dataBytes, position, length) => {
                        ProcessBytes32(ref hash, prime[0], dataBytes, position, length);
                    }).ConfigureAwait(false);

                return BitConverter.GetBytes(hash);

            } else if (HashSize == 64) {
                var hash = ((UInt64) offset[1] << 32) | offset[0];
                var prime64 = ((UInt64) prime[1] << 32) | prime[0];


                await data.ForEachReadAsync(
                    (dataBytes, position, length) => {
                        ProcessBytes64(ref hash, prime64, dataBytes, position, length);
                    }).ConfigureAwait(false);

                return BitConverter.GetBytes(hash);
            }


            // Process extended-sized FNV.
            {
                var hash = offset.ToArray();


                await data.ForEachReadAsync(
                    (dataBytes, position, length) => {
                        ProcessBytes(ref hash, prime, dataBytes, position, length);
                    }).ConfigureAwait(false);

                return hash.ToBytes()
                    .ToArray();
            }
        }



        /// <summary>
        /// Apply 32-bit FNV algorithm on all data supplied.
        /// </summary>
        /// <param name="hash">Hash value before calculations.</param>
        /// <param name="prime">FNV prime to use for calculations.</param>
        /// <param name="data">Data to process.</param>
        /// <param name="position">The starting index of the data array.</param>
        /// <param name="length">The length of the data in the data array, starting from the position parameter.</param>
        protected abstract void ProcessBytes32(ref UInt32 hash, UInt32 prime, byte[] data, int position, int length);

        /// <summary>
        /// Apply 64-bit FNV algorithm on all data supplied.
        /// </summary>
        /// <param name="hash">Hash value before calculations.</param>
        /// <param name="prime">FNV prime to use for calculations.</param>
        /// <param name="data">Data to process.</param>
        /// <param name="position">The starting index of the data array.</param>
        /// <param name="length">The length of the data in the data array, starting from the position parameter.</param>
        protected abstract void ProcessBytes64(ref UInt64 hash, UInt64 prime, byte[] data, int position, int length);

        /// <summary>
        /// Apply FNV algorithm on all data supplied.
        /// </summary>
        /// <param name="hash">Hash value before calculations.</param>
        /// <param name="prime">FNV prime to use for calculations.</param>
        /// <param name="data">Data to process.</param>
        /// <param name="position">The starting index of the data array.</param>
        /// <param name="length">The length of the data in the data array, starting from the position parameter.</param>
        protected abstract void ProcessBytes(ref UInt32[] hash, IReadOnlyList<UInt32> prime, byte[] data, int position, int length);
    }
}
