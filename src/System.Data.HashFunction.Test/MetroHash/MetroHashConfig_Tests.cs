using System;
using System.Collections.Generic;
using System.Data.HashFunction.MetroHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.MetroHash
{
    public class MetroHashConfig_Tests
    {
        [Fact]
        public void MetroHashConfig_Defaults_HaventChanged()
        {
            var metroHashConfig = new MetroHashConfig();


            Assert.Equal(0UL, metroHashConfig.Seed);
        }

        [Fact]
        public void MetroHashConfig_Clone_Works()
        {
            var metroHashConfig = new MetroHashConfig() {
                Seed = 1,
            };

            var metroHashConfigClone = metroHashConfig.Clone();

            Assert.IsType<MetroHashConfig>(metroHashConfigClone);

            Assert.Equal(metroHashConfig.Seed, metroHashConfigClone.Seed);
        }
    }
}
