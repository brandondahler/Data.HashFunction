using Moq;
using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.FarmHash
{
    public class FarmHashCcHash32FactoryBase_Tests
    {
        private class FarmHashCcHash32FactoryBase_Implementation
            : FarmHashCcHash32FactoryBase<IFarmHashCcHash32>
        {
            private readonly Func<IFarmHashCcHash32> _create;

            public FarmHashCcHash32FactoryBase_Implementation(Func<IFarmHashCcHash32> create)
            {
                _create = create;
            }

            public override IFarmHashCcHash32 Create() => _create();
        }

        [Fact]
        public void FarmHashCcHash32FactoryBase_IFarmHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashCcHash32>();

            Func<IFarmHashCcHash32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashCcHash32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashCcHash32FactoryBase_IFarmHashHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashCcHash32>();

            Func<IFarmHashCcHash32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashCcHash32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashCcHash32FactoryBase_IFarmHash32Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashCcHash32>();

            Func<IFarmHashCcHash32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashCcHash32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHash32Factory)farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashCcHash32FactoryBase_IFarmHashHash32Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashCcHash32>();

            Func<IFarmHashCcHash32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashCcHash32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashHash32Factory)farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashCcHash32FactoryBase_IFarmHashCcHash32Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashCcHash32>();

            Func<IFarmHashCcHash32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashCcHash32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashCcHash32Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
    }
}
