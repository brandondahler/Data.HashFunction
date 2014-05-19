using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    public abstract class FNVBase
        : HashFunctionBase
    {
        public override int HashSize
        {
            get { return base.HashSize; }
            set
            {
                if (!HashParameters.ContainsKey(value))
                    throw new NotImplementedException("No HashParameters implemented for this HashSize.");

                base.HashSize = value;
            }
        }

        public override IEnumerable<int> ValidHashSizes
        {
            get { return new[] { 32, 64, 128, 256, 512, 1024 }; }
        }

        /// <summary>
        /// Dictionary with keys matching the possible hash sizes and 
        /// values of FNV_prime and offset_basis.
        /// 
        /// Upon FNVBase construction, these are converted down to arrays of 32-bit constants.
        /// </summary>
        /// <remarks>
        /// FNV_prime and offset_basis are BigIntegers to allow storage of large constants.
        /// 
        /// Note that BigInteger construction can take a long time, overriders should ensure 
        /// BigIntegers are not being constructed every time HashParameters is being used.
        /// </remarks>
        protected virtual IDictionary<int, PrimeOffset> HashParameters { get { return _HashParameters; }}

        private static readonly IDictionary<int, PrimeOffset> _HashParameters = 
            new Dictionary<int, PrimeOffset>() { 
                { 
                    32, new PrimeOffset() {
                        Prime = new BigInteger(16777619), 
                        Offset = new BigInteger(2166136261)
                    }
                },
                { 
                    64, new PrimeOffset() {
                        Prime = new BigInteger(1099511628211), 
                        Offset = new BigInteger(14695981039346656037)
                    }
                },
                { 
                    128, new PrimeOffset() {
                        Prime = BigInteger.Parse("309485009821345068724781371"), 
                        Offset = BigInteger.Parse("144066263297769815596495629667062367629")
                    }
                },
                { 
                    256, new PrimeOffset() {
                        Prime = BigInteger.Parse("374144419156711147060143317175368453031918731002211"), 
                        Offset = BigInteger.Parse("100029257958052580907070968620625704837092796014241193945225284501741471925557")
                    }
                },
                { 
                    512, new PrimeOffset() {
                        Prime = BigInteger.Parse("35835915874844867368919076489095108449946327955754392558399825615420669938882575126094039892345713852759"), 
                        Offset = BigInteger.Parse("9659303129496669498009435400716310466090418745672637896108374329434462657994582932197716438449813051892206539805784495328239340083876191928701583869517785")
                    }
                },
                { 
                    1024, new PrimeOffset() {
                        Prime = BigInteger.Parse("5016456510113118655434598811035278955030765345404790744303017523831112055108147451509157692220295382716162651878526895249385292291816524375083746691371804094271873160484737966720260389217684476157468082573"), 
                        Offset = BigInteger.Parse("14197795064947621068722070641403218320880622795441933960878474914617582723252296732303717722150864096521202355549365628174669108571814760471015076148029755969804077320157692458563003215304957150157403644460363550505412711285966361610267868082893823963790439336411086884584107735010676915")
                    } 
                }
            };

        protected readonly IDictionary<int, UInt32[]> PrecomputedOffsets = new Dictionary<int, UInt32[]>();
        protected readonly IDictionary<int, UInt32[]> PrecomputedPrimes = new Dictionary<int, UInt32[]>();


        protected class PrimeOffset
        {
            public BigInteger Prime { get; set; }
            public BigInteger Offset { get; set; }
        }

        protected FNVBase(int defaultHashSize)
            : base(defaultHashSize)
        {
            // Verify all hash sizes have parameters defined
            foreach (var hashSize in ValidHashSizes)
            {
                if (!HashParameters.ContainsKey(hashSize))
                    throw new NotImplementedException("No hash parameters specified for hash size " + hashSize + ".");
            }

            // Precompute all hash parameters
            foreach (var kvp in HashParameters)
            {
                // Precompute offset
                PrecomputedOffsets[kvp.Key] = BigIntegerToUInt32s(kvp.Value.Offset, kvp.Key);
                PrecomputedPrimes[kvp.Key] =  BigIntegerToUInt32s(kvp.Value.Prime, kvp.Key);
            }
        }


        /// <summary>
        /// Converts a BigInteger to an array of UInt32s.
        /// </summary>
        /// <param name="value">BigInteger to be converted.</param>
        /// <param name="bitSize">Expected bit size of resulting array.</param>
        protected static UInt32[] BigIntegerToUInt32s(BigInteger value, int bitSize)
        {
            var uint32s = new UInt32[bitSize / 32];
            var bigIntegerBytes = value.ToByteArray();

            Buffer.BlockCopy(bigIntegerBytes, 0, uint32s, 0, 
                (uint32s.Length * 4 <= bigIntegerBytes.Length ? 
                    uint32s.Length * 4 : bigIntegerBytes.Length));

            return uint32s;
        }

        /// <summary>
        /// Multiplies hash by prime and stores result into hash.
        /// </summary>
        /// <param name="hash">Value to be multiplied</param>
        /// <param name="prime">Multiplier</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static void ExtendedMultiply(UInt32[] hash, UInt32[] prime)
        {
            // Temporary array to hold the results of 32-bit multipication.
            var hashProduct = new UInt32[hash.Length];

            // Bottom of equation
            for (int y = 0; y < prime.Length; ++y)
            {
                // Skip multiplying things by zero
                if (prime[y] == 0)
                    continue;

                UInt64 productResult = 0;

                // Top of equation
                for (int x = 0; x < hash.Length; ++x)
                {
                    if (x + y >= hashProduct.Length)
                        break;

                    var carryOver = (UInt32) (productResult >> 32);

                    productResult = hashProduct[x + y] + (((UInt64) prime[y]) * hash[x]) + carryOver;
                    hashProduct[x + y] = (UInt32) productResult;
                }
            }

            Buffer.BlockCopy(hashProduct, 0, hash, 0, hash.Length * 4);
        }

        /// <summary>
        /// Converts array of UInt32 to byte array as if it were a single integer.
        /// </summary>
        /// <param name="hash">Hash to convert to byte array.</param>
        /// <returns>Bytes represending the UInt32 array.</returns>
        protected static byte[] UInt32sToBytes(UInt32[] hash)
        {
            var result = new byte[hash.Length * 4];
            Buffer.BlockCopy(hash, 0, result, 0, result.Length);

            return result;
        }
    }
}
