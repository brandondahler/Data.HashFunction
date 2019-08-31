using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.MetroHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.MetroHash
{
    public class MetroHashFactoryBase_Tests
    {
        private class MetroHashFactoryBase_Implementation
            : MetroHashFactoryBase<IMetroHash>
        {
            private readonly Func<IMetroHash> _create;
            private readonly Func<IMetroHashConfig, IMetroHash> _createWithConfig;

            public MetroHashFactoryBase_Implementation(Func<IMetroHash> create, Func<IMetroHashConfig, IMetroHash> createWithConfig)
            {
                _create = create;
                _createWithConfig = createWithConfig;

            }

            public override IMetroHash Create() => _create();
            public override IMetroHash Create(IMetroHashConfig config) => _createWithConfig(config);
        }

        [Fact]
        public void MetroHashFactoryBase_IMetroHashFactory_Create_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedMetroHash = Mock.Of<IMetroHash>();

            Func<IMetroHash> create = () => {
                timesCalled += 1;

                return expectedMetroHash;
            };

            Func<IMetroHashConfig, IMetroHash> createWithConfig = config => throw new NotImplementedException();


            var metroHashFactory = new MetroHashFactoryBase_Implementation(create, createWithConfig);
            var metroHash = ((IMetroHashFactory) metroHashFactory).Create();

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedMetroHash, metroHash);
        }

        [Fact]
        public void MetroHashFactoryBase_IMetroHashFactory_Create_WithConfig_CallsImplementation()
        {
            var timesCalled = 0;
            var expectedMetroHash = Mock.Of<IMetroHash>();

            Func<IMetroHash> create = () => throw new NotImplementedException();
            Func<IMetroHashConfig, IMetroHash> createWithConfig = config => {
                timesCalled += 1;

                return expectedMetroHash;
            };


            var metroHashFactory = new MetroHashFactoryBase_Implementation(create, createWithConfig);
            var metroHash = ((IMetroHashFactory)metroHashFactory).Create(Mock.Of<IMetroHashConfig>());

            Assert.Equal(1, timesCalled);
            Assert.Equal(expectedMetroHash, metroHash);
        }
    }
}
