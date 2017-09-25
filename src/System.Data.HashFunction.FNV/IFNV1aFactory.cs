using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FNV
{
    public interface IFNV1aFactory
    {
        IFNV1a Create();

        IFNV1a Create(IFNVConfig config);
    }
}
