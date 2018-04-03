using System;
using System.Collections.Generic;
using System.Data.HashFunction.FarmHash.FarmHashSharp;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.FarmHash.FarmHashSharp
{
    public class FarmHashSharpHash32Factory_Tests
    {
        [Fact]
        public void FarmHashSharpHash32Factory_Instance_IsDefined()
        {
            Assert.NotNull(FarmHashSharpHash32Factory.Instance);
            Assert.IsType<FarmHashSharpHash32Factory>(FarmHashSharpHash32Factory.Instance);
        }

        [Fact]
        public void FarmHashSharpHash32Factory_Create_Works()
        {
            var farmHashSharpHash32Factory = FarmHashSharpHash32Factory.Instance;
            var farmHashSharpHash32 = farmHashSharpHash32Factory.Create();

            Assert.NotNull(farmHashSharpHash32);
            Assert.IsType<FarmHashSharpHash32_Implementation>(farmHashSharpHash32);
        }

    }
}
