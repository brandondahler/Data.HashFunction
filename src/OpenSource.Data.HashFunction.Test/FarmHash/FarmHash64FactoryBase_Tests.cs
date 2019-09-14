using Moq;
using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.FarmHash
{
    public class FarmHash64FactoryBase_Tests
    {
        private class FarmHash64FactoryBase_Implementation
            : FarmHash64FactoryBase<IFarmHash64>
        {
            private readonly Func<IFarmHash64> _create;

            public FarmHash64FactoryBase_Implementation(Func<IFarmHash64> create)
            {
                _create = create;
            }

            public override IFarmHash64 Create() => _create();
        }

        [Fact]
        public void FarmHash64FactoryBase_IFarmHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHash64>();

            Func<IFarmHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }

        [Fact]
        public void FarmHash64FactoryBase_IFarmHash64Factory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHash64>();

            Func<IFarmHash64> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHash64FactoryBase_Implementation(create);
            var farmHash = ((IFarmHash64Factory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
    }
}
