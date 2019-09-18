using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.Jenkins
{
    /// <summary>
    /// Implementation of Bob Jenkins' One-at-a-Time hash function as specified at http://www.burtleburtle.net/bob/hash/doobs.html (function named "one_at_a_time").
    /// 
    /// This hash function has been superseded by <see cref="IJenkinsLookup2">JenkinsLookup2</see> and <see cref="IJenkinsLookup3">JenkinsLookup3</see>.
    /// </summary>
    public interface IJenkinsOneAtATime
        : IJenkins
    {

    }
}
