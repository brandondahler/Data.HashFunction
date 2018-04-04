using System;
using System.Collections.Generic;
using System.Data.HashFunction.FarmHash.FarmHashSharp;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.FarmHash.FarmHashSharp
{
    public class FarmHashSharpHash64Factory_Tests
    {
        [Fact]
        public void FarmHashSharpHash64Factory_Instance_IsDefined()
        {
            Assert.NotNull(FarmHashSharpHash64Factory.Instance);
            Assert.IsType<FarmHashSharpHash64Factory>(FarmHashSharpHash64Factory.Instance);
        }

        [Fact]
        public void FarmHashSharpHash64Factory_Create_Works()
        {
            var farmHashSharpHash64Factory = FarmHashSharpHash64Factory.Instance;
            var farmHashSharpHash64 = farmHashSharpHash64Factory.Create();

            Assert.NotNull(farmHashSharpHash64);
            Assert.IsType<FarmHashSharpHash64_Implementation>(farmHashSharpHash64);
        }

    }
}
