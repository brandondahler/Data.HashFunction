using System;
using System.Collections.Generic;
using System.Data.HashFunction.MurmurHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.MurmurHash
{
    public class MurmurHash1Config_Tests
    {
        [Fact]
        public void MurmurHash1Config_Defaults_HaventChanged()
        {
            var murmurHash1Config = new MurmurHash1Config();


            Assert.Equal(0U, murmurHash1Config.Seed);
        }

        [Fact]
        public void MurmurHash1Config_Clone_Works()
        {
            var murmurHash1Config = new MurmurHash1Config() {
                Seed = 1337U
            };

            var murmurHash1ConfigClone = murmurHash1Config.Clone();

            Assert.IsType<MurmurHash1Config>(murmurHash1ConfigClone);

            Assert.Equal(murmurHash1Config.Seed, murmurHash1ConfigClone.Seed);
        }
    }
}
