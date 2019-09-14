using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Jenkins;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.Jenkins
{
    public class JenkinsLookup2Config_Tests
    {
        [Fact]
        public void JenkinsLookup2Config_Defaults_HaventChanged()
        {
            var jenkinsLookup2Config = new JenkinsLookup2Config();


            Assert.Equal(0U, jenkinsLookup2Config.Seed);
        }

        [Fact]
        public void JenkinsLookup2Config_Clone_Works()
        {
            var jenkinsLookup2Config = new JenkinsLookup2Config() {
                Seed = 1337U,
            };

            var jenkinsLookup2ConfigClone = jenkinsLookup2Config.Clone();

            Assert.IsType<JenkinsLookup2Config>(jenkinsLookup2ConfigClone);

            Assert.Equal(jenkinsLookup2Config.Seed, jenkinsLookup2ConfigClone.Seed);
        }
    }
}
