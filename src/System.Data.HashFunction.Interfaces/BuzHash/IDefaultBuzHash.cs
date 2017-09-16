using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.BuzHash
{
    /// <summary>
    /// Basic implementation of <see cref="IBuzHash" />.  
    /// 
    /// Uses randomly generated table.
    /// </summary>
    /// <seealso cref="IHashFunctionAsync" />
    /// <seealso cref="IBuzHash" />
    public interface IDefaultBuzHash
        : IHashFunctionAsync,
            IBuzHash
    {

    }
}
