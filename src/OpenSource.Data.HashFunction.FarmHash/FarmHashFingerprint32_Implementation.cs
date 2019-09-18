using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenSource.Data.HashFunction.Core.Utilities;

namespace OpenSource.Data.HashFunction.FarmHash
{
    internal class FarmHashFingerprint32_Implementation
        : HashFunctionBase,
            IFarmHashFingerprint32
    {
        private const UInt32 c1 = 0xcc9e2d51;
        private const UInt32 c2 = 0x1b873593;


        public override int HashSizeInBits { get; } = 32;


        protected override IHashValue ComputeHashInternal(ArraySegment<byte> data, CancellationToken cancellationToken)
        {
            var dataCount = data.Count;

            UInt32 hashValue;

            if (dataCount > 24)
            {
                hashValue = ComputeHash25Plus(data, cancellationToken);

            } else if (dataCount > 12) {
                hashValue = ComputeHash13To24(data);

            } else if (dataCount > 4) {
                hashValue = ComputeHash5To12(data);

            } else {
                hashValue = ComputeHash0To4(data);
            }

            return new HashValue(
                BitConverter.GetBytes(hashValue),
                32);
        }
        
        private static UInt32 ComputeHash25Plus(ArraySegment<byte> data, CancellationToken cancellationToken)
        {
            var dataArray = data.Array;
            var dataOffset = data.Offset;
            var dataCount = data.Count;

            var endOffset = dataOffset + dataCount;


            var h = (UInt32) dataCount;
            var g = (UInt32) (c1 * dataCount);
            var f = g;

            var a0 = RotateRight(BitConverter.ToUInt32(dataArray, endOffset - 4) * c1, 17) * c2;
            var a1 = RotateRight(BitConverter.ToUInt32(dataArray, endOffset - 8) * c1, 17) * c2;
            var a2 = RotateRight(BitConverter.ToUInt32(dataArray, endOffset - 16) * c1, 17) * c2;
            var a3 = RotateRight(BitConverter.ToUInt32(dataArray, endOffset - 12) * c1, 17) * c2;
            var a4 = RotateRight(BitConverter.ToUInt32(dataArray, endOffset - 20) * c1, 17) * c2;

            h ^= a0;
            h = RotateRight(h, 19);
            h = h * 5 + 0xe6546b64;
            h ^= a2;
            h = RotateRight(h, 19);
            h = h * 5 + 0xe6546b64;
            g ^= a1;
            g = RotateRight(g, 19);
            g = g * 5 + 0xe6546b64;
            g ^= a3;
            g = RotateRight(g, 19);
            g = g * 5 + 0xe6546b64;
            f += a4;
            f = RotateRight(f, 19) + 113;

            // Process groups of 20 bytes, leaving 1 to 20 bytes remaining.
            {
                var groupEndOffset = endOffset - 20;

                for (var currentOffset = dataOffset; currentOffset < groupEndOffset; currentOffset += 20)
                {

                    var a = BitConverter.ToUInt32(dataArray, currentOffset);
                    var b = BitConverter.ToUInt32(dataArray, currentOffset + 4);
                    var c = BitConverter.ToUInt32(dataArray, currentOffset + 8);
                    var d = BitConverter.ToUInt32(dataArray, currentOffset + 12);
                    var e = BitConverter.ToUInt32(dataArray, currentOffset + 16);

                    h += a;
                    g += b;
                    f += c;
                    h = Mur(d, h) + e;
                    g = Mur(c, g) + a;
                    f = Mur(b + e * c1, f) + d;
                    f += g;
                    g += f;
                }
            }


            g = RotateRight(g, 11) * c1;
            g = RotateRight(g, 17) * c1;
            f = RotateRight(f, 11) * c1;
            f = RotateRight(f, 17) * c1;
            h = RotateRight(h + g, 19);

            h = h * 5 + 0xe6546b64;
            h = RotateRight(h, 17) * c1;
            h = RotateRight(h + f, 19);
            h = h * 5 + 0xe6546b64;
            h = RotateRight(h, 17) * c1;

            return h;
        }


        private static UInt32 ComputeHash13To24(ArraySegment<byte> data)
        {
            var dataArray = data.Array;
            var dataOffset = data.Offset;
            var dataCount = data.Count;

            var endOffset = dataOffset + dataCount;

            var a = BitConverter.ToUInt32(dataArray, dataOffset + (dataCount >> 1) - 4);
            var b = BitConverter.ToUInt32(dataArray, dataOffset + 4);
            var c = BitConverter.ToUInt32(dataArray, endOffset - 8);
            var d = BitConverter.ToUInt32(dataArray, dataOffset + (dataCount >> 1));
            var e = BitConverter.ToUInt32(dataArray, dataOffset);
            var f = BitConverter.ToUInt32(dataArray, endOffset - 4);
            var h = (d * c1) + (UInt32) dataCount;


            a = RotateRight(a, 12) + f;
            h = Mur(c, h) + a;
            a = RotateRight(a, 3) + c;
            h = Mur(e, h) + a;
            a = RotateRight(a + f, 12) + d;
            h = Mur(b, h) + a;

            return FMix(h);
        }

        private static UInt32 ComputeHash5To12(ArraySegment<byte> data) 
        {
            var dataArray = data.Array;
            var dataOffset = data.Offset;
            var dataCount = data.Count;

            var endOffset = dataOffset + dataCount;

            var a = (UInt32) dataCount;
            var b = (UInt32) (dataCount * 5);
            var c = 9U;
            var d = b;

            a += BitConverter.ToUInt32(dataArray, dataOffset);
            b += BitConverter.ToUInt32(dataArray, endOffset - 4);
            c += BitConverter.ToUInt32(dataArray, dataOffset + ((dataCount >> 1) & 4));

            return FMix(Mur(c, Mur(b, Mur(a, d))));
        }
        
        private static UInt32 ComputeHash0To4(ArraySegment<byte> data) 
        {
            var dataArray = data.Array;
            var dataOffset = data.Offset;
            var dataCount = data.Count;

            var endOffset = dataOffset + dataCount;

            var b = 0U;
            var c = 9U;

            for (var currentOffset = dataOffset; currentOffset < endOffset; currentOffset += 1)
            {
                var v = (sbyte) dataArray[currentOffset];

                b = (UInt32) ((b * c1) + v);
                c ^= b;
            }

            return FMix(Mur(b, Mur((UInt32) dataCount, c)));
        }


        #region Utilities

        private static UInt32 FMix(UInt32 h)
        {
            h ^= h >> 16;
            h *= 0x85ebca6b;
            h ^= h >> 13;
            h *= 0xc2b2ae35;
            h ^= h >> 16;

            return h;
        }

        private static UInt32 Mur(UInt32 a, UInt32 h)
        {
            a *= c1;
            a = RotateRight(a, 17);
            a *= c2;

            h ^= a;
            h = RotateRight(h, 19);

            return (h * 5) + 0xe6546b64;
        }

        private static UInt32 RotateRight(UInt32 operand, int shiftCount)
        {
            shiftCount &= 0x1f;

            return
                (operand >> shiftCount) |
                (operand << (32 - shiftCount));
        }

        #endregion
    }
}
