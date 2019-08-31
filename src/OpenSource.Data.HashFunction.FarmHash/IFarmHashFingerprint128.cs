using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSource.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Implementation of FarmHash's Fingerprint128 method as specified at https://github.com/google/farmhash.
    /// </summary>
    public interface IFarmHashFingerprint128
        : IFarmHash128,
            IFarmHashFingerprint
            
    {

    }
}
