using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.FarmHash
{
    public class FarmHashFingerprint64Factory_Tests
    {
        [Fact]
        public void FarmHashFingerprint64Factory_Instance_IsDefined()
        {
            Assert.NotNull(FarmHashFingerprint64Factory.Instance);
            Assert.IsType<FarmHashFingerprint64Factory>(FarmHashFingerprint64Factory.Instance);
        }

        [Fact]
        public void FarmHashFingerprint64Factory_Create_Works()
        {
            var farmHashFingerprint64Factory = FarmHashFingerprint64Factory.Instance;
            var farmHashFingerprint64 = farmHashFingerprint64Factory.Create();

            Assert.NotNull(farmHashFingerprint64);
            Assert.IsType<FarmHashFingerprint64_Implementation>(farmHashFingerprint64);
        }

    }
}
