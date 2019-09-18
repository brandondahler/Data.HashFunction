using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core;
using OpenSource.Data.HashFunction.Core.Utilities;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSource.Data.HashFunction.Test._Mocks
{
    public class HashFunctionImpl
            : HashFunctionBase
    {
        public Func<ArraySegment<byte>, CancellationToken, IHashValue> OnComputeHashInternal { get; set; } = (_, __) => new HashValue(new byte[1], 1);


        public override int HashSizeInBits { get; }



        public HashFunctionImpl()
            : this(1)
        {

        }

        public HashFunctionImpl(int hashSize)
        {
            HashSizeInBits = hashSize;
        }


        protected override IHashValue ComputeHashInternal(ArraySegment<byte> data, CancellationToken cancellationToken)
        {
            return OnComputeHashInternal(data, cancellationToken);
        }
    }
}
