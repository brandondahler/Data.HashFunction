using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.SpookyHash
{
    public interface ISpookyHashV2Factory
    {
        ISpookyHashV2 Create();

        ISpookyHashV2 Create(ISpookyHashConfig config);
    }
}
