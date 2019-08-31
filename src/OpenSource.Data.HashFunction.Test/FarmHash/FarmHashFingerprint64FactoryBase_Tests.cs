using Moq;
using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.FarmHash
{
    public class FarmHashFingerprint64FactoryBase_Tests
    {
        private class FarmHashFingerprint64FactoryBase_Implementation
            : FarmHashFingerprint64FactoryBase<IFarmHashFingerprint64>
        {
            private readonly Func<IFarmHashFingerprint64> _create;

            public FarmHashFingerprint64FactoryBase_Implementation(Func<IFarmHashFingerprint64> create)
            {
                _create = create;
            }

            public override IFarmHashFingerprint64 Create() => _create();
        }

        [Fact]
        public void FarmHashFingerprint64FactoryBase_IFarmHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashFingerprint64>();

            Func<IFarmHashFingerprint64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashFingerprint64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashFingerprint64FactoryBase_IFarmHashFingerprintFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashFingerprint64>();

            Func<IFarmHashFingerprint64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashFingerprint64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFingerprintFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
        
        [Fact]
        public void FarmHashFingerprint64FactoryBase_IFarmHashHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashFingerprint64>();

            Func<IFarmHashFingerprint64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashFingerprint64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashFingerprint64FactoryBase_IFarmHash64Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashFingerprint64>();

            Func<IFarmHashFingerprint64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashFingerprint64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHash64Factory)farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashFingerprint64FactoryBase_IFarmHashFingerprint64Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashFingerprint64>();

            Func<IFarmHashFingerprint64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashFingerprint64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFingerprint64Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashFingerprint64FactoryBase_IFarmHashHash64Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashFingerprint64>();

            Func<IFarmHashFingerprint64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashFingerprint64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashHash64Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashFingerprint64FactoryBase_IFarmHashNaHash64Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashFingerprint64>();

            Func<IFarmHashFingerprint64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashFingerprint64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashNaHash64Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

    }
}
