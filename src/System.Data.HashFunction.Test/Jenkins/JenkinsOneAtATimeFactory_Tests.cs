using System;
using System.Collections.Generic;
using System.Data.HashFunction.Jenkins;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.Jenkins
{
    public class JenkinsOneAtATimeFactory_Tests
    {
        [Fact]
        public void JenkinsOneAtATimeFactory_Instance_IsDefined()
        {
            Assert.NotNull(JenkinsOneAtATimeFactory.Instance);
            Assert.IsType<JenkinsOneAtATimeFactory>(JenkinsOneAtATimeFactory.Instance);
        }

        [Fact]
        public void JenkinsOneAtATimeFactory_Create_Works()
        {
            var jenkinsOneAtATimeFactory = JenkinsOneAtATimeFactory.Instance;
            var jenkinsOneAtATime = jenkinsOneAtATimeFactory.Create();

            Assert.NotNull(jenkinsOneAtATime);
            Assert.IsType<JenkinsOneAtATime_Implementation>(jenkinsOneAtATime);
        }
    }
}
