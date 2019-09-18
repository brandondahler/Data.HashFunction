using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core;
using OpenSource.Data.HashFunction.Core.Utilities;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSource.Data.HashFunction.Jenkins
{
    internal class JenkinsLookup2_Implementation
        : StreamableHashFunctionBase, 
            IJenkinsLookup2
    {
        public IJenkinsLookup2Config Config => _config.Clone();

        public override int HashSizeInBits { get; } = 32;


        private IJenkinsLookup2Config _config;



        public JenkinsLookup2_Implementation(IJenkinsLookup2Config config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Clone();
        }



        public override IBlockTransformer CreateBlockTransformer() =>
            new BlockTransformer(_config);


        private class BlockTransformer
            : BlockTransformerBase<BlockTransformer>
        {
            private UInt32 _a;
            private UInt32 _b;
            private UInt32 _c;

            private UInt32 _bytesProcessed;


            public BlockTransformer()
                : base(inputBlockSize: 12)
            {
            }

            public BlockTransformer(IJenkinsLookup2Config config)
                : this()
            {
                _a = 0x9e3779b9;
                _b = 0x9e3779b9;
                _c = config.Seed;

                _bytesProcessed = 0;
            }

            protected override void CopyStateTo(BlockTransformer other)
            {
                other._a = _a;
                other._b = _b;
                other._c = _c;

                other._bytesProcessed = _bytesProcessed;
            }

            protected override void TransformByteGroupsInternal(ArraySegment<byte> data)
            {
                Debug.Assert(data.Count % 12 == 0);

                var dataArray = data.Array;
                var dataCount = data.Count;
                var endOffset = data.Offset + dataCount;

                var tempA = _a;
                var tempB = _b;
                var tempC = _c;

                for (var currentOffset = data.Offset; currentOffset < endOffset; currentOffset += 12)
                {
                    tempA += BitConverter.ToUInt32(dataArray, currentOffset);
                    tempB += BitConverter.ToUInt32(dataArray, currentOffset + 4);
                    tempC += BitConverter.ToUInt32(dataArray, currentOffset + 8);

                    Mix(ref tempA, ref tempB, ref tempC);
                }

                _a = tempA;
                _b = tempB;
                _c = tempC;

                _bytesProcessed += (UInt32) dataCount;
            }

            protected override IHashValue FinalizeHashValueInternal(CancellationToken cancellationToken)
            {
                var remainder = FinalizeInputBuffer;
                var remainderLength = (remainder?.Length).GetValueOrDefault();

                Debug.Assert(remainderLength >= 0);
                Debug.Assert(remainderLength < 12);

                var finalA = _a;
                var finalB = _b;
                var finalC = _c;

                // All the case statements fall through on purpose
                switch (remainderLength)
                {
                    case 11: finalC += (UInt32) remainder[10] << 24; goto case 10;
                    case 10: finalC += (UInt32) remainder[9] << 16; goto case 9;
                    case 9:  finalC += (UInt32) remainder[8] << 8; goto case 8;
                    // the first byte of c is reserved for the length

                    case 8:
                        finalB += BitConverter.ToUInt32(remainder, 4);
                        goto case 4;

                    case 7: finalB += (UInt32) remainder[6] << 16; goto case 6;
                    case 6: finalB += (UInt32) remainder[5] << 8; goto case 5;
                    case 5: finalB += (UInt32) remainder[4]; goto case 4;

                    case 4:
                        finalA += BitConverter.ToUInt32(remainder, 0);
                        break;

                    case 3: finalA += (UInt32)remainder[2] << 16; goto case 2;
                    case 2: finalA += (UInt32)remainder[1] << 8; goto case 1;
                    case 1:
                        finalA += (UInt32)remainder[0];
                        break;
                }

                finalC += _bytesProcessed + (UInt32) remainderLength;

                Mix(ref finalA, ref finalB, ref finalC);

                return new HashValue(
                    BitConverter.GetBytes(finalC),
                    32);
            }

            private static void Mix(ref UInt32 a, ref UInt32 b, ref UInt32 c)
            {
                a -= b; a -= c; a ^= (c >> 13);
                b -= c; b -= a; b ^= (a << 8);
                c -= a; c -= b; c ^= (b >> 13);

                a -= b; a -= c; a ^= (c >> 12);
                b -= c; b -= a; b ^= (a << 16);
                c -= a; c -= b; c ^= (b >> 5);

                a -= b; a -= c; a ^= (c >> 3);
                b -= c; b -= a; b ^= (a << 10);
                c -= a; c -= b; c ^= (b >> 15);
            }
        }
    }
}
