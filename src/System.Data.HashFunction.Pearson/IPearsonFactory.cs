using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Pearson
{
    public interface IPearsonFactory
    {
        IPearson Create();
        IPearson Create(IPearsonConfig config);
    }
}
