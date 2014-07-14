using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
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
        : HashFunctionBase
    {
        /// <inheritdoc/>
        public override int HashSize
        {
            get { return base.HashSize; }
            set
            {
                if (!HashParameters.ContainsKey(value))
                    throw new ArgumentOutOfRangeException("value", "No HashParameters implemented for this HashSize.");

                base.HashSize = value;
            }
        }

        /// <inheritdoc/>
        public override IEnumerable<int> ValidHashSizes
        {
            get { return HashParameters.Keys; }
        }


        /// <summary>
        /// Dictionary with keys matching the possible hash sizes and 
        /// values of FNV_prime and offset_basis.
        /// 
        /// Upon <see cref="FNV1Base" /> construction, these are converted down to arrays of 32-bit constants.
        /// </summary>
        /// <remarks>
        /// FNV_prime and offset_basis are <see cref="BigInteger"/>s to allow storage of large constants.
        /// 
        /// Note that BigInteger construction can take a long time, overriders should ensure 
        /// BigIntegers are not being constructed every time HashParameters is being used by
        ///   returning a reference to a static, readonly dictionary.
        /// </remarks>
        protected virtual IDictionary<int, FNVPrimeOffset> HashParameters { get { return _HashParameters; }}


        /// <summary>
        /// Parameters as defined by the FNV specifications.
        /// </summary>
        private static readonly IDictionary<int, FNVPrimeOffset> _HashParameters =
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
            };


        /// <summary>
        /// Creates new <see cref="FNV1Base"/> instance.
        /// </summary>
        /// <param name="defaultHashSize">Default hash size to pass to <see cref="HashFunctionBase" /></param>
        /// <remarks>
        /// Precomputes offsets and primes for the child type.  
        /// Assumes values that already exist in precomputed storage are the correct values for the child type being constructed.
        /// </remarks>
        protected FNV1Base(int defaultHashSize)
            : base(defaultHashSize)
        {
            // Verify all hash sizes have parameters defined
            foreach (var hashSize in ValidHashSizes)
            {
                if (!HashParameters.ContainsKey(hashSize))
                {
                    throw new InvalidOperationException(
                        "No hash parameters specified for hash size " + hashSize + " despite being listed as valid.");
                }
            }
        }
    }
}
