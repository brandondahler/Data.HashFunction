using System;
using System.Collections.Generic;
using System.Data.HashFunction.FNV;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.FNV
{
    public class FNV1Factory_Tests
    {
        [Fact]
        public void FNV1Factory_Instance_IsDefined()
        {
            Assert.NotNull(FNV1Factory.Instance);
            Assert.IsType<FNV1Factory>(FNV1Factory.Instance);
        }

        [Fact]
        public void FNV1Factory_Create_Works()
        {
            var defaultFNVConfig = FNVConfig.GetPredefinedConfig(64);

            var fnv1Factory = FNV1Factory.Instance;
            var fnv1 = fnv1Factory.Create();

            Assert.NotNull(fnv1);
            Assert.IsType<FNV1_Implementation>(fnv1);


            var resultingFNVConfig = fnv1.Config;

            Assert.Equal(defaultFNVConfig.HashSizeInBits, resultingFNVConfig.HashSizeInBits);
            Assert.Equal(defaultFNVConfig.Prime, resultingFNVConfig.Prime);
            Assert.Equal(defaultFNVConfig.Offset, resultingFNVConfig.Offset);
        }


        [Fact]
        public void FNV1Factory_Create_Config_IsNull_Throws()
        {
            var fnv1Factory = FNV1Factory.Instance;

            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => fnv1Factory.Create(null))
                    .ParamName);
        }

        [Fact]
        public void FNV1Factory_Create_Config_Works()
        {
            var fnvConfig = FNVConfig.GetPredefinedConfig(32);

            var fnv1Factory = FNV1Factory.Instance;
            var fnv1 = fnv1Factory.Create(fnvConfig);

            Assert.NotNull(fnv1);
            Assert.IsType<FNV1_Implementation>(fnv1);


            var resultingFNVConfig = fnv1.Config;

            Assert.Equal(fnvConfig.HashSizeInBits, resultingFNVConfig.HashSizeInBits);
            Assert.Equal(fnvConfig.Prime, resultingFNVConfig.Prime);
            Assert.Equal(fnvConfig.Offset, resultingFNVConfig.Offset);
        }
    }
}
