using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.FNV;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.FNV
{
    public class FNV1aFactory_Tests
    {
        [Fact]
        public void FNV1aFactory_Instance_IsDefined()
        {
            Assert.NotNull(FNV1aFactory.Instance);
            Assert.IsType<FNV1aFactory>(FNV1aFactory.Instance);
        }

        [Fact]
        public void FNV1aFactory_Create_Works()
        {
            var defaultFNVConfig = FNVConfig.GetPredefinedConfig(64);

            var fnv1aFactory = FNV1aFactory.Instance;
            var fnv1a = fnv1aFactory.Create();

            Assert.NotNull(fnv1a);
            Assert.IsType<FNV1a_Implementation>(fnv1a);


            var resultingFNVConfig = fnv1a.Config;

            Assert.Equal(defaultFNVConfig.HashSizeInBits, resultingFNVConfig.HashSizeInBits);
            Assert.Equal(defaultFNVConfig.Prime, resultingFNVConfig.Prime);
            Assert.Equal(defaultFNVConfig.Offset, resultingFNVConfig.Offset);
        }


        [Fact]
        public void FNV1aFactory_Create_Config_IsNull_Throws()
        {
            var fnv1aFactory = FNV1aFactory.Instance;

            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => fnv1aFactory.Create(null))
                    .ParamName);
        }

        [Fact]
        public void FNV1aFactory_Create_Config_Works()
        {
            var fnvConfig = FNVConfig.GetPredefinedConfig(32);

            var fnv1aFactory = FNV1aFactory.Instance;
            var fnv1a = fnv1aFactory.Create(fnvConfig);

            Assert.NotNull(fnv1a);
            Assert.IsType<FNV1a_Implementation>(fnv1a);


            var resultingFNVConfig = fnv1a.Config;

            Assert.Equal(fnvConfig.HashSizeInBits, resultingFNVConfig.HashSizeInBits);
            Assert.Equal(fnvConfig.Prime, resultingFNVConfig.Prime);
            Assert.Equal(fnvConfig.Offset, resultingFNVConfig.Offset);
        }
    }
}
