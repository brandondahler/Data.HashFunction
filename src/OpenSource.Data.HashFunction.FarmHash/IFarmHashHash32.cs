using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Implementation of one of FarmHash's Hash32 methods as specified at https://github.com/google/farmhash.
    /// </summary>
    public interface IFarmHashHash32
        : IFarmHash32,
            IFarmHashHash
    {

    }
}
