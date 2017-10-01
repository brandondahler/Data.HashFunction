using System;
using System.Collections.Generic;
using System.Data.HashFunction.Jenkins;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.Jenkins
{
    public class JenkinsLookup3Factory_Tests
    {
        [Fact]
        public void JenkinsLookup3Factory_Instance_IsDefined()
        {
            Assert.NotNull(JenkinsLookup3Factory.Instance);
            Assert.IsType<JenkinsLookup3Factory>(JenkinsLookup3Factory.Instance);
        }

        [Fact]
        public void JenkinsLookup3Factory_Create_Works()
        {
            var defaultJenkinsLookup3Config = new JenkinsLookup3Config();

            var jenkinsLookup3Factory = JenkinsLookup3Factory.Instance;
            var jenkinsLookup3 = jenkinsLookup3Factory.Create();

            Assert.NotNull(jenkinsLookup3);
            Assert.IsType<JenkinsLookup3_Implementation>(jenkinsLookup3);


            var resultingJenkinsLookup3Config = jenkinsLookup3.Config;

            Assert.Equal(defaultJenkinsLookup3Config.HashSizeInBits, resultingJenkinsLookup3Config.HashSizeInBits);
            Assert.Equal(defaultJenkinsLookup3Config.Seed, resultingJenkinsLookup3Config.Seed);
            Assert.Equal(defaultJenkinsLookup3Config.Seed2, resultingJenkinsLookup3Config.Seed2);
        }


        [Fact]
        public void JenkinsLookup3Factory_Create_Config_IsNull_Throws()
        {
            var jenkinsLookup3Factory = JenkinsLookup3Factory.Instance;

            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => jenkinsLookup3Factory.Create(null))
                    .ParamName);
        }

        [Fact]
        public void JenkinsLookup3Factory_Create_Config_Works()
        {
            var jenkinsLookup3Config = new JenkinsLookup3Config() {
                HashSizeInBits = 64,
                Seed = 1337U,
                Seed2 = 7331U
            };

            var jenkinsLookup3Factory = JenkinsLookup3Factory.Instance;
            var jenkinsLookup3 = jenkinsLookup3Factory.Create(jenkinsLookup3Config);

            Assert.NotNull(jenkinsLookup3);
            Assert.IsType<JenkinsLookup3_Implementation>(jenkinsLookup3);


            var resultingJenkinsLookup3Config = jenkinsLookup3.Config;

            Assert.Equal(jenkinsLookup3Config.HashSizeInBits, resultingJenkinsLookup3Config.HashSizeInBits);
            Assert.Equal(jenkinsLookup3Config.Seed, resultingJenkinsLookup3Config.Seed);
            Assert.Equal(jenkinsLookup3Config.Seed2, resultingJenkinsLookup3Config.Seed2);
        }
    }
}
