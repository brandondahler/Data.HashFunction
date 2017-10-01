using System;
using System.Collections.Generic;
using System.Data.HashFunction.ELF64;
using System.Data.HashFunction.Test._Utilities;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.ELF64
{
    public class ELF64_Implementation_Tests
    {

        #region HashSizeInBits

        [Fact]
        public void ELF64_Implementation_HashSizeInBits_IsNotChanged()
        {
            var elf64 = new ELF64_Implementation();

            Assert.Equal(64, elf64.HashSizeInBits);
        }

        #endregion



        public class IHashFunctionAsync_Tests_ELF64
            : IHashFunctionAsync_TestBase<IELF64>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                new KnownValue(32, TestConstants.Empty, 0x00000000),
                new KnownValue(32, TestConstants.FooBar, 0x06d65882),
                new KnownValue(32, TestConstants.LoremIpsum, 0x09e0a53e),
                };

            protected override IELF64 CreateHashFunction(int hashSize) => 
                new ELF64_Implementation();
        }

    }
}
