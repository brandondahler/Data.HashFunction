using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.SpookyHash
{
    /// <summary>
    /// Implementation of SpookyHash V1 as specified at http://burtleburtle.net/bob/hash/spooky.html.
    /// 
    /// This hash function has been superseded by <see cref="ISpookyHashV2"/> due to a loss of entropy from a 
    ///   coding error in the original specification.  It still passes the hash function tests the creator set for it,
    ///   but it is preferred that SpookyHash V2 is used.
    /// </summary>
    [Obsolete("SpookyHashV1 has known issues, use SpookyHashV2.")]
    public interface ISpookyHashV1
        : ISpookyHash
    {

    }
}
