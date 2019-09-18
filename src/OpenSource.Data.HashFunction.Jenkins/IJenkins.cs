using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.Jenkins
{
    /// <summary>
    /// Implementation of Bob Jenkins' One at a Time, Lookup2, or Lookup3 hash function as specified at http://www.burtleburtle.net/bob/hash/doobs.html.
    /// </summary>
    public interface IJenkins
        : IStreamableHashFunction
    {

    }
}
