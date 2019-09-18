using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core;
using OpenSource.Data.HashFunction.FarmHash.Utilities;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenSource.Data.HashFunction.Core.Utilities;

namespace OpenSource.Data.HashFunction.FarmHash
{
    internal class FarmHashFingerprint64_Implementation
        : HashFunctionBase,
            IFarmHashFingerprint64
    {
        private const UInt64 _k0 = 0xc3a5c85c97cb3127UL;
        private const UInt64 _k1 = 0xb492b66fbe98f273UL;
        private const UInt64 _k2 = 0x9ae16a3b2f90404fUL;

        private const UInt64 _seed = 81UL;


        public override int HashSizeInBits { get; } = 64;



        protected override IHashValue ComputeHashInternal(ArraySegment<byte> data, CancellationToken cancellationToken)
        {
            var dataCount = data.Count;


            UInt64 hashValue;

            if (dataCount > 64)
            {
                hashValue = ComputeHash65Plus(data, cancellationToken);

            } else if (dataCount > 32) {
                hashValue = ComputeHash33To64(data);

            } else if (dataCount > 16) {
                hashValue = ComputeHash17To32(data);

            } else {
                hashValue = ComputeHash0To16(data);
            }

            return new HashValue(
                BitConverter.GetBytes(hashValue),
                64);
        }

        private static UInt64 ComputeHash16(UInt64 u, UInt64 v, UInt64 mul)
        {
            var a = (u ^ v) * mul;
            a ^= (a >> 47);

            var b = (v ^ a) * mul;
            b ^= (b >> 47);
            b *= mul;

            return b;
        }

        private static UInt64 ComputeHash0To16(ArraySegment<byte> data)
        {
            var dataArray = data.Array;
            var dataOffset = data.Offset;
            var dataCount = data.Count;

            var endOffset = dataOffset + dataCount;

            if (dataCount >= 8)
            {
                var mul = _k2 + (UInt64) (dataCount * 2);
                var a = BitConverter.ToUInt64(dataArray, dataOffset) + _k2;
                var b = BitConverter.ToUInt64(dataArray, endOffset - 8);
                var c = (RotateRight(b, 37) * mul) + a;
                var d = (RotateRight(a, 25) + b) * mul;

                return ComputeHash16(c, d, mul);
            }

            if (dataCount >= 4)
            {
                var mul = _k2 + ((UInt64) dataCount * 2);
                UInt64 a = BitConverter.ToUInt32(dataArray, dataOffset);

                return ComputeHash16((UInt64) dataCount + (a << 3), BitConverter.ToUInt32(dataArray, endOffset - 4), mul);
            }

            if (dataCount > 0)
            {
                var a = dataArray[dataOffset];
                var b = dataArray[dataOffset + (dataCount >> 1)];
                var c = dataArray[endOffset - 1];

                var y = a + (((UInt32) b) << 8);
                var z = (UInt32) dataCount + (((UInt32) c) << 2);

                return ShiftMix(y * _k2 ^ z * _k0) * _k2;
            }

            return _k2;
        }

        private static UInt64 ComputeHash17To32(ArraySegment<byte> data)
        {
            var dataArray = data.Array;
            var dataOffset = data.Offset;
            var dataCount = data.Count;

            var endOffset = dataOffset + dataCount;

            var mul = _k2 + ((UInt64) dataCount * 2);
            var a = BitConverter.ToUInt64(dataArray, dataOffset) * _k1;
            var b = BitConverter.ToUInt64(dataArray, dataOffset + 8);
            var c = BitConverter.ToUInt64(dataArray, endOffset - 8) * mul;
            var d = BitConverter.ToUInt64(dataArray, endOffset - 16) * _k2;

            return ComputeHash16(
                RotateRight(a + b, 43) + RotateRight(c, 30) + d,
                a + RotateRight(b + _k2, 18) + c,
                mul);
        }

        private static UInt128 WeakHashLen32WithSeeds(UInt64 w, UInt64 x, UInt64 y, UInt64 z, UInt64 a, UInt64 b)
        {
            a += w;
            b = RotateRight(b + a + z, 21);

            var c = a;

            a += x;
            a += y;
            b += RotateRight(a, 44);

            return new UInt128(a + z, b + c);
        }

        private static UInt128 WeakHashLen32WithSeeds(byte[] dataArray, int dataOffset, UInt64 a, UInt64 b) =>
            WeakHashLen32WithSeeds(
                BitConverter.ToUInt64(dataArray, dataOffset),
                BitConverter.ToUInt64(dataArray, dataOffset + 8),
                BitConverter.ToUInt64(dataArray, dataOffset + 16),
                BitConverter.ToUInt64(dataArray, dataOffset + 24),
                a,
                b);

        private static UInt64 ComputeHash33To64(ArraySegment<byte> data)
        {
            var dataArray = data.Array;
            var dataOffset = data.Offset;
            var dataCount = data.Count;

            var endOffset = dataOffset + dataCount;

            var mul = _k2 + ((UInt64) dataCount * 2);
            var a = BitConverter.ToUInt64(dataArray, dataOffset) * _k2;
            var b = BitConverter.ToUInt64(dataArray, dataOffset + 8);
            var c = BitConverter.ToUInt64(dataArray, endOffset - 8) * mul;
            var d = BitConverter.ToUInt64(dataArray, endOffset - 16) * _k2;

            var y = RotateRight(a + b, 43) + RotateRight(c, 30) + d;
            var z = ComputeHash16(y, a + RotateRight(b + _k2, 18) + c, mul);

            var e = BitConverter.ToUInt64(dataArray, dataOffset + 16) * mul;
            var f = BitConverter.ToUInt64(dataArray, dataOffset + 24);
            var g = (y + BitConverter.ToUInt64(dataArray, endOffset - 32)) * mul;
            var h = (z + BitConverter.ToUInt64(dataArray, endOffset - 24)) * mul;

            return ComputeHash16(
                RotateRight(e + f, 43) + RotateRight(g, 30) + h,
                e + RotateRight(f + a, 18) + g,
                mul);
        }

        private static UInt64 ComputeHash65Plus(ArraySegment<byte> data, CancellationToken cancellationToken)
        {
            var dataArray = data.Array;
            var dataOffset = data.Offset;
            var dataCount = data.Count;

            var endOffset = dataOffset + dataCount;


            var x = _seed;
            var y = (unchecked(_seed * _k1) + 113);
            var z = ShiftMix(y * _k2 + 113) * _k2;

            var v = new UInt128();
            var w = new UInt128();

            x = (x * _k2) + BitConverter.ToUInt64(dataArray, dataOffset);

            // Process 64-byte groups, leaving a final group of 1-64 bytes in size.
            {
                var groupEndOffset = endOffset - 64;

                for (var currentOffset = dataOffset; currentOffset < groupEndOffset; currentOffset += 64)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    x = RotateRight(x + y + v.Low + BitConverter.ToUInt64(dataArray, currentOffset + 8), 37) * _k1;
                    y = RotateRight(y + v.High + BitConverter.ToUInt64(dataArray, currentOffset + 48), 42) * _k1;
                    x ^= w.High;
                    y += v.Low + BitConverter.ToUInt64(dataArray, currentOffset + 40);
                    z = RotateRight(z + w.Low, 33) * _k1;
                    v = WeakHashLen32WithSeeds(dataArray, currentOffset, v.High * _k1, x + w.Low);
                    w = WeakHashLen32WithSeeds(dataArray, currentOffset + 32, z + w.High, y + BitConverter.ToUInt64(dataArray, currentOffset + 16));

                    Swap(ref z, ref x);
                }
            }

            cancellationToken.ThrowIfCancellationRequested();


            var mul = _k1 + ((z & 0xff) << 1);

            w.Low += (UInt64) ((dataCount - 1) & 63);
            v.Low += w.Low;
            w.Low += v.Low;

            x = RotateRight(x + y + v.Low + BitConverter.ToUInt64(dataArray, endOffset - 56), 37) * mul;
            y = RotateRight(y + v.High + BitConverter.ToUInt64(dataArray, endOffset - 16), 42) * mul;

            x ^= w.High * 9;
            y += v.Low * 9 + BitConverter.ToUInt64(dataArray, endOffset - 24);
            z = RotateRight(z + w.Low, 33) * mul;

            v = WeakHashLen32WithSeeds(dataArray, endOffset - 64, v.High * mul, x + w.Low);
            w = WeakHashLen32WithSeeds(dataArray, endOffset - 32, z + w.High, y + BitConverter.ToUInt64(dataArray, endOffset - 48));

            Swap(ref z, ref x);

            cancellationToken.ThrowIfCancellationRequested();

            return ComputeHash16(
                ComputeHash16(v.Low, w.Low, mul) + ShiftMix(y) * _k0 + z,
                ComputeHash16(v.High, w.High, mul) + x,
                mul);
        }
        
        #region Utilities

        private static UInt64 RotateRight(UInt64 operand, int shiftCount)
        {
            shiftCount &= 0x3f;

            return
                (operand >> shiftCount) |
                (operand << (64 - shiftCount));
        }

        private static UInt64 ShiftMix(UInt64 value) =>
            value ^ (value >> 47);

        private static void Swap(ref UInt64 first, ref UInt64 second)
        {
            var temp = first;

            first = second;
            second = temp;
        }

        #endregion
    }
}
