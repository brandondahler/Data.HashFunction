using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Jenkins
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IJenkinsLookup3"/>.
    /// </summary>
    public class JenkinsLookup3Factory
        : IJenkinsLookup3Factory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IJenkinsLookup3Factory Instance { get; } = new JenkinsLookup3Factory();


        private JenkinsLookup3Factory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IJenkinsLookup3"/> instance with the default configuration.
        /// </summary>
        /// <returns>A <see cref="IJenkinsLookup3"/> instance.</returns>
        public IJenkinsLookup3 Create() => 
            Create(new JenkinsLookup3Config());

        /// <summary>
        /// Creates a new <see cref="IJenkinsLookup3"/> instance with the specified configuration.
        /// </summary>
        /// <param name="config">Configuration to use when constructing the instance.</param>
        /// <returns>A <see cref="IJenkinsLookup3"/> instance.</returns>
        public IJenkinsLookup3 Create(IJenkinsLookup3Config config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new JenkinsLookup3_Implementation(config);
        }
    }
}
