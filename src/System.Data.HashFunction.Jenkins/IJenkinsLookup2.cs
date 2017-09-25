using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Jenkins
{
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
