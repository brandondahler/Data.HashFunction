using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Jenkins
{
    public class JenkinsOneAtATimeFactory
        : IJenkinsOneAtATimeFactory
    {
        public static IJenkinsOneAtATimeFactory Instance { get; } = new JenkinsOneAtATimeFactory();


        private JenkinsOneAtATimeFactory()
        {

        }


        public IJenkinsOneAtATime Create() =>
            new JenkinsOneAtATime_Implementation();
    }
}
