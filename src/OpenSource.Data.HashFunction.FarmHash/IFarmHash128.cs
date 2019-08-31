using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Implementation of one of FarmHash's 128-bit methods as specified at https://github.com/google/farmhash.
    /// </summary>
    public interface IFarmHash128
        : IFarmHash
    {
    }
}
