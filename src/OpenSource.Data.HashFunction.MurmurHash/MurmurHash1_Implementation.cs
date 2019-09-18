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

namespace OpenSource.Data.HashFunction.MurmurHash
{
    internal class MurmurHash1_Implementation
        : HashFunctionBase,
            IMurmurHash1
    {

        public IMurmurHash1Config Config => _config.Clone();

        public override int HashSizeInBits { get; } = 32;


        private const UInt32 _m = 0XC6A4A793;


        private readonly IMurmurHash1Config _config;


        public MurmurHash1_Implementation(IMurmurHash1Config config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Clone();
        }


        protected override IHashValue ComputeHashInternal(ArraySegment<byte> data, CancellationToken cancellationToken)
        {
            var dataArray = data.Array;
            var dataOffset = data.Offset;
            var dataCount = data.Count;

            var endOffset = dataOffset + dataCount;
            var remainderCount = dataCount % 4;

            UInt32 hashValue = _config.Seed ^ ((UInt32) dataCount * _m);

            // Process 4-byte groups
            {
                var groupEndOffset = endOffset - remainderCount;

                for (var currentOffset = dataOffset; currentOffset < groupEndOffset; currentOffset += 4)
                {
                    hashValue += BitConverter.ToUInt32(dataArray, currentOffset);
                    hashValue *= _m;
                    hashValue ^= hashValue >> 16;
                }
            }

            // Process remainder
            if (remainderCount > 0)
            {
                var remainderOffset = endOffset - remainderCount;

                switch (remainderCount)
                {
                    case 3: hashValue += (UInt32) dataArray[remainderOffset + 2] << 16; goto case 2;
                    case 2: hashValue += (UInt32) dataArray[remainderOffset + 1] << 8; goto case 1;
                    case 1:
                        hashValue += (UInt32) dataArray[remainderOffset];
                        break;
                };

                hashValue *= _m;
                hashValue ^= hashValue >> 16;
            }


            hashValue *= _m;
            hashValue ^= hashValue >> 10;
            hashValue *= _m;
            hashValue ^= hashValue >> 17;

            return new HashValue(
                BitConverter.GetBytes(hashValue),
                32);
        }
    }
}
