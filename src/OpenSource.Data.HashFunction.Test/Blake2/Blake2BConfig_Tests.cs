using System;
using System.Collections.Generic;
using System.Data.HashFunction.Blake2;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.Blake2
{
    public class Blake2BConfig_Tests
    {
        [Fact]
        public void Blake2BConfig_Defaults_HaventChanged()
        {
            var blake2BConfig = new Blake2BConfig();

            Assert.Equal(512, blake2BConfig.HashSizeInBits);

            Assert.Null(blake2BConfig.Key);
            Assert.Null(blake2BConfig.Salt);
            Assert.Null(blake2BConfig.Personalization);
        }

        [Fact]
        public void Blake2BConfig_Clone_Works()
        {
            var blake2BConfig = new Blake2BConfig() {
                HashSizeInBits = 256,
                Key = new byte[64],
                Salt = new byte[16],
                Personalization = new byte[16],
            };

            var blake2BConfigClone = blake2BConfig.Clone();

            Assert.IsType<Blake2BConfig>(blake2BConfigClone);

            Assert.Equal(blake2BConfig.HashSizeInBits, blake2BConfigClone.HashSizeInBits);

            Assert.Equal(blake2BConfig.Key, blake2BConfigClone.Key);
            Assert.Equal(blake2BConfig.Salt, blake2BConfigClone.Salt);
            Assert.Equal(blake2BConfig.Personalization, blake2BConfigClone.Personalization);
        }

        [Fact]
        public void Blake2BConfig_Clone_WithNullArrays_Works()
        {
            var blake2BConfig = new Blake2BConfig() {
                Key = null,
                Salt = null,
                Personalization = null,
            };


            var blake2BConfigClone = blake2BConfig.Clone();

            Assert.IsType<Blake2BConfig>(blake2BConfigClone);

            Assert.Null(blake2BConfigClone.Key);
            Assert.Null(blake2BConfigClone.Salt);
            Assert.Null(blake2BConfigClone.Personalization);
        }
    }
}
