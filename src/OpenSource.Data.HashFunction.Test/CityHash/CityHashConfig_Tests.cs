using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.CityHash;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.CityHash
{
    public class CityHashConfig_Tests
    {
        [Fact]
        public void CityHashConfig_Defaults_HaventChanged()
        {
            var cityHashConfig = new CityHashConfig();


            Assert.Equal(32, cityHashConfig.HashSizeInBits);
        }

        [Fact]
        public void CityHashConfig_Clone_Works()
        {
            var cityHashConfig = new CityHashConfig() {
                HashSizeInBits = 64,
            };

            var cityHashConfigClone = cityHashConfig.Clone();

            Assert.IsType<CityHashConfig>(cityHashConfigClone);

            Assert.Equal(cityHashConfig.HashSizeInBits, cityHashConfigClone.HashSizeInBits);
        }
    }
}
