using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.ELF64;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.ELF64
{
    public class ELF64Factory_Tests
    {
        [Fact]
        public void ELF64Factory_Instance_IsDefined()
        {
            Assert.NotNull(ELF64Factory.Instance);
            Assert.IsType<ELF64Factory>(ELF64Factory.Instance);
        }

        [Fact]
        public void ELF64Factory_Create_Works()
        {
            var elf64Factory = ELF64Factory.Instance;
            var elf64 = elf64Factory.Create();

            Assert.NotNull(elf64);
            Assert.IsType<ELF64_Implementation>(elf64);
        }
    }
}
