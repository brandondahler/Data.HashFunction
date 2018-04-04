using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.FarmHash
{
    public class FarmHashSuHash32FactoryBase_Tests
    {
        private class FarmHashSuHash32FactoryBase_Implementation
            : FarmHashSuHash32FactoryBase<IFarmHashSuHash32>
        {
            private readonly Func<IFarmHashSuHash32> _create;

            public FarmHashSuHash32FactoryBase_Implementation(Func<IFarmHashSuHash32> create)
            {
                _create = create;
            }

            public override IFarmHashSuHash32 Create() => _create();
        }

        [Fact]
        public void FarmHashSuHash32FactoryBase_IFarmHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashSuHash32>();

            Func<IFarmHashSuHash32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashSuHash32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
        
        [Fact]
        public void FarmHashSuHash32FactoryBase_IFarmHashHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashSuHash32>();

            Func<IFarmHashSuHash32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashSuHash32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashSuHash32FactoryBase_IFarmHash32Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashSuHash32>();

            Func<IFarmHashSuHash32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashSuHash32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHash32Factory)farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashSuHash32FactoryBase_IFarmHashHash32Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashSuHash32>();

            Func<IFarmHashSuHash32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashSuHash32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashHash32Factory)farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashSuHash32FactoryBase_IFarmHashSuHash32Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashSuHash32>();

            Func<IFarmHashSuHash32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashSuHash32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashSuHash32Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
    }
}
