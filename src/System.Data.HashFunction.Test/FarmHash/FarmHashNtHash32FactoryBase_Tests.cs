using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.FarmHash
{
    public class FarmHashNtHash32FactoryBase_Tests
    {
        private class FarmHashNtHash32FactoryBase_Implementation
            : FarmHashNtHash32FactoryBase<IFarmHashNtHash32>
        {
            private readonly Func<IFarmHashNtHash32> _create;

            public FarmHashNtHash32FactoryBase_Implementation(Func<IFarmHashNtHash32> create)
            {
                _create = create;
            }

            public override IFarmHashNtHash32 Create() => _create();
        }

        [Fact]
        public void FarmHashNtHash32FactoryBase_IFarmHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashNtHash32>();

            Func<IFarmHashNtHash32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashNtHash32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashNtHash32FactoryBase_IFarmHashHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashNtHash32>();

            Func<IFarmHashNtHash32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashNtHash32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashNtHash32FactoryBase_IFarmHash32Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashNtHash32>();

            Func<IFarmHashNtHash32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashNtHash32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHash32Factory)farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashNtHash32FactoryBase_IFarmHashHash32Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashNtHash32>();

            Func<IFarmHashNtHash32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashNtHash32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashHash32Factory)farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashNtHash32FactoryBase_IFarmHashNtHash32Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashNtHash32>();

            Func<IFarmHashNtHash32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashNtHash32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashNtHash32Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
    }
}
