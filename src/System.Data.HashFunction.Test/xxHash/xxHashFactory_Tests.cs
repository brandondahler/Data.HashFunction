using System;
using System.Collections.Generic;
using System.Data.HashFunction.xxHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.xxHash
{
    public class xxHashFactory_Tests
    {
        [Fact]
        public void xxHashFactory_Instance_IsDefined()
        {
            Assert.NotNull(xxHashFactory.Instance);
            Assert.IsType<xxHashFactory>(xxHashFactory.Instance);
        }

        [Fact]
        public void xxHashFactory_Create_Works()
        {
            var defaultXxHashConfig = new xxHashConfig();

            var xxHashFactoryInstance = xxHashFactory.Instance;
            var xxHash = xxHashFactoryInstance.Create();

            Assert.NotNull(xxHash);
            Assert.IsType<xxHash_Implementation>(xxHash);


            var resultingXxHashConfig = xxHash.Config;

            Assert.Equal(defaultXxHashConfig.HashSizeInBits, resultingXxHashConfig.HashSizeInBits);
            Assert.Equal(defaultXxHashConfig.Seed, resultingXxHashConfig.Seed);
        }


        [Fact]
        public void xxHashFactory_Create_Config_IsNull_Throws()
        {
            var xxHashFactoryInstance = xxHashFactory.Instance;

            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => xxHashFactoryInstance.Create(null))
                    .ParamName);
        }

        [Fact]
        public void xxHashFactory_Create_Config_Works()
        {
            var XxHashConfig = new xxHashConfig() {
                HashSizeInBits = 32,
                Seed = 1337UL,
            };

            var xxHashFactoryInstance = xxHashFactory.Instance;
            var xxHash = xxHashFactoryInstance.Create(XxHashConfig);

            Assert.NotNull(xxHash);
            Assert.IsType<xxHash_Implementation>(xxHash);


            var resultingXxHashConfig = xxHash.Config;

            Assert.Equal(XxHashConfig.HashSizeInBits, resultingXxHashConfig.HashSizeInBits);
            Assert.Equal(XxHashConfig.Seed, resultingXxHashConfig.Seed);
        }
    }
}
