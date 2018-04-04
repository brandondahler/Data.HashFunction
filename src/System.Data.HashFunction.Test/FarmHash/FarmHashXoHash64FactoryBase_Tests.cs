using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.FarmHash
{
    public class FarmHashXoHash64FactoryBase_Tests
    {
        private class FarmHashXoHash64FactoryBase_Implementation
            : FarmHashXoHash64FactoryBase<IFarmHashXoHash64>
        {
            private readonly Func<IFarmHashXoHash64> _create;

            public FarmHashXoHash64FactoryBase_Implementation(Func<IFarmHashXoHash64> create)
            {
                _create = create;
            }

            public override IFarmHashXoHash64 Create() => _create();
        }

        [Fact]
        public void FarmHashXoHash64FactoryBase_IFarmHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashXoHash64>();

            Func<IFarmHashXoHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashXoHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
        
        [Fact]
        public void FarmHashXoHash64FactoryBase_IFarmHashHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashXoHash64>();

            Func<IFarmHashXoHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashXoHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashXoHash64FactoryBase_IFarmHash64Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashXoHash64>();

            Func<IFarmHashXoHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashXoHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHash64Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashXoHash64FactoryBase_IFarmHashHash64Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashXoHash64>();

            Func<IFarmHashXoHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashXoHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashHash64Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHashXoHash64FactoryBase_IFarmHashXoHash64Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashXoHash64>();

            Func<IFarmHashXoHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashXoHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashXoHash64Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
    }
}
