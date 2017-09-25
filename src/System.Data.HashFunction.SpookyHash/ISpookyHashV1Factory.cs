using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.SpookyHash
{
    public interface ISpookyHashV1Factory
    {
        ISpookyHashV1 Create();

        ISpookyHashV1 Create(ISpookyHashConfig config);
    }
}
