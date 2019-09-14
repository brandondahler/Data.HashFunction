using Moq;
using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.FarmHash
{
    public class FarmHashFactoryBase_Tests
    {
        private class FarmHashFactoryBase_Implementation
            : FarmHashFactoryBase<IFarmHashFingerprint32>
        {
            private readonly Func<IFarmHashFingerprint32> _create;

            public FarmHashFactoryBase_Implementation(Func<IFarmHashFingerprint32> create)
            {
                _create = create;
            }

            public override IFarmHashFingerprint32 Create() => _create();
        }

        [Fact]
        public void FarmHashFactoryBase_IFarmHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedFarmHash = Mock.Of<IFarmHashFingerprint32>();

            Func<IFarmHashFingerprint32> create = () => {
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
