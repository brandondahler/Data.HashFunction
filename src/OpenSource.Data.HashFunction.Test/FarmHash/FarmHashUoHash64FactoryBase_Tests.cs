using Moq;
using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.FarmHash
{
    public class FarmHashUoHash64FactoryBase_Tests
    {
        private class FarmHashUoHash64FactoryBase_Implementation
            : FarmHashUoHash64FactoryBase<IFarmHashUoHash64>
        {
            private readonly Func<IFarmHashUoHash64> _create;

            public FarmHashUoHash64FactoryBase_Implementation(Func<IFarmHashUoHash64> create)
            {
                _create = create;
            }

            public override IFarmHashUoHash64 Create() => _create();
        }

        [Fact]
        public void FarmHashUoHash64FactoryBase_IFarmHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashUoHash64>();

            Func<IFarmHashUoHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashUoHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
        
        [Fact]
        public void FarmHashUoHash64FactoryBase_IFarmHashHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashUoHash64>();

            Func<IFarmHashUoHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashUoHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashUoHash64FactoryBase_IFarmHash64Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashUoHash64>();

            Func<IFarmHashUoHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashUoHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHash64Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashUoHash64FactoryBase_IFarmHashHash64Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashUoHash64>();

            Func<IFarmHashUoHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashUoHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashHash64Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashUoHash64FactoryBase_IFarmHashUoHash64Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashUoHash64>();

            Func<IFarmHashUoHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashUoHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashUoHash64Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
    }
}
