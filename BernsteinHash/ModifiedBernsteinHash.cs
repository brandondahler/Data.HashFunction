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
    /// Implementation of Modified Bernstein hash as specified at http://www.eternallyconfuzzled.com/tuts/algorithms/jsw_tut_hashing.aspx#djb2.
    /// 
    /// "
    /// [The Modified Bernstein hash is a] minor update to <see cref="BernsteinHash">Bernstein's hash</see> replaces addition with 
    ///   XOR for the combining step. 
    /// This change does not appear to be well known or often used, the original algorithm is still 
    ///   recommended by nearly everyone, but the new algorithm typically results in a better distribution.
    ///   
    /// Dan Bernstein created [the Bernstein hash] algorithm and posted it in a newsgroup. 
    ///   It is known by many as the Chris Torek hash because Chris went a long way toward popularizing it. 
    ///   Since then it has been used successfully by many, but despite that the algorithm itself is not very sound 
    ///     when it comes to avalanche and permutation of the internal state. 
    ///   It has proven very good for small character keys, where it can outperform algorithms that result 
    ///     in a more random distribution.    
    ///     
    ///   Bernstein's hash should be used with caution. 
    ///   It performs very well in practice, for no apparently known reasons 
    ///     (much like how the constant 33 does better than more logical constants for no apparent reason), 
    ///     but in theory it is not up to snuff. 
    ///   Always test this function with sample data for every application to ensure that it does not encounter 
    ///     a degenerate case and cause excessive collisions.
    /// "
    /// </summary>
    public class ModifiedBernsteinHash
#if NET45
        : HashFunctionAsyncBase
#else
        : HashFunctionBase
#endif
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModifiedBernsteinHash"/> class.
        /// </summary>
        /// <remarks>
        /// <see cref="HashFunctionBase.HashSize "/> defaults to 32 bits.
        /// </remarks>
        /// <inheritdoc cref="HashFunctionBase(int)" />
        public ModifiedBernsteinHash()
            : base(32)
        { 
            
        }


        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(UnifiedData data)
        {
            UInt32 h = 0;

            data.ForEachRead((dataBytes, position, length) => {
                ProcessBytes(ref h, dataBytes, position, length);
            });
            
            return BitConverter.GetBytes(h);
        }
        
#if NET45
        /// <inheritdoc />
        protected override async Task<byte[]> ComputeHashAsyncInternal(UnifiedData data)
        {
            UInt32 h = 0;

            await data.ForEachReadAsync((dataBytes, position, length) => {
                ProcessBytes(ref h, dataBytes, position, length);
            }).ConfigureAwait(false);

            return BitConverter.GetBytes(h);
        }
#endif


#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static void ProcessBytes(ref UInt32 h, byte[] dataBytes, int position, int length)
        {
            for (var x = position; x < position + length; ++x)
                h = (33 * h) ^ dataBytes[x];
        }
    }
}
