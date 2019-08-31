using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.FarmHash
{
    public class FarmHashFingerprint128Factory_Tests
    {
        [Fact]
        public void FarmHashFingerprint128Factory_Instance_IsDefined()
        {
            Assert.NotNull(FarmHashFingerprint128Factory.Instance);
            Assert.IsType<FarmHashFingerprint128Factory>(FarmHashFingerprint128Factory.Instance);
        }

        [Fact]
        public void FarmHashFingerprint128Factory_Create_Works()
        {
            var farmHashFingerprint128Factory = FarmHashFingerprint128Factory.Instance;
            var farmHashFingerprint128 = farmHashFingerprint128Factory.Create();

            Assert.NotNull(farmHashFingerprint128);
            Assert.IsType<FarmHashFingerprint128_Implementation>(farmHashFingerprint128);
        }

    }
}
