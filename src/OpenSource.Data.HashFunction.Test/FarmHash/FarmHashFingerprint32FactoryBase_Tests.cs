using Moq;
using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.FarmHash
{
    public class FarmHashFingerprint32FactoryBase_Tests
    {
        private class FarmHashFingerprint32FactoryBase_Implementation
            : FarmHashFingerprint32FactoryBase<IFarmHashFingerprint32>
        {
            private readonly Func<IFarmHashFingerprint32> _create;

            public FarmHashFingerprint32FactoryBase_Implementation(Func<IFarmHashFingerprint32> create)
            {
                _create = create;
            }

            public override IFarmHashFingerprint32 Create() => _create();
        }

        [Fact]
        public void FarmHashFingerprint32FactoryBase_IFarmHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashFingerprint32>();

            Func<IFarmHashFingerprint32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashFingerprint32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
        
        [Fact]
        public void FarmHashFingerprint32FactoryBase_IFarmHashFingerprintFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashFingerprint32>();

            Func<IFarmHashFingerprint32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashFingerprint32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFingerprintFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
        
        [Fact]
        public void FarmHashFingerprint32FactoryBase_IFarmHashHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashFingerprint32>();

            Func<IFarmHashFingerprint32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashFingerprint32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
        
        [Fact]
        public void FarmHashFingerprint32FactoryBase_IFarmHash32Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashFingerprint32>();

            Func<IFarmHashFingerprint32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashFingerprint32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHash32Factory)farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashFingerprint32FactoryBase_IFarmHashFingerprint32Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashFingerprint32>();

            Func<IFarmHashFingerprint32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashFingerprint32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFingerprint32Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashFingerprint32FactoryBase_IFarmHashHash32Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashFingerprint32>();

            Func<IFarmHashFingerprint32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashFingerprint32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashHash32Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
        
        [Fact]
        public void FarmHashFingerprint32FactoryBase_IFarmHashMkHash32Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashFingerprint32>();

            Func<IFarmHashFingerprint32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashFingerprint32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashMkHash32Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

    }
}
