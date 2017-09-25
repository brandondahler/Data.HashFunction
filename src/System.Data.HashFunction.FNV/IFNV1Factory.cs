using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FNV
{
    public interface IFNV1Factory
    {
        IFNV1 Create();

        IFNV1 Create(IFNVConfig config);
    }
}
