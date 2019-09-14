using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.BuzHash;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.BuzHash
{
    public class BuzHashFactory_Tests
    {
        [Fact]
        public void BuzHashFactory_Instance_IsDefined()
        {
            Assert.NotNull(BuzHashFactory.Instance);
            Assert.IsType<BuzHashFactory>(BuzHashFactory.Instance);
        }

        [Fact]
        public void BuzHashFactory_Create_Works()
        {
            var defaultBuzHashConfig = new DefaultBuzHashConfig();

            var buzHashFactory = BuzHashFactory.Instance;
            var buzHash = buzHashFactory.Create();

            Assert.NotNull(buzHash);
            Assert.IsType<BuzHash_Implementation>(buzHash);


            var resultingBuzHashConfig = buzHash.Config;

            Assert.Equal(defaultBuzHashConfig.Rtab, resultingBuzHashConfig.Rtab);
            Assert.Equal(defaultBuzHashConfig.HashSizeInBits, resultingBuzHashConfig.HashSizeInBits);
            Assert.Equal(defaultBuzHashConfig.Seed, resultingBuzHashConfig.Seed);
            Assert.Equal(defaultBuzHashConfig.ShiftDirection, resultingBuzHashConfig.ShiftDirection);
        }


        [Fact]
        public void BuzHashFactory_Create_Config_IsNull_Throws()
        {
            var buzHashFactory = BuzHashFactory.Instance;

            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => buzHashFactory.Create(null))
                    .ParamName);
        }

        [Fact]
        public void BuzHashFactory_Create_Config_Works()
        {
            var buzHashConfig = new BuzHashConfig() {
                Rtab = new UInt64[256],
                HashSizeInBits = 32,
                Seed = 1337UL,
                ShiftDirection = CircularShiftDirection.Right
            };

            var buzHashFactory = BuzHashFactory.Instance;
            var buzHash = buzHashFactory.Create(buzHashConfig);

            Assert.NotNull(buzHash);
            Assert.IsType<BuzHash_Implementation>(buzHash);


            var resultingBuzHashConfig = buzHash.Config;

            Assert.Equal(buzHashConfig.Rtab, resultingBuzHashConfig.Rtab);
            Assert.Equal(buzHashConfig.HashSizeInBits, resultingBuzHashConfig.HashSizeInBits);
            Assert.Equal(buzHashConfig.Seed, resultingBuzHashConfig.Seed);
            Assert.Equal(buzHashConfig.ShiftDirection, resultingBuzHashConfig.ShiftDirection);
        }
    }
}
