using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.FarmHash
{
    public class FarmHashFactoryBase_Tests
    {
        private class FarmHashFactoryBase_Implementation
            : FarmHashFactoryBase<IFarmHash32>
        {
            private readonly Func<IFarmHash32> _create;

            public FarmHashFactoryBase_Implementation(Func<IFarmHash32> create)
            {
                _create = create;
            }

            public override IFarmHash32 Create() => _create();
        }

        [Fact]
        public void MetroHashFactoryBase_IMetroHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHash32>();

            Func<IFarmHash32> create = () => {
                timesCalled += 1;
                return expectedFarmHash;
            };

            var farmHashFactory = new FarmHashFactoryBase_Implementation(create);
            var farmHash = ((IFarmHashFactory) farmHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedFarmHash, farmHash);
        }
}
}
