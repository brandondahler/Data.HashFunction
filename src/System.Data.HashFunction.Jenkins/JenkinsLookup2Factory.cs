using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Jenkins
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IJenkinsLookup2"/>.
    /// </summary>
    public class JenkinsLookup2Factory
        : IJenkinsLookup2Factory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IJenkinsLookup2Factory Instance { get; } = new JenkinsLookup2Factory();


        private JenkinsLookup2Factory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IJenkinsLookup2"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IJenkinsLookup2"/> instance.</returns>
        public IJenkinsLookup2 Create() =>
            Create(new JenkinsLookup2Config());

        /// <summary>
        /// Creates a new <see cref="IJenkinsLookup2"/> instance with the specified configuration.
        /// </summary>
        /// <param name="config">Configuration to use when constructing the instance.</param>
        /// <returns>A <see cref="IJenkinsLookup2"/> instance.</returns>
        public IJenkinsLookup2 Create(IJenkinsLookup2Config config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new JenkinsLookup2_Implementation(config);
        }
    }
}
