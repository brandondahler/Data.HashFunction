using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.ELF64
{
    public class ELF64Factory
        : IELF64Factory
    {
        public IELF64Factory Instance { get; } = new ELF64Factory();


        private ELF64Factory()
        {

        }


        public IELF64 Create()
        {
            return new ELF64_Implementation();
        }
    }
}
