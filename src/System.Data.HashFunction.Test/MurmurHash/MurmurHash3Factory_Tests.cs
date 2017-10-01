using System;
using System.Collections.Generic;
using System.Data.HashFunction.MurmurHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.MurmurHash
{
    public class MurmurHash3Factory_Tests
    {
        [Fact]
        public void MurmurHash3Factory_Instance_IsDefined()
        {
            Assert.NotNull(MurmurHash3Factory.Instance);
            Assert.IsType<MurmurHash3Factory>(MurmurHash3Factory.Instance);
        }

        [Fact]
        public void MurmurHash3Factory_Create_Works()
        {
            var defaultMurmurHash3Config = new MurmurHash3Config();

            var murmurHash3Factory = MurmurHash3Factory.Instance;
            var murmurHash3 = murmurHash3Factory.Create();

            Assert.NotNull(murmurHash3);
            Assert.IsType<MurmurHash3_Implementation>(murmurHash3);


            var resultingMurmurHash3Config = murmurHash3.Config;

            Assert.Equal(defaultMurmurHash3Config.HashSizeInBits, resultingMurmurHash3Config.HashSizeInBits);
            Assert.Equal(defaultMurmurHash3Config.Seed, resultingMurmurHash3Config.Seed);
        }


        [Fact]
        public void MurmurHash3Factory_Create_Config_IsNull_Throws()
        {
            var murmurHash3Factory = MurmurHash3Factory.Instance;

            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => murmurHash3Factory.Create(null))
                    .ParamName);
        }

        [Fact]
        public void MurmurHash3Factory_Create_Config_Works()
        {
            var murmurHash3Config = new MurmurHash3Config() {
                HashSizeInBits = 128,
                Seed = 1337U
            };

            var murmurHash3Factory = MurmurHash3Factory.Instance;
            var murmurHash3 = murmurHash3Factory.Create(murmurHash3Config);

            Assert.NotNull(murmurHash3);
            Assert.IsType<MurmurHash3_Implementation>(murmurHash3);


            var resultingMurmurHash3Config = murmurHash3.Config;

            Assert.Equal(murmurHash3Config.HashSizeInBits, resultingMurmurHash3Config.HashSizeInBits);
            Assert.Equal(murmurHash3Config.Seed, resultingMurmurHash3Config.Seed);
        }
    }
}
