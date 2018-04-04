using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.FarmHash
{
    public class FarmHashFingerprint128FactoryBase_Tests
    {
        private class FarmHashFingerprint128FactoryBase_Implementation
            : FarmHashFingerprint128FactoryBase<IFarmHashFingerprint128>
        {
            private readonly Func<IFarmHashFingerprint128> _create;

            public FarmHashFingerprint128FactoryBase_Implementation(Func<IFarmHashFingerprint128> create)
            {
                _create = create;
            }

            public override IFarmHashFingerprint128 Create() => _create();
        }

        [Fact]
        public void FarmHashFingerprint128FactoryBase_IFarmHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashFingerprint128>();

            Func<IFarmHashFingerprint128> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashFingerprint128FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
        
        [Fact]
        public void FarmHashFingerprint128FactoryBase_IFarmHashFingerprintFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashFingerprint128>();

            Func<IFarmHashFingerprint128> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashFingerprint128FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFingerprintFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashFingerprint128FactoryBase_IFarmHash128Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashFingerprint128>();

            Func<IFarmHashFingerprint128> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashFingerprint128FactoryBase_Implementation(create);
            var farmHash = ((IFarmHash128Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashFingerprint128FactoryBase_IFarmHashFingerprint128Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashFingerprint128>();

            Func<IFarmHashFingerprint128> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashFingerprint128FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFingerprint128Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
    }
}
