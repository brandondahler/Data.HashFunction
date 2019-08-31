using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Implementation of one of FarmHash's Hash128 methods as specified at https://github.com/google/farmhash.
    /// </summary>
    public interface IFarmHashHash128
        : IFarmHashFingerprint128,
            IFarmHashHash
    {

    }
}
