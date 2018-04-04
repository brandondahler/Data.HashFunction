using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.FarmHash
{
    public class FarmHashTeHash64FactoryBase_Tests
    {
        private class FarmHashTeHash64FactoryBase_Implementation
            : FarmHashTeHash64FactoryBase<IFarmHashTeHash64>
        {
            private readonly Func<IFarmHashTeHash64> _create;

            public FarmHashTeHash64FactoryBase_Implementation(Func<IFarmHashTeHash64> create)
            {
                _create = create;
            }

            public override IFarmHashTeHash64 Create() => _create();
        }

        [Fact]
        public void FarmHashTeHash64FactoryBase_IFarmHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashTeHash64>();

            Func<IFarmHashTeHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashTeHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
        
        [Fact]
        public void FarmHashTeHash64FactoryBase_IFarmHashHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashTeHash64>();

            Func<IFarmHashTeHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashTeHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashTeHash64FactoryBase_IFarmHash64Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashTeHash64>();

            Func<IFarmHashTeHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashTeHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHash64Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashTeHash64FactoryBase_IFarmHashHash64Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashTeHash64>();

            Func<IFarmHashTeHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashTeHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashHash64Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashTeHash64FactoryBase_IFarmHashTeHash64Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashTeHash64>();

            Func<IFarmHashTeHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashTeHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashTeHash64Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
    }
}
