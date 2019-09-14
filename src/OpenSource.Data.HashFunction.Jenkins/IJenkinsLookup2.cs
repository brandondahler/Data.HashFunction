using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.Jenkins
{
    /// <summary>
    /// Implementation of Bob Jenkins' Lookup2 hash function as specified at http://burtleburtle.net/bob/c/lookup2.c and http://www.burtleburtle.net/bob/hash/doobs.html.
    /// 
    /// This hash function has been superseded by <seealso cref="IJenkinsLookup3">JenkinsLookup3</seealso>.
    /// </summary>
    public interface IJenkinsLookup2
        : IJenkins
    {

        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        IJenkinsLookup2Config Config { get; }

    }
}
