using System;
using System.Collections.Generic;
using System.Data.HashFunction.BernsteinHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.BernsteinHash
{
    public class BernsteinHashFactory_Tests
    {
        [Fact]
        public void BernsteinHashFactory_Instance_IsDefined()
        {
            Assert.NotNull(BernsteinHashFactory.Instance);
            Assert.IsType<BernsteinHashFactory>(BernsteinHashFactory.Instance);
        }

        [Fact]
        public void BernsteinHashFactory_Create_Works()
        {
            var bernsteinHashFactory = BernsteinHashFactory.Instance;
            var bernsteinHash = bernsteinHashFactory.Create();

            Assert.NotNull(bernsteinHash);
            Assert.IsType<BernsteinHash_Implementation>(bernsteinHash);
        }
    }
}
