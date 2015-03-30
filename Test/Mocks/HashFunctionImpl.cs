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
        public HashFunctionImpl(int hashSize)
            : base(hashSize)
        {

        }


        protected override byte[] ComputeHashInternal(UnifiedData data)
        {
            return new byte[0];
        }
        
        protected override async Task<byte[]> ComputeHashAsyncInternal(UnifiedData data)
        {
            return await Task.FromResult(new byte[0])
                .ConfigureAwait(false);
        }
    }
}
