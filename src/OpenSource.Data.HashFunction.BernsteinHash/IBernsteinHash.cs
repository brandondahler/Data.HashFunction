using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.BernsteinHash
{
    /// <summary>
    /// Implementation of Bernstein hash as specified at http://www.eternallyconfuzzled.com/tuts/algorithms/jsw_tut_hashing.aspx#djb.
    /// 
    /// From http://www.eternallyconfuzzled.com/tuts/algorithms/jsw_tut_hashing.aspx#djb:
    /// "
    ///   Dan Bernstein created this algorithm and posted it in a newsgroup. 
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
    /// <seealso cref="IStreamableHashFunction" />
    public interface IBernsteinHash
        : IStreamableHashFunction
    {

    }
}
