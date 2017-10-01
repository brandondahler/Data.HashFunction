using System;
using System.Collections.Generic;
using System.Data.HashFunction.SpookyHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.SpookyHash
{
    public class SpookyHashV2Factory_Tests
    {
        [Fact]
        public void SpookyHashV2Factory_Instance_IsDefined()
        {
            Assert.NotNull(SpookyHashV2Factory.Instance);
            Assert.IsType<SpookyHashV2Factory>(SpookyHashV2Factory.Instance);
        }

        [Fact]
        public void SpookyHashV2Factory_Create_Works()
        {
            var defaultSpookyHashV2Config = new SpookyHashConfig();

            var spookyHashV2Factory = SpookyHashV2Factory.Instance;
            var spookyHashV2 = spookyHashV2Factory.Create();

            Assert.NotNull(spookyHashV2);
            Assert.IsType<SpookyHashV2_Implementation>(spookyHashV2);


            var resultingSpookyHashV2Config = spookyHashV2.Config;

            Assert.Equal(defaultSpookyHashV2Config.HashSizeInBits, resultingSpookyHashV2Config.HashSizeInBits);
            Assert.Equal(defaultSpookyHashV2Config.Seed, resultingSpookyHashV2Config.Seed);
            Assert.Equal(defaultSpookyHashV2Config.Seed2, resultingSpookyHashV2Config.Seed2);
        }


        [Fact]
        public void SpookyHashV2Factory_Create_Config_IsNull_Throws()
        {
            var spookyHashV2Factory = SpookyHashV2Factory.Instance;

            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => spookyHashV2Factory.Create(null))
                    .ParamName);
        }

        [Fact]
        public void SpookyHashV2Factory_Create_Config_Works()
        {
            var spookyHashV2Config = new SpookyHashConfig() {
                HashSizeInBits = 32,
                Seed = 1337UL,
                Seed2 = 7331UL,
            };

            var spookyHashV2Factory = SpookyHashV2Factory.Instance;
            var spookyHashV2 = spookyHashV2Factory.Create(spookyHashV2Config);

            Assert.NotNull(spookyHashV2);
            Assert.IsType<SpookyHashV2_Implementation>(spookyHashV2);


            var resultingSpookyHashV2Config = spookyHashV2.Config;

            Assert.Equal(spookyHashV2Config.HashSizeInBits, resultingSpookyHashV2Config.HashSizeInBits);
            Assert.Equal(spookyHashV2Config.Seed, resultingSpookyHashV2Config.Seed);
            Assert.Equal(spookyHashV2Config.Seed2, resultingSpookyHashV2Config.Seed2);
        }
    }
}
