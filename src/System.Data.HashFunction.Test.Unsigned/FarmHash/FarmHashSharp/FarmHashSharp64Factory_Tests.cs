using System;
using System.Collections.Generic;
using System.Data.HashFunction.FarmHash.FarmHashSharp;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.FarmHash.FarmHashSharp
{
    public class FarmHashSharp64Factory_Tests
    {
        [Fact]
        public void FarmHashSharp64Factory_Instance_IsDefined()
        {
            Assert.NotNull(FarmHashSharp64Factory.Instance);
            Assert.IsType<FarmHashSharp64Factory>(FarmHashSharp64Factory.Instance);
        }

        [Fact]
        public void FarmHashSharp64Factory_Create_Works()
        {
            var farmHashSharp64Factory = FarmHashSharp64Factory.Instance;
            var farmHashSharp64 = farmHashSharp64Factory.Create();

            Assert.NotNull(farmHashSharp64);
            Assert.IsType<FarmHashSharp64_Implementation>(farmHashSharp64);
        }

    }
}
