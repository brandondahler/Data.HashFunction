using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.MurmurHash;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.MurmurHash
{
    public class MurmurHash3Config_Tests
    {
        [Fact]
        public void MurmurHash3Config_Defaults_HaventChanged()
        {
            var murmurHash3Config = new MurmurHash3Config();


            Assert.Equal(32, murmurHash3Config.HashSizeInBits);
            Assert.Equal(0U, murmurHash3Config.Seed);
        }

        [Fact]
        public void MurmurHash3Config_Clone_Works()
        {
            var murmurHash3Config = new MurmurHash3Config() {
                HashSizeInBits = 64,
                Seed = 1337U,
            };

            var murmurHash3ConfigClone = murmurHash3Config.Clone();

            Assert.IsType<MurmurHash3Config>(murmurHash3ConfigClone);

            Assert.Equal(murmurHash3Config.HashSizeInBits, murmurHash3ConfigClone.HashSizeInBits);
            Assert.Equal(murmurHash3Config.Seed, murmurHash3ConfigClone.Seed);
        }
    }
}
