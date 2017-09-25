using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Jenkins
{
    public class JenkinsLookup3Factory
        : IJenkinsLookup3Factory
    {
        public IJenkinsLookup3Factory Instance { get; } = new JenkinsLookup3Factory();


        private JenkinsLookup3Factory()
        {

        }


        public IJenkinsLookup3 Create() => 
            Create(new JenkinsLookup3Config());

        public IJenkinsLookup3 Create(IJenkinsLookup3Config config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            return new JenkinsLookup3_Implementation(config);
        }
    }
}
