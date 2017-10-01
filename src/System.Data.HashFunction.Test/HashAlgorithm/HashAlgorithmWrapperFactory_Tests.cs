using System;
using System.Collections.Generic;
using System.Data.HashFunction.HashAlgorithm;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.HashAlgorithm
{
    public class HashAlgorithmWrapperFactory_Tests
    {
        [Fact]
        public void HashAlgorithmWrapperFactory_Instance_IsDefined()
        {
            Assert.NotNull(HashAlgorithmWrapperFactory.Instance);
            Assert.IsType<HashAlgorithmWrapperFactory>(HashAlgorithmWrapperFactory.Instance);
        }
        
        [Fact]
        public void HashAlgorithmWrapperFactory_Create_Config_IsNull_Throws()
        {
            var hashAlgorithmWrapperFactory = HashAlgorithmWrapperFactory.Instance;

            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => hashAlgorithmWrapperFactory.Create(null))
                    .ParamName);
        }

        [Fact]
        public void HashAlgorithmWrapperFactory_Create_Config_Works()
        {
            var hashAlgorithmWrapperConfig = new HashAlgorithmWrapperConfig() {
                InstanceFactory = () => SHA1.Create()
            };

            var hashAlgorithmWrapperFactory = HashAlgorithmWrapperFactory.Instance;
            var hashAlgorithmWrapper = hashAlgorithmWrapperFactory.Create(hashAlgorithmWrapperConfig);

            Assert.NotNull(hashAlgorithmWrapper);
            Assert.IsType<HashAlgorithmWrapper_Implementation>(hashAlgorithmWrapper);


            var resultingHashAlgorithmWrapperConfig = hashAlgorithmWrapper.Config;

            Assert.Equal(hashAlgorithmWrapperConfig.InstanceFactory, resultingHashAlgorithmWrapperConfig.InstanceFactory);
        }
    }
}
