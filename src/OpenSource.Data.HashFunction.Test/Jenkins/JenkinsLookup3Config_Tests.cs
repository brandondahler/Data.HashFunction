using System;
using System.Collections.Generic;
using System.Data.HashFunction.Jenkins;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.Jenkins
{
    public class JenkinsLookup3Config_Tests
    {
        [Fact]
        public void JenkinsLookup3Config_Defaults_HaventChanged()
        {
            var jenkinsLookup3Config = new JenkinsLookup3Config();


            Assert.Equal(32, jenkinsLookup3Config.HashSizeInBits);
            Assert.Equal(0U, jenkinsLookup3Config.Seed);
            Assert.Equal(0U, jenkinsLookup3Config.Seed2);
        }

        [Fact]
        public void JenkinsLookup3Config_Clone_Works()
        {
            var jenkinsLookup3Config = new JenkinsLookup3Config() {
                HashSizeInBits = 64,
                Seed = 1337U,
                Seed2 = 7331U
            };

            var jenkinsLookup3ConfigClone = jenkinsLookup3Config.Clone();

            Assert.IsType<JenkinsLookup3Config>(jenkinsLookup3ConfigClone);

            Assert.Equal(jenkinsLookup3Config.HashSizeInBits, jenkinsLookup3ConfigClone.HashSizeInBits);
            Assert.Equal(jenkinsLookup3Config.Seed, jenkinsLookup3ConfigClone.Seed);
            Assert.Equal(jenkinsLookup3Config.Seed2, jenkinsLookup3ConfigClone.Seed2);
        }
    }
}
