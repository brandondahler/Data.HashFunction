using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Data.HashFunction.Utilities.UnifiedData;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Test.Mocks
{
    public class HashFunctionImpl
            : HashFunctionAsyncBase
    {
        public HashFunctionImpl()
            : base(0)
        {

        }


        protected override byte[] ComputeHashInternal(UnifiedData data)
        {
            if (HashSize != 0)
                throw new InvalidOperationException("HashSize set to an invalid value.");

            return new byte[0];
        }
        
        protected override async Task<byte[]> ComputeHashAsyncInternal(UnifiedData data)
        {
            if (HashSize != 0)
                throw new InvalidOperationException("HashSize set to an invalid value.");

            return await Task.FromResult(new byte[0])
                .ConfigureAwait(false);
        }
    }
}
