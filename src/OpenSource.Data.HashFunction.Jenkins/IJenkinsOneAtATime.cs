using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Jenkins
{
    /// <summary>
    /// Implementation of Bob Jenkins' One-at-a-Time hash function as specified at http://www.burtleburtle.net/bob/hash/doobs.html (function named "one_at_a_time").
    /// 
    /// This hash function has been superseded by <seealso cref="IJenkinsLookup2">JenkinsLookup2</seealso> and <seealso cref="IJenkinsLookup3">JenkinsLookup3</seealso>.
    /// </summary>
    public interface IJenkinsOneAtATime
        : IJenkins
    {

    }
}
