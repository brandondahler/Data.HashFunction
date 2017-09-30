using System;
using System.Collections.Generic;
using System.Data.HashFunction.BernsteinHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.BernsteinHash
{
    public class ModifiedBernsteinHashFactory_Tests
    {
        [Fact]
        public void ModifiedBernsteinHashFactory_Instance_IsDefined()
        {
            Assert.NotNull(ModifiedBernsteinHashFactory.Instance);
            Assert.IsType<ModifiedBernsteinHashFactory>(ModifiedBernsteinHashFactory.Instance);
        }

        [Fact]
        public void ModifiedBernsteinHashFactory_Create_Works()
        {
            var modifiedBernsteinHashFactory = ModifiedBernsteinHashFactory.Instance;
            var modifiedBernsteinHash = modifiedBernsteinHashFactory.Create();

            Assert.NotNull(modifiedBernsteinHash);
            Assert.IsType<ModifiedBernsteinHash_Implementation>(modifiedBernsteinHash);
        }
    }
}
