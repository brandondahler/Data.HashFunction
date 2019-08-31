using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.MurmurHash;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.MurmurHash
{
    public class MurmurHash2Config_Tests
    {
        [Fact]
        public void MurmurHash2Config_Defaults_HaventChanged()
        {
            var murmurHash2Config = new MurmurHash2Config();


            Assert.Equal(64, murmurHash2Config.HashSizeInBits);
            Assert.Equal(0UL, murmurHash2Config.Seed);
        }

        [Fact]
        public void MurmurHash2Config_Clone_Works()
        {
            var murmurHash2Config = new MurmurHash2Config() {
                HashSizeInBits = 32,
                Seed = 1337UL,
            };

            var murmurHash2ConfigClone = murmurHash2Config.Clone();

            Assert.IsType<MurmurHash2Config>(murmurHash2ConfigClone);

            Assert.Equal(murmurHash2Config.HashSizeInBits, murmurHash2ConfigClone.HashSizeInBits);
            Assert.Equal(murmurHash2Config.Seed, murmurHash2ConfigClone.Seed);
        }
    }
}
