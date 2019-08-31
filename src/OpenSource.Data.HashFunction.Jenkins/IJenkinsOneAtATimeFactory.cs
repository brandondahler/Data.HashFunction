using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Jenkins
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IJenkinsOneAtATime"/>.
    /// </summary>
    public interface IJenkinsOneAtATimeFactory
    {
        /// <summary>
        /// Creates a new <see cref="IJenkinsOneAtATime"/> instance.
        /// </summary>
        /// <returns>A <see cref="IJenkinsOneAtATime"/> instance.</returns>
        IJenkinsOneAtATime Create();
    }
}
