using System;
using System.Collections.Generic;
using System.Data.HashFunction.CityHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.CityHash
{
    public class CityHashFactory_Tests
    {
        [Fact]
        public void CityHashFactory_Instance_IsDefined()
        {
            Assert.NotNull(CityHashFactory.Instance);
            Assert.IsType<CityHashFactory>(CityHashFactory.Instance);
        }

        [Fact]
        public void CityHashFactory_Create_Works()
        {
            var defaultCityHashConfig = new CityHashConfig();

            var cityHashFactory = CityHashFactory.Instance;
            var cityHash = cityHashFactory.Create();

            Assert.NotNull(cityHash);
            Assert.IsType<CityHash_Implementation>(cityHash);


            var resultingCityHashConfig = cityHash.Config;

            Assert.Equal(defaultCityHashConfig.HashSizeInBits, resultingCityHashConfig.HashSizeInBits);
        }


        [Fact]
        public void CityHashFactory_Create_Config_IsNull_Throws()
        {
            var cityHashFactory = CityHashFactory.Instance;

            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => cityHashFactory.Create(null))
                    .ParamName);
        }

        [Fact]
        public void CityHashFactory_Create_Config_Works()
        {
            var cityHashConfig = new CityHashConfig() {
                HashSizeInBits = 64,
            };

            var cityHashFactory = CityHashFactory.Instance;
            var cityHash = cityHashFactory.Create(cityHashConfig);

            Assert.NotNull(cityHash);
            Assert.IsType<CityHash_Implementation>(cityHash);


            var resultingCityHashConfig = cityHash.Config;

            Assert.Equal(cityHashConfig.HashSizeInBits, resultingCityHashConfig.HashSizeInBits);
        }
    }
}
