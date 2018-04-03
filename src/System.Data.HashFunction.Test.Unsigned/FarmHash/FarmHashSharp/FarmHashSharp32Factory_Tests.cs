using System;
using System.Collections.Generic;
using System.Data.HashFunction.FarmHash.FarmHashSharp;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.FarmHash.FarmHashSharp
{
    public class FarmHashSharp32Factory_Tests
    {
        [Fact]
        public void FarmHashSharp32Factory_Instance_IsDefined()
        {
            Assert.NotNull(FarmHashSharp32Factory.Instance);
            Assert.IsType<FarmHashSharp32Factory>(FarmHashSharp32Factory.Instance);
        }

        [Fact]
        public void FarmHashSharp32Factory_Create_Works()
        {
            var farmHashSharp32Factory = FarmHashSharp32Factory.Instance;
            var farmHashSharp32 = farmHashSharp32Factory.Create();

            Assert.NotNull(farmHashSharp32);
            Assert.IsType<FarmHashSharp32_Implementation>(farmHashSharp32);
        }

    }
}
