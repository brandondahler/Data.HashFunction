using System;
using System.Collections.Generic;
using System.Data.HashFunction.SpookyHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.SpookyHash
{
    public class SpookyHashV1Factory_Tests
    {
        [Fact]
        public void SpookyHashV1Factory_Instance_IsDefined()
        {
            Assert.NotNull(SpookyHashV1Factory.Instance);
            Assert.IsType<SpookyHashV1Factory>(SpookyHashV1Factory.Instance);
        }

        [Fact]
        public void SpookyHashV1Factory_Create_Works()
        {
            var defaultSpookyHashV1Config = new SpookyHashConfig();

            var spookyHashV1Factory = SpookyHashV1Factory.Instance;
            var spookyHashV1 = spookyHashV1Factory.Create();

            Assert.NotNull(spookyHashV1);
            Assert.IsType<SpookyHashV1_Implementation>(spookyHashV1);


            var resultingSpookyHashV1Config = spookyHashV1.Config;

            Assert.Equal(defaultSpookyHashV1Config.HashSizeInBits, resultingSpookyHashV1Config.HashSizeInBits);
            Assert.Equal(defaultSpookyHashV1Config.Seed, resultingSpookyHashV1Config.Seed);
            Assert.Equal(defaultSpookyHashV1Config.Seed2, resultingSpookyHashV1Config.Seed2);
        }


        [Fact]
        public void SpookyHashV1Factory_Create_Config_IsNull_Throws()
        {
            var spookyHashV1Factory = SpookyHashV1Factory.Instance;

            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => spookyHashV1Factory.Create(null))
                    .ParamName);
        }

        [Fact]
        public void SpookyHashV1Factory_Create_Config_Works()
        {
            var spookyHashV1Config = new SpookyHashConfig() {
                HashSizeInBits = 32,
                Seed = 1337UL,
                Seed2 = 7331UL,
            };

            var spookyHashV1Factory = SpookyHashV1Factory.Instance;
            var spookyHashV1 = spookyHashV1Factory.Create(spookyHashV1Config);

            Assert.NotNull(spookyHashV1);
            Assert.IsType<SpookyHashV1_Implementation>(spookyHashV1);


            var resultingSpookyHashV1Config = spookyHashV1.Config;

            Assert.Equal(spookyHashV1Config.HashSizeInBits, resultingSpookyHashV1Config.HashSizeInBits);
            Assert.Equal(spookyHashV1Config.Seed, resultingSpookyHashV1Config.Seed);
            Assert.Equal(spookyHashV1Config.Seed2, resultingSpookyHashV1Config.Seed2);
        }
    }
}
