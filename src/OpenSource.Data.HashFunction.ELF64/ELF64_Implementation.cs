using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenSource.Data.HashFunction.Core.Utilities;

namespace OpenSource.Data.HashFunction.ELF64
{
    /// <summary>
    /// Implementation of the hash function used in the elf64 object file format as specified at 
    ///   http://downloads.openwatcom.org/ftp/devel/docs/elf-64-gen.pdf on page 17.
    ///
    /// Contrary to the name, the hash algorithm is only designed for 32-bit output hash sizes.
    /// </summary>
    internal class ELF64_Implementation
        : StreamableHashFunctionBase,
            IELF64
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
                    tempHashValue <<= 4;
                    tempHashValue += dataArray[currentOffset];

                    var tmp = tempHashValue & 0xF0000000;

                    if (tmp != 0)
                        tempHashValue ^= tmp >> 24;

                    tempHashValue &= 0x0FFFFFFF;
                }

                _hashValue = tempHashValue;
            }

            protected override IHashValue FinalizeHashValueInternal(CancellationToken cancellationToken)
            {
                return new HashValue(
                    BitConverter.GetBytes(_hashValue),
                    32);
            }
        }
    }
}
