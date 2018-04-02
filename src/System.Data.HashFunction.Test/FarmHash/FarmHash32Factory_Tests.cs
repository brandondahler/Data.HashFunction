using System;
using System.Collections.Generic;
using System.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.FarmHash
{
    public class FarmHash32Factory_Tests
    {
        [Fact]
        public void FarmHash32Factory_Instance_IsDefined()
        {
            Assert.NotNull(FarmHash32Factory.Instance);
            Assert.IsType<FarmHash32Factory>(FarmHash32Factory.Instance);
        }

        [Fact]
        public void FarmHash32Factory_Create_Works()
        {
            var farmHash32Factory = FarmHash32Factory.Instance;
            var farmHash32 = farmHash32Factory.Create();

            Assert.NotNull(farmHash32);
            Assert.IsType<FarmHash32_Implementation>(farmHash32);
        }

    }
}
