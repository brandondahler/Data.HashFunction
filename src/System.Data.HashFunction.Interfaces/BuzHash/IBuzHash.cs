using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.BuzHash
{
    /// <summary>
    /// Implementation of BuzHash as specified at http://www.serve.net/buz/hash.adt/java.002.html.
    /// 
    /// Relies on a table of 256 (preferably distinct) 64-bit integers.
    /// Also can be set to use left or right rotation when running the rotate step.
    /// </summary>
    /// <seealso cref="IHashFunctionAsync" />
    public interface IBuzHash
        : IHashFunctionAsync
    {

    }
}
