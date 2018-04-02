using System;
using System.Collections.Generic;
using System.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.FarmHash
{
    public class FarmHash64Factory_Tests
    {
        [Fact]
        public void FarmHash64Factory_Instance_IsDefined()
        {
            Assert.NotNull(FarmHash64Factory.Instance);
            Assert.IsType<FarmHash64Factory>(FarmHash64Factory.Instance);
        }

        [Fact]
        public void FarmHash64Factory_Create_Works()
        {
            var farmHash64Factory = FarmHash64Factory.Instance;
            var farmHash64 = farmHash64Factory.Create();

            Assert.NotNull(farmHash64);
            Assert.IsType<FarmHash64_Implementation>(farmHash64);
        }

    }
}
