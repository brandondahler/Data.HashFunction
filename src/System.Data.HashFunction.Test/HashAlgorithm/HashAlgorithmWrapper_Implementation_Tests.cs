using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.HashAlgorithm;
using System.Data.HashFunction.Test._Utilities;
using System.Security.Cryptography;
using System.Text;

namespace System.Data.HashFunction.Test.HashAlgorithm
{
    using HashAlgorithm = Security.Cryptography.HashAlgorithm;

    public class HashAlgorithmWrapper_Implementation_Tests
    {
        public class IHashFunction_Tests_SHA1
            : IHashFunction_TestBase<IHashAlgorithmWrapper>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(160, TestConstants.Empty, "da39a3ee5e6b4b0d3255bfef95601890afd80709"),
                    new KnownValue(160, TestConstants.FooBar, "8843d7f92416211de9ebb963ff4ce28125932878"),
                    new KnownValue(160, TestConstants.LoremIpsum, "2dd4010f15f21c9e26e31a693ba31c6ab78a5a4c"),
                    new KnownValue(160, TestConstants.RandomShort, "d64df40c72068b01e7dfb5ceb2b519ad3b483eb0"),
                    new KnownValue(160, TestConstants.RandomLong, "e5901cb4679133729c5555210c3cfe3e5851a2aa"),
                };

            protected override IHashAlgorithmWrapper CreateHashFunction(int hashSize) => 
                new HashAlgorithmWrapper_Implementation(
                    new HashAlgorithmWrapperConfig() {
                        InstanceFactory = SHA1.Create
                    });
        }
    

        public class IHashFunction_Tests_SHA256
            : IHashFunction_TestBase<IHashAlgorithmWrapper>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(256, TestConstants.FooBar, "c3ab8ff13720e8ad9047dd39466b3c8974e592c2fa383d4a3960714caef0c4f2"),
                };

            protected override IHashAlgorithmWrapper CreateHashFunction(int hashSize) => 
                new HashAlgorithmWrapper_Implementation(
                    new HashAlgorithmWrapperConfig() {
                        InstanceFactory = SHA256.Create
                    });
        }
    

        public class IHashFunction_Tests_SHA384
            : IHashFunction_TestBase<IHashAlgorithmWrapper>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(384, TestConstants.FooBar, "3c9c30d9f665e74d515c842960d4a451c83a0125fd3de7392d7b37231af10c72ea58aedfcdf89a5765bf902af93ecf06"),
                };

            protected override IHashAlgorithmWrapper CreateHashFunction(int hashSize) => 
                new HashAlgorithmWrapper_Implementation(
                    new HashAlgorithmWrapperConfig() {
                        InstanceFactory = SHA384.Create
                    });
        }
    

        public class IHashFunction_Tests_SHA512
            : IHashFunction_TestBase<IHashAlgorithmWrapper>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(512, TestConstants.FooBar, "0a50261ebd1a390fed2bf326f2673c145582a6342d523204973d0219337f81616a8069b012587cf5635f6925f1b56c360230c19b273500ee013e030601bf2425"),
                };

            protected override IHashAlgorithmWrapper CreateHashFunction(int hashSize) => 
                new HashAlgorithmWrapper_Implementation(
                    new HashAlgorithmWrapperConfig() {
                        InstanceFactory = SHA512.Create
                    });
        }
    

        public class IHashFunction_Tests_MD5
            : IHashFunction_TestBase<IHashAlgorithmWrapper>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(128, TestConstants.FooBar, "3858f62230ac3c915f300c664312c63f"),
                };

            protected override IHashAlgorithmWrapper CreateHashFunction(int hashSize) => 
                new HashAlgorithmWrapper_Implementation(
                    new HashAlgorithmWrapperConfig() {
                        InstanceFactory = MD5.Create
                    });
        }
    
    }
}
