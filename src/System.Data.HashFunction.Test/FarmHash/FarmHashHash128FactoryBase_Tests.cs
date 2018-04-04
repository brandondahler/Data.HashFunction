using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.FarmHash
{
    public class FarmHashHash128FactoryBase_Tests
    {
        private class FarmHashHash128FactoryBase_Implementation
            : FarmHashHash128FactoryBase<IFarmHashHash128>
        {
            private readonly Func<IFarmHashHash128> _create;

            public FarmHashHash128FactoryBase_Implementation(Func<IFarmHashHash128> create)
            {
                _create = create;
            }

            public override IFarmHashHash128 Create() => _create();
        }

        [Fact]
        public void FarmHashHash128FactoryBase_IFarmHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashHash128>();

            Func<IFarmHashHash128> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashHash128FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashHash128FactoryBase_IFarmHashHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashHash128>();

            Func<IFarmHashHash128> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashHash128FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
        
        [Fact]
        public void FarmHashHash128FactoryBase_IFarmHashFingerprintFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashHash128>();

            Func<IFarmHashHash128> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashHash128FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFingerprintFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashHash128FactoryBase_IFarmHash128Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashHash128>();

            Func<IFarmHashHash128> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashHash128FactoryBase_Implementation(create);
            var farmHash = ((IFarmHash128Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashHash128FactoryBase_IFarmHashHash128Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashHash128>();

            Func<IFarmHashHash128> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashHash128FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashHash128Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
        
        [Fact]
        public void FarmHashHash128FactoryBase_IFarmHashFingerprint128Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashHash128>();

            Func<IFarmHashHash128> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashHash128FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFingerprint128Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

    }
}
