using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.FarmHash
{
    public class FarmHashFingerprintFactoryBase_Tests
    {
        private class FarmHashFingerprintFactoryBase_Implementation
            : FarmHashFingerprintFactoryBase<IFarmHashFingerprint32>
        {
            private readonly Func<IFarmHashFingerprint32> _create;

            public FarmHashFingerprintFactoryBase_Implementation(Func<IFarmHashFingerprint32> create)
            {
                _create = create;
            }

            public override IFarmHashFingerprint32 Create() => _create();
        }

        [Fact]
        public void FarmHashFingerprintFactoryBase_IFarmHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashFingerprint32>();

            Func<IFarmHashFingerprint32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashFingerprintFactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashFingerprintFactoryBase_IFarmHashFingerprintFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashFingerprint32>();

            Func<IFarmHashFingerprint32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashFingerprintFactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFingerprintFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
    }
}
