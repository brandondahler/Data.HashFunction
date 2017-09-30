using System;
using System.Collections.Generic;
using System.Data.HashFunction.Blake2;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.Blake2
{
    public class Blake2BFactory_Tests
    {
        [Fact]
        public void Blake2BFactory_Instance_IsDefined()
        {
            Assert.NotNull(Blake2BFactory.Instance);
            Assert.IsType<Blake2BFactory>(Blake2BFactory.Instance);
        }

        [Fact]
        public void Blake2BFactory_Create_Works()
        {
            var defaultBlake2BConfig = new Blake2BConfig();

            var blake2BFactory = Blake2BFactory.Instance;
            var blake2BHash = blake2BFactory.Create();

            Assert.NotNull(blake2BHash);
            Assert.IsType<Blake2B_Implementation>(blake2BHash);


            var resultingBlake2BConfig = blake2BHash.Config;

            Assert.Equal(defaultBlake2BConfig.HashSizeInBits, resultingBlake2BConfig.HashSizeInBits);

            Assert.Equal(defaultBlake2BConfig.Key, resultingBlake2BConfig.Key);
            Assert.Equal(defaultBlake2BConfig.Salt, resultingBlake2BConfig.Salt);
            Assert.Equal(defaultBlake2BConfig.Personalization, resultingBlake2BConfig.Personalization);
        }


        [Fact]
        public void Blake2BFactory_Create_Config_IsNull_Throws()
        {
            var blake2BFactory = Blake2BFactory.Instance;

            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => blake2BFactory.Create(null))
                    .ParamName);
        }

        [Fact]
        public void Blake2BFactory_Create_Config_Works()
        {
            var blake2BConfig = new Blake2BConfig() {
                HashSizeInBits = 256,
                Key = new byte[64],
                Salt = new byte[16],
                Personalization = new byte[16],
            };

            var blake2BFactory = Blake2BFactory.Instance;
            var blake2BHash = blake2BFactory.Create(blake2BConfig);

            Assert.NotNull(blake2BHash);
            Assert.IsType<Blake2B_Implementation>(blake2BHash);


            var resultingBlake2BConfig = blake2BHash.Config;

            Assert.Equal(blake2BConfig.HashSizeInBits, resultingBlake2BConfig.HashSizeInBits);

            Assert.Equal(blake2BConfig.Key, resultingBlake2BConfig.Key);
            Assert.Equal(blake2BConfig.Salt, resultingBlake2BConfig.Salt);
            Assert.Equal(blake2BConfig.Personalization, resultingBlake2BConfig.Personalization);
        }
    }
}
