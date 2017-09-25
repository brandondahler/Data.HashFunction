using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Jenkins
{
    public class JenkinsLookup2Factory
        : IJenkinsLookup2Factory
    {
        public static IJenkinsLookup2Factory Instance { get; } = new JenkinsLookup2Factory();


        private JenkinsLookup2Factory()
        {

        }


        public IJenkinsLookup2 Create() =>
            Create(new JenkinsLookup2Config());

        public IJenkinsLookup2 Create(IJenkinsLookup2Config config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new JenkinsLookup2_Implementation(config);
        }
    }
}
