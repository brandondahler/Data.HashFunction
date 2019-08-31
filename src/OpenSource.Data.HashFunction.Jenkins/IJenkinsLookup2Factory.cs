using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Jenkins
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IJenkinsLookup2"/>.
    /// </summary>
    public interface IJenkinsLookup2Factory
    {
        /// <summary>
        /// Creates a new <see cref="IJenkinsLookup2"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IJenkinsLookup2"/> instance.</returns>
        IJenkinsLookup2 Create();

        /// <summary>
        /// Creates a new <see cref="IJenkinsLookup2"/> instance with the specified configuration.
        /// </summary>
        /// <param name="config">Configuration to use when constructing the instance.</param>
        /// <returns>A <see cref="IJenkinsLookup2"/> instance.</returns>
        IJenkinsLookup2 Create(IJenkinsLookup2Config config);
    }
}
