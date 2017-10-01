using System;
using System.Collections.Generic;
using System.Data.HashFunction.Jenkins;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.Jenkins
{
    public class JenkinsLookup2Factory_Tests
    {
        [Fact]
        public void JenkinsLookup2Factory_Instance_IsDefined()
        {
            Assert.NotNull(JenkinsLookup2Factory.Instance);
            Assert.IsType<JenkinsLookup2Factory>(JenkinsLookup2Factory.Instance);
        }

        [Fact]
        public void JenkinsLookup2Factory_Create_Works()
        {
            var defaultJenkinsLookup2Config = new JenkinsLookup2Config();

            var jenkinsLookup2Factory = JenkinsLookup2Factory.Instance;
            var jenkinsLookup2 = jenkinsLookup2Factory.Create();

            Assert.NotNull(jenkinsLookup2);
            Assert.IsType<JenkinsLookup2_Implementation>(jenkinsLookup2);


            var resultingJenkinsLookup2Config = jenkinsLookup2.Config;

            Assert.Equal(defaultJenkinsLookup2Config.Seed, resultingJenkinsLookup2Config.Seed);
        }


        [Fact]
        public void JenkinsLookup2Factory_Create_Config_IsNull_Throws()
        {
            var jenkinsLookup2Factory = JenkinsLookup2Factory.Instance;

            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => jenkinsLookup2Factory.Create(null))
                    .ParamName);
        }

        [Fact]
        public void JenkinsLookup2Factory_Create_Config_Works()
        {
            var jenkinsLookup2Config = new JenkinsLookup2Config() {
                Seed = 1337U
            };

            var jenkinsLookup2Factory = JenkinsLookup2Factory.Instance;
            var jenkinsLookup2 = jenkinsLookup2Factory.Create(jenkinsLookup2Config);

            Assert.NotNull(jenkinsLookup2);
            Assert.IsType<JenkinsLookup2_Implementation>(jenkinsLookup2);


            var resultingJenkinsLookup2Config = jenkinsLookup2.Config;

            Assert.Equal(jenkinsLookup2Config.Seed, resultingJenkinsLookup2Config.Seed);
        }
    }
}
