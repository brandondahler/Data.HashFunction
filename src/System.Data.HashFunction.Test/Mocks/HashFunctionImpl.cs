using System;
using System.Collections.Generic;
using System.Data.HashFunction.Core;
using System.Data.HashFunction.Core.Utilities;
using System.Data.HashFunction.Core.Utilities.UnifiedData;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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


        protected override byte[] ComputeHashInternal(IUnifiedData data, CancellationToken cancellationToken)
        {
            return new byte[0];
        }
        
        protected override async Task<byte[]> ComputeHashAsyncInternal(IUnifiedDataAsync data, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new byte[0])
                .ConfigureAwait(false);
        }
    }
}
