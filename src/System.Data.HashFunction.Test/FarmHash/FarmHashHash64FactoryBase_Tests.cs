using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.FarmHash
{
    public class FarmHashHash64FactoryBase_Tests
    {
        private class FarmHashHash64FactoryBase_Implementation
            : FarmHashHash64FactoryBase<IFarmHashHash64>
        {
            private readonly Func<IFarmHashHash64> _create;

            public FarmHashHash64FactoryBase_Implementation(Func<IFarmHashHash64> create)
            {
                _create = create;
            }

            public override IFarmHashHash64 Create() => _create();
        }

        [Fact]
        public void FarmHashHash64FactoryBase_IFarmHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashHash64>();

            Func<IFarmHashHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
        
        [Fact]
        public void FarmHashHash64FactoryBase_IFarmHashHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashHash64>();

            Func<IFarmHashHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashHash64FactoryBase_IFarmHash64Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashHash64>();

            Func<IFarmHashHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHash64Factory)farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashHash64FactoryBase_IFarmHashHash64Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashHash64>();

            Func<IFarmHashHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashHash64Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
    }
}
