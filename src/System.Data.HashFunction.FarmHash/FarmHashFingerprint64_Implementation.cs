using System;
using System.Collections.Generic;
using System.Data.HashFunction.Core;
using System.Data.HashFunction.Core.Utilities.UnifiedData;
using System.Data.HashFunction.FarmHash.Utilities;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Implementation of FarmHash's Fingerprint64 method as specified at https://github.com/google/farmhash.
    /// </summary>
    internal class FarmHashFingerprint64_Implementation
        : HashFunctionAsyncBase,
            IFarmHashFingerprint64
    {
        private const UInt64 k0 = 0xc3a5c85c97cb3127UL;
        private const UInt64 k1 = 0xb492b66fbe98f273UL;
        private const UInt64 k2 = 0x9ae16a3b2f90404fUL;


        public override int HashSizeInBits { get; } = 64;

        


        protected override byte[] ComputeHashInternal(IUnifiedData data, CancellationToken cancellationToken)
        {
            var dataArray = data.ToArray(cancellationToken);

            return BitConverter.GetBytes(
                ComputeHashFromArray(dataArray));
        }

        protected override async Task<byte[]> ComputeHashAsyncInternal(IUnifiedDataAsync data, CancellationToken cancellationToken)
        {
            var dataArray = await data.ToArrayAsync(cancellationToken)
                .ConfigureAwait(false);

            return BitConverter.GetBytes(
                ComputeHashFromArray(dataArray));
        }


        private static UInt64 ComputeHashFromArray(byte[] dataArray)
        {
            var dataLength = dataArray.Length;

            var seed = 81UL;


            if (dataLength <= 32)
            {
                if (dataLength <= 16)
                    return ComputeHash0To16(dataArray);

                return ComputeHash17To32(dataArray);
            }

            if (dataLength <= 64)
                return ComputeHash33To64(dataArray);



            var x = seed;
            var y = ((seed * k1) + 113);
            var z = ShiftMix(y * k2 + 113) * k2;

            var v = new UInt128();
            var w = new UInt128();

            x = (x * k2) + BitConverter.ToUInt64(dataArray, 0);

            for (var i = 0; i < dataLength - 64; i += 64)
            {
                x = RotateRight(x + y + v.Low + BitConverter.ToUInt64(dataArray, i + 8), 37) * k1;
                y = RotateRight(y + v.High + BitConverter.ToUInt64(dataArray, i + 48), 42) * k1;
                x ^= w.High;
                y += v.Low + BitConverter.ToUInt64(dataArray, i + 40);
                z = RotateRight(z + w.Low, 33) * k1;
                v = WeakHashLen32WithSeeds(dataArray, i, v.High * k1, x + w.Low);
                w = WeakHashLen32WithSeeds(dataArray, i + 32, z + w.High, y + BitConverter.ToUInt64(dataArray, i + 16));

                Swap(ref z, ref x);
            }


            var mul = k1 + ((z & 0xff) << 1);

            w.Low += (UInt64) ((dataLength - 1) & 63);
            v.Low += w.Low;
            w.Low += v.Low;

            x = RotateRight(x + y + v.Low + BitConverter.ToUInt64(dataArray, dataLength - 64 + 8), 37) * mul;
            y = RotateRight(y + v.High + BitConverter.ToUInt64(dataArray, dataLength - 64 + 48), 42) * mul;

            x ^= w.High * 9;
            y += v.Low * 9 + BitConverter.ToUInt64(dataArray, dataLength - 64 + 40);
            z = RotateRight(z + w.Low, 33) * mul;

            v = WeakHashLen32WithSeeds(dataArray, dataLength - 64, v.High * mul, x + w.Low);
            w = WeakHashLen32WithSeeds(dataArray, dataLength - 64 + 32, z + w.High, y + BitConverter.ToUInt64(dataArray, dataLength - 64 + 16));

            Swap(ref z, ref x);

            return ComputeHash16(
                ComputeHash16(v.Low, w.Low, mul) + ShiftMix(y) * k0 + z,
                ComputeHash16(v.High, w.High, mul) + x,
                mul);
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

        private static UInt64 ComputeHash0To16(byte[] dataArray)
        {
            var dataLength = dataArray.Length;

            if (dataLength >= 8)
            {
                var mul = k2 + (UInt64) (dataLength * 2);
                var a = BitConverter.ToUInt64(dataArray, 0) + k2;
                var b = BitConverter.ToUInt64(dataArray, dataLength - 8);
                var c = (RotateRight(b, 37) * mul) + a;
                var d = (RotateRight(a, 25) + b) * mul;

                return ComputeHash16(c, d, mul);
            }

            if (dataLength >= 4)
            {
                var mul = k2 + ((UInt64) dataLength * 2);
                UInt64 a = BitConverter.ToUInt32(dataArray, 0);

                return ComputeHash16((UInt64) dataLength + (a << 3), BitConverter.ToUInt32(dataArray, dataLength - 4), mul);
            }

            if (dataLength > 0)
            {
                var a = dataArray[0];
                var b = dataArray[dataLength >> 1];
                var c = dataArray[dataLength - 1];

                var y = a + (((UInt32) b) << 8);
                var z = (UInt32) dataLength + (((UInt32) c) << 2);

                return ShiftMix(y * k2 ^ z* k0) * k2;
            }

            return k2;
        }

        private static UInt64 ComputeHash17To32(byte[] dataArray)
        {
            var dataLength = dataArray.Length;

            var mul = k2 + ((UInt64) dataLength * 2);
            var a = BitConverter.ToUInt64(dataArray, 0) * k1;
            var b = BitConverter.ToUInt64(dataArray, 8);
            var c = BitConverter.ToUInt64(dataArray, dataLength - 8) * mul;
            var d = BitConverter.ToUInt64(dataArray, dataLength - 16) * k2;

            return ComputeHash16(
                RotateRight(a + b, 43) + RotateRight(c, 30) + d,
                a + RotateRight(b + k2, 18) + c, 
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

        private static UInt128 WeakHashLen32WithSeeds(byte[] dataArray, int start, UInt64 a, UInt64 b) =>
            WeakHashLen32WithSeeds(
                BitConverter.ToUInt64(dataArray, start),
                BitConverter.ToUInt64(dataArray, start + 8),
                BitConverter.ToUInt64(dataArray, start + 16),
                BitConverter.ToUInt64(dataArray, start + 24),
                a,
                b);

        private static UInt64 ComputeHash33To64(byte[] dataArray)
        {
            var dataLength = dataArray.Length;
            var mul = k2 + ((UInt64) dataLength * 2);
            var a = BitConverter.ToUInt64(dataArray, 0) * k2;
            var b = BitConverter.ToUInt64(dataArray, 8);
            var c = BitConverter.ToUInt64(dataArray, dataLength - 8) * mul;
            var d = BitConverter.ToUInt64(dataArray, dataLength - 16) * k2;

            var y = RotateRight(a + b, 43) + RotateRight(c, 30) + d;
            var z = ComputeHash16(y, a + RotateRight(b + k2, 18) + c, mul);

            var e = BitConverter.ToUInt64(dataArray, 16) * mul;
            var f = BitConverter.ToUInt64(dataArray, 24);
            var g = (y + BitConverter.ToUInt64(dataArray, dataLength - 32)) * mul;
            var h = (z + BitConverter.ToUInt64(dataArray, dataLength - 24)) * mul;

            return ComputeHash16(
                RotateRight(e + f, 43) + RotateRight(g, 30) + h,
                e + RotateRight(f + a, 18) + g, 
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
