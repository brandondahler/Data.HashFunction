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
    internal class JenkinsLookup3_Implementation
        : HashFunctionBase,
            IJenkinsLookup3
    {

        public IJenkinsLookup3Config Config => _config.Clone();

        public override int HashSizeInBits => _config.HashSizeInBits;


        private readonly IJenkinsLookup3Config _config;

        private static readonly IEnumerable<int> _validHashSizes = new HashSet<int>() { 32, 64 };


        public JenkinsLookup3_Implementation(IJenkinsLookup3Config config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Clone();


            if (!_validHashSizes.Contains(_config.HashSizeInBits))
                throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.HashSizeInBits)}", _config.HashSizeInBits, $"{nameof(config)}.{nameof(config.HashSizeInBits)} must be contained within JenkinsLookup3.ValidHashSizes.");
        }


        protected override IHashValue ComputeHashInternal(ArraySegment<byte> data, CancellationToken cancellationToken)
        {
            UInt32 a = 0xdeadbeef + (UInt32) data.Count + _config.Seed;
            UInt32 b = a;
            UInt32 c = a;

            if (_config.HashSizeInBits == 64)
                c += _config.Seed2;

            var dataArray = data.Array;
            var dataOffset = data.Offset;
            var dataCount = data.Count;

            var remainderCount = dataCount % 12;
            {
                if (remainderCount == 0 && dataCount > 0)
                    remainderCount = 12;
            }

            var remainderOffset = dataOffset + dataCount - remainderCount;

            // Main group processing
            int currentOffset = dataOffset;
            {
                while (currentOffset < remainderOffset)
                {
                    a += BitConverter.ToUInt32(dataArray, currentOffset);
                    b += BitConverter.ToUInt32(dataArray, currentOffset + 4);
                    c += BitConverter.ToUInt32(dataArray, currentOffset + 8);

                    Mix(ref a, ref b, ref c);

                    currentOffset += 12;
                }
            }

            // Remainder processing
            {
                Debug.Assert(remainderCount >= 0);
                Debug.Assert(remainderCount <= 12);

                switch (remainderCount)
                {
                    case 12:
                        c += BitConverter.ToUInt32(dataArray, currentOffset + 8);
                        goto case 8;

                    case 11: c += (UInt32) dataArray[currentOffset + 10] << 16; goto case 10;
                    case 10: c += (UInt32) dataArray[currentOffset + 9] << 8; goto case 9;
                    case 9:  c += (UInt32) dataArray[currentOffset + 8]; goto case 8;

                    case 8:
                        b += BitConverter.ToUInt32(dataArray, currentOffset + 4);
                        goto case 4;

                    case 7: b += (UInt32) dataArray[currentOffset + 6] << 16; goto case 6;
                    case 6: b += (UInt32) dataArray[currentOffset + 5] << 8; goto case 5;
                    case 5: b += (UInt32) dataArray[currentOffset + 4]; goto case 4;

                    case 4:
                        a += BitConverter.ToUInt32(dataArray, currentOffset);

                        Final(ref a, ref b, ref c);
                        break;

                    case 3: a += (UInt32) dataArray[currentOffset + 2] << 16; goto case 2;
                    case 2: a += (UInt32) dataArray[currentOffset + 1] << 8; goto case 1;
                    case 1:
                        a += (UInt32) dataArray[currentOffset];

                        Final(ref a, ref b, ref c);
                        break;
                }
            }


            byte[] hash;

            switch (_config.HashSizeInBits)
            {
                case 32:
                    hash = BitConverter.GetBytes(c);
                    break;

                case 64:
                    hash = BitConverter.GetBytes((((UInt64) b) << 32) | c);
                    break;

                default:
                    throw new NotImplementedException();
            }

            return new HashValue(hash, _config.HashSizeInBits);
        }

        private void Mix(ref UInt32 a, ref UInt32 b, ref UInt32 c)
        {
            a -= c; a ^= RotateLeft(c, 4); c += b;
            b -= a; b ^= RotateLeft(a,  6); a += c;
            c -= b; c ^= RotateLeft(b,  8); b += a;

            a -= c; a ^= RotateLeft(c, 16); c += b;
            b -= a; b ^= RotateLeft(a, 19); a += c;
            c -= b; c ^= RotateLeft(b,  4); b += a;
        }

        private void Final(ref UInt32 a, ref UInt32 b, ref UInt32 c)
        {
            c ^= b; c -= RotateLeft(b, 14);
            a ^= c; a -= RotateLeft(c, 11);
            b ^= a; b -= RotateLeft(a, 25);

            c ^= b; c -= RotateLeft(b, 16);
            a ^= c; a -= RotateLeft(c,  4);
            b ^= a; b -= RotateLeft(a, 14);

            c ^= b; c -= RotateLeft(b, 24);
        }


        private static UInt32 RotateLeft(UInt32 operand, int shiftCount)
        {
            shiftCount &= 0x1f;

            return
                (operand << shiftCount) |
                (operand >> (32 - shiftCount));
        }
    }
}
