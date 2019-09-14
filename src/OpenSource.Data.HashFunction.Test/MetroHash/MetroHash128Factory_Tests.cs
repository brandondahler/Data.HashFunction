using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.MetroHash;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.MetroHash
{
    public class MetroHash128Factory_Tests
    {
        [Fact]
        public void MetroHash128Factory_Instance_IsDefined()
        {
            Assert.NotNull(MetroHash128Factory.Instance);
            Assert.IsType<MetroHash128Factory>(MetroHash128Factory.Instance);
        }

        [Fact]
        public void MetroHash128Factory_Create_Works()
        {
            var defaultMetroHashConfig = new MetroHashConfig();

            var metroHashFactory = MetroHash128Factory.Instance;
            var metroHash = metroHashFactory.Create();

            Assert.NotNull(metroHash);
            Assert.IsType<MetroHash128_Implementation>(metroHash);


            var resultingMetroHashConfig = metroHash.Config;

            Assert.Equal(defaultMetroHashConfig.Seed, resultingMetroHashConfig.Seed);
        }


        [Fact]
        public void MetroHash128Factory_Create_Config_IsNull_Throws()
        {
            var metroHashFactory = MetroHash128Factory.Instance;

            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => metroHashFactory.Create(null))
                    .ParamName);
        }

        [Fact]
        public void MetroHash128Factory_Create_Config_Works()
        {
            var metroHashConfig = new MetroHashConfig() {
                Seed = 1,
            };

            var metroHashFactory = MetroHash128Factory.Instance;
            var metroHash = metroHashFactory.Create(metroHashConfig);

            Assert.NotNull(metroHash);
            Assert.IsType<MetroHash128_Implementation>(metroHash);


            var resultingMetroHashConfig = metroHash.Config;

            Assert.Equal(metroHashConfig.Seed, resultingMetroHashConfig.Seed);
        }
    }
}
