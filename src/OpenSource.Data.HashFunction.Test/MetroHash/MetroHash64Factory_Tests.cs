using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.MetroHash;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.MetroHash
{
    public class MetroHash64Factory_Tests
    {
        [Fact]
        public void MetroHash64Factory_Instance_IsDefined()
        {
            Assert.NotNull(MetroHash64Factory.Instance);
            Assert.IsType<MetroHash64Factory>(MetroHash64Factory.Instance);
        }

        [Fact]
        public void MetroHash64Factory_Create_Works()
        {
            var defaultMetroHashConfig = new MetroHashConfig();

            var metroHashFactory = MetroHash64Factory.Instance;
            var metroHash = metroHashFactory.Create();

            Assert.NotNull(metroHash);
            Assert.IsType<MetroHash64_Implementation>(metroHash);


            var resultingMetroHashConfig = metroHash.Config;

            Assert.Equal(defaultMetroHashConfig.Seed, resultingMetroHashConfig.Seed);
        }


        [Fact]
        public void MetroHash64Factory_Create_Config_IsNull_Throws()
        {
            var metroHashFactory = MetroHash64Factory.Instance;

            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => metroHashFactory.Create(null))
                    .ParamName);
        }

        [Fact]
        public void MetroHash64Factory_Create_Config_Works()
        {
            var metroHashConfig = new MetroHashConfig() {
                Seed = 1,
            };

            var metroHashFactory = MetroHash64Factory.Instance;
            var metroHash = metroHashFactory.Create(metroHashConfig);

            Assert.NotNull(metroHash);
            Assert.IsType<MetroHash64_Implementation>(metroHash);


            var resultingMetroHashConfig = metroHash.Config;

            Assert.Equal(metroHashConfig.Seed, resultingMetroHashConfig.Seed);
        }
    }
}
