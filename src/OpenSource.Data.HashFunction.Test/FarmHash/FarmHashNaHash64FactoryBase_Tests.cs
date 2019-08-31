using Moq;
using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.FarmHash
{
    public class FarmHashNaHash64FactoryBase_Tests
    {
        private class FarmHashNaHash64FactoryBase_Implementation
            : FarmHashNaHash64FactoryBase<IFarmHashNaHash64>
        {
            private readonly Func<IFarmHashNaHash64> _create;

            public FarmHashNaHash64FactoryBase_Implementation(Func<IFarmHashNaHash64> create)
            {
                _create = create;
            }

            public override IFarmHashNaHash64 Create() => _create();
        }

        [Fact]
        public void FarmHashNaHash64FactoryBase_IFarmHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashNaHash64>();

            Func<IFarmHashNaHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashNaHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
        
        [Fact]
        public void FarmHashNaHash64FactoryBase_IFarmHashHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashNaHash64>();

            Func<IFarmHashNaHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashNaHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashNaHash64FactoryBase_IFarmHash64Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashNaHash64>();

            Func<IFarmHashNaHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashNaHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHash64Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashNaHash64FactoryBase_IFarmHashHash64Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashNaHash64>();

            Func<IFarmHashNaHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashNaHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashHash64Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashNaHash64FactoryBase_IFarmHashNaHash64Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashNaHash64>();

            Func<IFarmHashNaHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashNaHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashNaHash64Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
    }
}
