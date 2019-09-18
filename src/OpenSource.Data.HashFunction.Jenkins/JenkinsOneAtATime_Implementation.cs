using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core;
using OpenSource.Data.HashFunction.Core.Utilities;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSource.Data.HashFunction.Jenkins
{
    internal class JenkinsOneAtATime_Implementation
        : StreamableHashFunctionBase,
            IJenkinsOneAtATime
    {
        public override int HashSizeInBits { get; } = 32;


        public override IBlockTransformer CreateBlockTransformer() =>
            new BlockTransformer();


        private class BlockTransformer
            : BlockTransformerBase<BlockTransformer>
        {
            private UInt32 _hashValue;


            protected override void CopyStateTo(BlockTransformer other)
            {
                base.CopyStateTo(other);

                other._hashValue = _hashValue;
            }

            protected override void TransformByteGroupsInternal(ArraySegment<byte> data)
            {
                var dataArray = data.Array;
                var endOffset = data.Offset + data.Count;

                var tempHashValue = _hashValue;

                for (var currentOffset = data.Offset; currentOffset < endOffset; ++currentOffset)
                {
                    tempHashValue += dataArray[currentOffset];
                    tempHashValue += (tempHashValue << 10);
                    tempHashValue ^= (tempHashValue >> 6);
                }

                _hashValue = tempHashValue;
            }

            protected override IHashValue FinalizeHashValueInternal(CancellationToken cancellationToken)
            {
                var finalHashValue = _hashValue;
                finalHashValue += finalHashValue << 3;
                finalHashValue ^= finalHashValue >> 11;
                finalHashValue += finalHashValue << 15;

                return new HashValue(
                    BitConverter.GetBytes(finalHashValue),
                    32);
            }
        }
    }
}
