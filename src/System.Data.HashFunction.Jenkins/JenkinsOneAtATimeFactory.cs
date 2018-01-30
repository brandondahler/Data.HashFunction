using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Jenkins
{
    /// <summary>
    /// Provides instances of implementations of <see cref="IJenkinsOneAtATime"/>.
    /// </summary>
    public class JenkinsOneAtATimeFactory
        : IJenkinsOneAtATimeFactory
    {
        /// <summary>
        /// Gets the singleton instance of this factory.
        /// </summary>
        public static IJenkinsOneAtATimeFactory Instance { get; } = new JenkinsOneAtATimeFactory();


        private JenkinsOneAtATimeFactory()
        {

        }


        /// <summary>
        /// Creates a new <see cref="IJenkinsOneAtATime"/> instance.
        /// </summary>
        /// <returns>A <see cref="IJenkinsOneAtATime"/> instance.</returns>
        public IJenkinsOneAtATime Create() =>
            new JenkinsOneAtATime_Implementation();
    }
}
