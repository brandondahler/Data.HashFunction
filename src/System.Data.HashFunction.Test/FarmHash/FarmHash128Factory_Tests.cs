using System;
using System.Collections.Generic;
using System.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.FarmHash
{
    public class FarmHash128Factory_Tests
    {
        [Fact]
        public void FarmHash128Factory_Instance_IsDefined()
        {
            Assert.NotNull(FarmHash128Factory.Instance);
            Assert.IsType<FarmHash128Factory>(FarmHash128Factory.Instance);
        }

        [Fact]
        public void FarmHash128Factory_Create_Works()
        {
            var farmHash128Factory = FarmHash128Factory.Instance;
            var farmHash128 = farmHash128Factory.Create();

            Assert.NotNull(farmHash128);
            Assert.IsType<FarmHash128_Implementation>(farmHash128);
        }

    }
}
