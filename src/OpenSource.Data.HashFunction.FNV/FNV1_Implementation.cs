using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core.Utilities;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using OpenSource.Data.HashFunction.FNV.Utilities;

namespace OpenSource.Data.HashFunction.FNV
{
    internal class FNV1_Implementation
        : FNV1Base,
            IFNV1
    {

        public FNV1_Implementation(IFNVConfig config)
            : base(config)
        {

        }


        public override IBlockTransformer CreateBlockTransformer()
        {
            switch (_config.HashSizeInBits)
            {
                case 32:
                    return new BlockTransformer_32Bit(_fnvPrimeOffset);

                case 64:
                    return new BlockTransformer_64Bit(_fnvPrimeOffset);

                default:
                    return new BlockTransformer_Extended(_fnvPrimeOffset);
            }
        }

        private class BlockTransformer_32Bit
            : BlockTransformer_32BitBase<BlockTransformer_32Bit>
        {
            public BlockTransformer_32Bit()
                : base()
            {

            }

            public BlockTransformer_32Bit(FNVPrimeOffset fnvPrimeOffset)
                : base(fnvPrimeOffset)
            {

            }


            protected override void TransformByteGroupsInternal(ArraySegment<byte> data)
            {
                var dataArray = data.Array;
                var dataCount = data.Count;
                var endOffset = data.Offset + dataCount;

                var tempHashValue = _hashValue;
                var tempPrime = _prime;

                for (int currentOffset = data.Offset; currentOffset < endOffset; ++currentOffset)
                {
                    tempHashValue *= tempPrime;
                    tempHashValue ^= dataArray[currentOffset];
                }

                _hashValue = tempHashValue;
            }
        }

        private class BlockTransformer_64Bit
            : BlockTransformer_64BitBase<BlockTransformer_64Bit>
        {
            public BlockTransformer_64Bit()
                : base()
            {

            }

            public BlockTransformer_64Bit(FNVPrimeOffset fnvPrimeOffset)
                : base(fnvPrimeOffset)
            {

            }


            protected override void TransformByteGroupsInternal(ArraySegment<byte> data)
            {
                var dataArray = data.Array;
                var dataCount = data.Count;
                var endOffset = data.Offset + dataCount;

                var tempHashValue = _hashValue;
                var tempPrime = _prime;

                for (int currentOffset = data.Offset; currentOffset < endOffset; ++currentOffset)
                {
                    tempHashValue *= tempPrime;
                    tempHashValue ^= dataArray[currentOffset];
                }

                _hashValue = tempHashValue;
            }
        }

        private class BlockTransformer_Extended
            : BlockTransformer_ExtendedBase<BlockTransformer_Extended>
        {
            public BlockTransformer_Extended()
                : base()
            {

            }

            public BlockTransformer_Extended(FNVPrimeOffset fnvPrimeOffset)
                : base(fnvPrimeOffset)
            {

            }


            protected override void TransformByteGroupsInternal(ArraySegment<byte> data)
            {
                var dataArray = data.Array;
                var dataCount = data.Count;
                var endOffset = data.Offset + dataCount;

                var tempHashValue = _hashValue;
                var tempPrime = _prime;

                var tempHashSizeInBytes = _hashSizeInBytes;

                for (int currentOffset = data.Offset; currentOffset < endOffset; ++currentOffset)
                {

                    tempHashValue = ExtendedMultiply(tempHashValue, tempPrime, tempHashSizeInBytes);
                    tempHashValue[0] ^= dataArray[currentOffset];
                }

                _hashValue = tempHashValue;
            }
        }
    }
}
