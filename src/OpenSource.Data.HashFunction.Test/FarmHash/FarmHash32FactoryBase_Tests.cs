using Moq;
using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.FarmHash
{
    public class FarmHash32FactoryBase_Tests
    {
        private class FarmHash32FactoryBase_Implementation
            : FarmHash32FactoryBase<IFarmHash32>
        {
            private readonly Func<IFarmHash32> _create;

            public FarmHash32FactoryBase_Implementation(Func<IFarmHash32> create)
            {
                _create = create;
            }

            public override IFarmHash32 Create() => _create();
        }

        [Fact]
        public void FarmHash32FactoryBase_IFarmHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHash32>();

            Func<IFarmHash32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHash32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHash32FactoryBase_IFarmHash32Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHash32>();

            Func<IFarmHash32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHash32FactoryBase_Implementation(create);
            var farmHash = ((IFarmHash32Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
    }
}
