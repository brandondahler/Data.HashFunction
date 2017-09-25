using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Jenkins
{
    public class JenkinsOneAtATimeFactory
        : IJenkinsOneAtATimeFactory
    {
        public IJenkinsOneAtATimeFactory Instance { get; } = new JenkinsOneAtATimeFactory();


        private JenkinsOneAtATimeFactory()
        {

        }


        public IJenkinsOneAtATime Create() =>
            new JenkinsOneAtATime_Implementation();
    }
}
