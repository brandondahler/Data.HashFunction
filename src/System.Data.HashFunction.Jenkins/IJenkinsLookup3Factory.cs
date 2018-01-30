using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Jenkins
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IJenkinsLookup3"/>.
    /// </summary>
    public interface IJenkinsLookup3Factory
    {
        /// <summary>
        /// Creates a new <see cref="IJenkinsLookup3"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IJenkinsLookup3"/> instance.</returns>
        IJenkinsLookup3 Create();

        /// <summary>
        /// Creates a new <see cref="IJenkinsLookup3"/> instance with the specified configuration.
        /// </summary>
        /// <param name="config">Configuration to use when constructing the instance.</param>
        /// <returns>A <see cref="IJenkinsLookup3"/> instance.</returns>
        IJenkinsLookup3 Create(IJenkinsLookup3Config config);
    }
}
