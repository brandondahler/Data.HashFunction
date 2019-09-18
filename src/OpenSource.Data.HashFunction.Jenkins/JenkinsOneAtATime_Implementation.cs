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
    /// <summary>
    /// Implementation of Bob Jenkins' One-at-a-Time hash function as specified at http://www.burtleburtle.net/bob/hash/doobs.html (function named "one_at_a_time").
    /// 
    /// This hash function has been superseded by JenkinsLookup2 and JenkinsLookup3.
    /// </summary>
    internal class JenkinsOneAtATime_Implementation
        : StreamableHashFunctionBase,
            IJenkinsOneAtATime
    {
        public override int HashSizeInBits { get; } = 32;


        public override IHashFunctionBlockTransformer CreateBlockTransformer() =>
            new BlockTransformer();


        private class BlockTransformer
            : HashFunctionBlockTransformerBase<BlockTransformer>
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
