using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core;
using OpenSource.Data.HashFunction.Core.Utilities.UnifiedData;
using OpenSource.Data.HashFunction.FarmHash.Utilities;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSource.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Implementation of FarmHash's Fingerprint128 method as specified at https://github.com/google/farmhash.
    /// </summary>
    internal class FarmHashFingerprint128_Implementation
        : HashFunctionAsyncBase,
            IFarmHashFingerprint128
    {
        private const UInt64 k0 = 0xc3a5c85c97cb3127UL;
        private const UInt64 k1 = 0xb492b66fbe98f273UL;
        private const UInt64 k2 = 0x9ae16a3b2f90404fUL;


        public override int HashSizeInBits { get; } = 128;

        


        protected override byte[] ComputeHashInternal(IUnifiedData data, CancellationToken cancellationToken)
        {
            var dataArray = data.ToArray(cancellationToken);

            return ComputeHashFromArray(dataArray)
                .GetBytes();
        }

        protected override async Task<byte[]> ComputeHashAsyncInternal(IUnifiedDataAsync data, CancellationToken cancellationToken)
        {
            var dataArray = await data.ToArrayAsync(cancellationToken)
                .ConfigureAwait(false);

            return ComputeHashFromArray(dataArray)
                .GetBytes();
        }


        private static UInt128 ComputeHashFromArray(byte[] dataArray)
        {
            var dataLength = dataArray.Length;

            if (dataLength >= 16)
                return CityHash128WithSeed(dataArray, 16, dataLength - 16, new UInt128(BitConverter.ToUInt64(dataArray, 0), BitConverter.ToUInt64(dataArray, 8) + k0));

            return CityHash128WithSeed(dataArray, 0, dataLength, new UInt128(k0, k1));
        }
        

        private static UInt128 CityHash128WithSeed(byte[] dataArray, int start, int dataLength, UInt128 seed) 
        {
            if (dataLength < 128) 
                return CityMurmur(dataArray, start, dataLength, seed);


            var v = new UInt128();
            var w = new UInt128();

            var x = seed.Low;
            var y = seed.High;
            var z = (UInt64) dataLength * k1;

            v.Low = RotateRight(y ^ k1, 49) * k1 + BitConverter.ToUInt64(dataArray, start);
            v.High = RotateRight(v.Low, 42) * k1 + BitConverter.ToUInt64(dataArray, start + 8);
            w.Low = RotateRight(y + z, 35) * k1 + x;
            w.High = RotateRight(x + BitConverter.ToUInt64(dataArray, start + 88), 53) * k1;

            do
            {
                x = RotateRight(x + y + v.Low + BitConverter.ToUInt64(dataArray, start + 8), 37) * k1;
                y = RotateRight(y + v.High + BitConverter.ToUInt64(dataArray, start + 48), 42) * k1;
                x ^= w.High;
                y += v.Low + BitConverter.ToUInt64(dataArray, start + 40);
                z = RotateRight(z + w.Low, 33) * k1;
                v = WeakHashLen32WithSeeds(dataArray, start, v.High * k1, x + w.Low);
                w = WeakHashLen32WithSeeds(dataArray, start + 32, z + w.High, y + BitConverter.ToUInt64(dataArray, start + 16));
                Swap(ref z, ref x);

                start += 64;
                x = RotateRight(x + y + v.Low + BitConverter.ToUInt64(dataArray, start + 8), 37) * k1;
                y = RotateRight(y + v.High + BitConverter.ToUInt64(dataArray, start + 48), 42) * k1;
                x ^= w.High;
                y += v.Low + BitConverter.ToUInt64(dataArray, start + 40);
                z = RotateRight(z + w.Low, 33) * k1;
                v = WeakHashLen32WithSeeds(dataArray, start, v.High * k1, x + w.Low);
                w = WeakHashLen32WithSeeds(dataArray, start + 32, z + w.High, y + BitConverter.ToUInt64(dataArray, start + 16));
                Swap(ref z, ref x);

                start += 64;
                dataLength -= 128;
            } while (dataLength >= 128);

            x += RotateRight(v.Low + z, 49) * k0;
            y = y * k0 + RotateRight(w.High, 37);
            z = z * k0 + RotateRight(w.Low, 27);

            w.Low *= 9;
            v.Low *= k0;
            
            for (var i = 32; i < dataLength + 32; i += 32)
            {
                y = RotateRight(x + y, 42) * k0 + v.High;
                w.Low += BitConverter.ToUInt64(dataArray, start + dataLength - i + 16);
                x = x * k0 + w.Low;
                z += w.High + BitConverter.ToUInt64(dataArray, start + dataLength - i);
                w.High += v.Low;
                v = WeakHashLen32WithSeeds(dataArray, start + dataLength - i, v.Low + z, v.High);
                v.Low *= k0;
            }

            x = ComputeHash16(x, v.Low);
            y = ComputeHash16(y + z, w.Low);

            return new UInt128(
                ComputeHash16(x + v.High, w.High) + y,
                ComputeHash16(x + w.High, y + v.High));
        }

        private static UInt128 CityMurmur(byte[] dataArray, int start, int dataLength, UInt128 seed)
        {
            var a = seed.Low;
            var b = seed.High;
            var c = 0UL;
            var d = 0UL;


            if (dataLength <= 16)
            {
                a = ShiftMix(a * k1) * k1;
                c = b * k1 + ComputeHash0To16(dataArray, start, dataLength);
                d = ShiftMix(a + (dataLength >= 8 ? BitConverter.ToUInt64(dataArray, start) : c));

            } else {  // dataLength > 16
                c = ComputeHash16(BitConverter.ToUInt64(dataArray, start + dataLength - 8) + k1, a);
                d = ComputeHash16(b + (UInt64) dataLength, c + BitConverter.ToUInt64(dataArray, start + dataLength - 16));
                a += d;

                for (int x = 0; x < dataLength - 16; x += 16)
                {
                    a ^= ShiftMix(BitConverter.ToUInt64(dataArray, start + x) * k1) * k1;
                    a *= k1;
                    b ^= a;
                    c ^= ShiftMix(BitConverter.ToUInt64(dataArray, start + x + 8) * k1) * k1;
                    c *= k1;
                    d ^= c;
                }
            }

            a = ComputeHash16(a, c);
            b = ComputeHash16(d, b);

            return new UInt128(a ^ b, ComputeHash16(b, a));
        }


        private static UInt64 ComputeHash16(UInt64 u, UInt64 v) =>
            ComputeHash128To64(new UInt128(u, v));

        private static UInt64 ComputeHash16(UInt64 u, UInt64 v, UInt64 mul)
        {
            var a = (u ^ v) * mul;
            a ^= (a >> 47);

            var b = (v ^ a) * mul;
            b ^= (b >> 47);
            b *= mul;

            return b;
        }

        private static UInt64 ComputeHash0To16(byte[] dataArray, int start, int dataLength)
        {
            if (dataLength >= 8)
            {
                var mul = k2 + (UInt64)(dataLength * 2);
                var a = BitConverter.ToUInt64(dataArray, start) + k2;
                var b = BitConverter.ToUInt64(dataArray, start + dataLength - 8);
                var c = (RotateRight(b, 37) * mul) + a;
                var d = (RotateRight(a, 25) + b) * mul;

                return ComputeHash16(c, d, mul);
            }

            if (dataLength >= 4)
            {
                var mul = k2 + ((UInt64)dataLength * 2);
                UInt64 a = BitConverter.ToUInt32(dataArray, start);

                return ComputeHash16((UInt64)dataLength + (a << 3), BitConverter.ToUInt32(dataArray, start + dataLength - 4), mul);
            }

            if (dataLength > 0)
            {
                var a = dataArray[start];
                var b = dataArray[start + (dataLength >> 1)];
                var c = dataArray[start + dataLength - 1];

                var y = a + (((UInt32) b) << 8);
                var z = (UInt32) dataLength + (((UInt32) c) << 2);

                return ShiftMix(y * k2 ^ z * k0) * k2;
            }

            return k2;
        }

        private static UInt64 ComputeHash128To64(UInt128 x)
        {
            var kMul = 0x9ddfea08eb382d69UL;

            var a = (x.Low ^ x.High) * kMul;
            a ^= (a >> 47);

            var b = (x.High ^ a) * kMul;
            b ^= (b >> 47);
            b *= kMul;

            return b;
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
