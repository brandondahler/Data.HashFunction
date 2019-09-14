using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.xxHash;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.xxHash
{
    public class xxHashConfig_Tests
    {
        [Fact]
        public void xxHashConfig_Defaults_HaventChanged()
        {
            var xxHashConfigInstance = new xxHashConfig();

            Assert.Equal(32, xxHashConfigInstance.HashSizeInBits);
            Assert.Equal(0UL, xxHashConfigInstance.Seed);
        }

        [Fact]
        public void xxHashConfig_Clone_Works()
        {
            var xxHashConfigInstance = new xxHashConfig() {
                HashSizeInBits = 64,
                Seed = 1337UL,
            };

            var xxHashConfigClone = xxHashConfigInstance.Clone();

            Assert.IsType<xxHashConfig>(xxHashConfigClone);

            Assert.Equal(xxHashConfigInstance.HashSizeInBits, xxHashConfigClone.HashSizeInBits);
            Assert.Equal(xxHashConfigInstance.Seed, xxHashConfigClone.Seed);
        }
    }
}
