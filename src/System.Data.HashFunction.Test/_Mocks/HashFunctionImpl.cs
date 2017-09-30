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

namespace System.Data.HashFunction.Test._Mocks
{
    public class HashFunctionImpl
            : HashFunctionAsyncBase
    {
        public Func<IUnifiedData, CancellationToken, byte[]> OnComputeHashInternal { get; set; } = (_, __) => new byte[0];
        public Func<IUnifiedData, CancellationToken, Task<byte[]>> OnComputeHashAsyncInternal { get; set; } = (_, __) => Task.FromResult(new byte[0]);


        public override int HashSizeInBits { get; }



        public HashFunctionImpl()
            : this(0)
        {

        }

        public HashFunctionImpl(int hashSize)
        {
            HashSizeInBits = hashSize;
        }


        protected override byte[] ComputeHashInternal(IUnifiedData data, CancellationToken cancellationToken)
        {
            return OnComputeHashInternal(data, cancellationToken);
        }
        
        protected override Task<byte[]> ComputeHashAsyncInternal(IUnifiedDataAsync data, CancellationToken cancellationToken)
        {
            return OnComputeHashAsyncInternal(data, cancellationToken);
        }
    }
}
