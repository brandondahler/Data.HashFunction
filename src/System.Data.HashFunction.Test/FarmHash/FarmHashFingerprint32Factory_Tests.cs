using System;
using System.Collections.Generic;
using System.Data.HashFunction.FarmHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.FarmHash
{
    public class FarmHashFingerprint32Factory_Tests
    {
        [Fact]
        public void FarmHashFingerprint32Factory_Instance_IsDefined()
        {
            Assert.NotNull(FarmHashFingerprint32Factory.Instance);
            Assert.IsType<FarmHashFingerprint32Factory>(FarmHashFingerprint32Factory.Instance);
        }

        [Fact]
        public void FarmHashFingerprint32Factory_Create_Works()
        {
            var farmHashFingerprint32Factory = FarmHashFingerprint32Factory.Instance;
            var farmHashFingerprint32 = farmHashFingerprint32Factory.Create();

            Assert.NotNull(farmHashFingerprint32);
            Assert.IsType<FarmHashFingerprint32_Implementation>(farmHashFingerprint32);
        }

    }
}
