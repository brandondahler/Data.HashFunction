using Moq;
using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.FarmHash
{
    public class FarmHash128FactoryBase_Tests
    {
        private class FarmHash128FactoryBase_Implementation
            : FarmHash128FactoryBase<IFarmHash128>
        {
            private readonly Func<IFarmHash128> _create;

            public FarmHash128FactoryBase_Implementation(Func<IFarmHash128> create)
            {
                _create = create;
            }

            public override IFarmHash128 Create() => _create();
        }

        [Fact]
        public void FarmHash128FactoryBase_IFarmHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHash128>();

            Func<IFarmHash128> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHash128FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHash128FactoryBase_IFarmHash128Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHash128>();

            Func<IFarmHash128> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHash128FactoryBase_Implementation(create);
            var farmHash = ((IFarmHash128Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
    }
}
