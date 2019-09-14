using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.MurmurHash;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.MurmurHash
{
    public class MurmurHash2Factory_Tests
    {
        [Fact]
        public void MurmurHash2Factory_Instance_IsDefined()
        {
            Assert.NotNull(MurmurHash2Factory.Instance);
            Assert.IsType<MurmurHash2Factory>(MurmurHash2Factory.Instance);
        }

        [Fact]
        public void MurmurHash2Factory_Create_Works()
        {
            var defaultMurmurHash2Config = new MurmurHash2Config();

            var murmurHash2Factory = MurmurHash2Factory.Instance;
            var murmurHash2 = murmurHash2Factory.Create();

            Assert.NotNull(murmurHash2);
            Assert.IsType<MurmurHash2_Implementation>(murmurHash2);


            var resultingMurmurHash2Config = murmurHash2.Config;

            Assert.Equal(defaultMurmurHash2Config.HashSizeInBits, resultingMurmurHash2Config.HashSizeInBits);
            Assert.Equal(defaultMurmurHash2Config.Seed, resultingMurmurHash2Config.Seed);
        }


        [Fact]
        public void MurmurHash2Factory_Create_Config_IsNull_Throws()
        {
            var murmurHash2Factory = MurmurHash2Factory.Instance;

            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => murmurHash2Factory.Create(null))
                    .ParamName);
        }

        [Fact]
        public void MurmurHash2Factory_Create_Config_Works()
        {
            var murmurHash2Config = new MurmurHash2Config() {
                HashSizeInBits = 32,
                Seed = 1337U
            };

            var murmurHash2Factory = MurmurHash2Factory.Instance;
            var murmurHash2 = murmurHash2Factory.Create(murmurHash2Config);

            Assert.NotNull(murmurHash2);
            Assert.IsType<MurmurHash2_Implementation>(murmurHash2);


            var resultingMurmurHash2Config = murmurHash2.Config;

            Assert.Equal(murmurHash2Config.HashSizeInBits, resultingMurmurHash2Config.HashSizeInBits);
            Assert.Equal(murmurHash2Config.Seed, resultingMurmurHash2Config.Seed);
        }
    }
}
