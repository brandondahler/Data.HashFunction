using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.SpookyHash;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.SpookyHash
{
    public class SpookyHashConfig_Tests
    {
        [Fact]
        public void SpookyHashConfig_Defaults_HaventChanged()
        {
            var spookyHashConfig = new SpookyHashConfig();


            Assert.Equal(128, spookyHashConfig.HashSizeInBits);
            Assert.Equal(0UL, spookyHashConfig.Seed);
            Assert.Equal(0UL, spookyHashConfig.Seed2);
        }

        [Fact]
        public void SpookyHashConfig_Clone_Works()
        {
            var spookyHashConfig = new SpookyHashConfig() {
                HashSizeInBits = 32,
                Seed = 1337UL,
                Seed2 = 7331UL
            };

            var spookyHashConfigClone = spookyHashConfig.Clone();

            Assert.IsType<SpookyHashConfig>(spookyHashConfigClone);

            Assert.Equal(spookyHashConfig.HashSizeInBits, spookyHashConfigClone.HashSizeInBits);
            Assert.Equal(spookyHashConfig.Seed, spookyHashConfigClone.Seed);
            Assert.Equal(spookyHashConfig.Seed2, spookyHashConfigClone.Seed2);
        }
    }
}
