using System;
using System.Collections.Generic;
using System.Data.HashFunction.HashAlgorithm;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.HashAlgorithm
{
    public class HashAlgorithmWrapperConfig_Tests
    {
        [Fact]
        public void HashAlgorithmWrapperConfig_Defaults_HaventChanged()
        {
            var hashAlgorithmWrapperConfig = new HashAlgorithmWrapperConfig();


            Assert.Null(hashAlgorithmWrapperConfig.InstanceFactory);
        }

        [Fact]
        public void HashAlgorithmWrapperConfig_Clone_Works()
        {
            var hashAlgorithmWrapperConfig = new HashAlgorithmWrapperConfig() {
                InstanceFactory = () => SHA1.Create()
            };

            var hashAlgorithmWrapperConfigClone = hashAlgorithmWrapperConfig.Clone();

            Assert.IsType<HashAlgorithmWrapperConfig>(hashAlgorithmWrapperConfigClone);

            Assert.Equal(hashAlgorithmWrapperConfig.InstanceFactory, hashAlgorithmWrapperConfigClone.InstanceFactory);
        }
    }
}
