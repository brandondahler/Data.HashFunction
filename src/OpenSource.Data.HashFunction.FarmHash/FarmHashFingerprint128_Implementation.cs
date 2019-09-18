using System;
using OpenSource.Data.HashFunction.Core;
using OpenSource.Data.HashFunction.FarmHash.Utilities;
using System.Threading;
using System.Threading.Tasks;
using OpenSource.Data.HashFunction.Core.Utilities;
using System.Linq;

namespace OpenSource.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Implementation of FarmHash's Fingerprint128 method as specified at https://github.com/google/farmhash.
    /// </summary>
    internal class FarmHashFingerprint128_Implementation
        : HashFunctionBase,
            IFarmHashFingerprint128
    {
        private const UInt64 k0 = 0xc3a5c85c97cb3127UL;
        private const UInt64 k1 = 0xb492b66fbe98f273UL;
        private const UInt64 k2 = 0x9ae16a3b2f90404fUL;


        public override int HashSizeInBits { get; } = 128;



        protected override IHashValue ComputeHashInternal(ArraySegment<byte> data, CancellationToken cancellationToken)
        {
            var dataCount = data.Count;

            UInt128 hashValue;

            if (dataCount >= 16)
            {
                var dataArray = data.Array;
                var dataOffset = data.Offset;

                hashValue = CityHash128WithSeed(
                    new ArraySegment<byte>(dataArray, dataOffset + 16, dataCount - 16),
                    new UInt128(
                        BitConverter.ToUInt64(dataArray, dataOffset),
                        BitConverter.ToUInt64(dataArray, dataOffset + 8) + k0),
                    cancellationToken);

            } else {
                hashValue = CityHash128WithSeed(data, new UInt128(k0, k1), cancellationToken);
            }


            var hashValueBytes = BitConverter.GetBytes(hashValue.Low)
                .Concat(BitConverter.GetBytes(hashValue.High));

            return new HashValue(hashValueBytes, 128);
        }


        private UInt128 CityHash128WithSeed(ArraySegment<byte> data, UInt128 seed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var dataCount = data.Count;

            if (dataCount < 128)
                return CityMurmur(data, seed);


            var dataArray = data.Array;
            var dataOffset = data.Offset;

            var endOffset = dataOffset + dataCount;

            // We expect len >= 128 to be the common case.  Keep 56 bytes of state:
            // v, w, x, y, and z.
            UInt128 v;
            {
                var vLow = RotateRight(seed.High ^ k1, 49) * k1 + BitConverter.ToUInt64(dataArray, dataOffset);
                v = new UInt128(
                    vLow,
                    RotateRight(vLow, 42) * k1 + BitConverter.ToUInt64(dataArray, dataOffset + 8));
            }


            UInt128 w = new UInt128(
                RotateRight(seed.High + ((UInt64)dataCount * k1), 35) * k1 + seed.Low,
                RotateRight(seed.Low + BitConverter.ToUInt64(dataArray, dataOffset + 88), 53) * k1);

            UInt64 x = seed.Low;
            UInt64 y = seed.High;
            UInt64 z = (UInt64)dataCount * k1;


            // This is the same inner loop as CityHash64()
            int lastGroupEndOffset;
            {
                var groupEndOffset = dataOffset + (dataCount - (dataCount % 128));

                for (var groupCurrentOffset = dataOffset; groupCurrentOffset < groupEndOffset; groupCurrentOffset += 128)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    x = RotateRight(x + y + v.Low + BitConverter.ToUInt64(dataArray, groupCurrentOffset + 8), 37) * k1;
                    y = RotateRight(y + v.High + BitConverter.ToUInt64(dataArray, groupCurrentOffset + 48), 42) * k1;
                    x ^= w.High;
                    y += v.Low + BitConverter.ToUInt64(dataArray, groupCurrentOffset + 40);
                    z = RotateRight(z + w.Low, 33) * k1;
                    v = WeakHashLen32WithSeeds(dataArray, groupCurrentOffset, v.High * k1, x + w.Low);
                    w = WeakHashLen32WithSeeds(dataArray, groupCurrentOffset + 32, z + w.High, y + BitConverter.ToUInt64(dataArray, groupCurrentOffset + 16));

                    {
                        UInt64 temp = z;
                        z = x;
                        x = temp;
                    }

                    x = RotateRight(x + y + v.Low + BitConverter.ToUInt64(dataArray, groupCurrentOffset + 72), 37) * k1;
                    y = RotateRight(y + v.High + BitConverter.ToUInt64(dataArray, groupCurrentOffset + 112), 42) * k1;
                    x ^= w.High;
                    y += v.Low + BitConverter.ToUInt64(dataArray, groupCurrentOffset + 104);
                    z = RotateRight(z + w.Low, 33) * k1;
                    v = WeakHashLen32WithSeeds(dataArray, groupCurrentOffset + 64, v.High * k1, x + w.Low);
                    w = WeakHashLen32WithSeeds(dataArray, groupCurrentOffset + 96, z + w.High, y + BitConverter.ToUInt64(dataArray, groupCurrentOffset + 80));

                    {
                        UInt64 temp = z;
                        z = x;
                        x = temp;
                    }
                }

                lastGroupEndOffset = groupEndOffset;
            }

            cancellationToken.ThrowIfCancellationRequested();

            x += RotateRight(v.Low + z, 49) * k0;
            y = y * k0 + RotateRight(w.High, 37);
            z = z * k0 + RotateRight(w.Low, 27);
            w = new UInt128(w.Low * 9, w.High);
            v = new UInt128(v.Low * k0, v.High);

            
            // Hash up to 4 chunks of 32 bytes each from the end of data.
            {
                var groupEndOffset = lastGroupEndOffset - 32;

                for (var groupCurrentOffset = endOffset - 32; groupCurrentOffset > groupEndOffset; groupCurrentOffset -= 32) 
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    y = RotateRight(x + y, 42) * k0 + v.High;
                    w = new UInt128(w.Low + BitConverter.ToUInt64(dataArray, groupCurrentOffset + 16), w.High);
                    x = x * k0 + w.Low;
                    z += w.High + BitConverter.ToUInt64(dataArray, groupCurrentOffset);
                    w = new UInt128(w.Low, w.High + v.Low);
                    v = WeakHashLen32WithSeeds(dataArray, groupCurrentOffset, v.Low + z, v.High);
                    v = new UInt128(v.Low * k0, v.High);
                }
            }

            // At this point our 56 bytes of state should contain more than
            // enough information for a strong 128-bit hash.  We use two
            // different 56-byte-to-8-byte hashes to get a 16-byte final result.
            x = HashLen16(x, v.Low);
            y = HashLen16(y + z, w.Low);

            return new UInt128(
                HashLen16(x + v.High, w.High) + y,
                HashLen16(x + w.High, y + v.High));
        }


        // A subroutine for CityHash128().  Returns a decent 128-bit hash for strings
        // of any length representable in signed long.  Based on City and Murmur.
        private UInt128 CityMurmur(ArraySegment<byte> data, UInt128 seed)
        {
            var dataArray = data.Array;
            var dataOffset = data.Offset;
            var dataCount = data.Count;

            var endOffset = dataOffset + dataCount;

            UInt64 a = seed.Low;
            UInt64 b = seed.High;
            UInt64 c;
            UInt64 d;

            if (dataCount <= 16)
            {
                // len <= 16
                a = Mix(a * k1) * k1;
                c = b * k1 + HashLen0to16(data);
                d = Mix(a + (dataCount >= 8 ? BitConverter.ToUInt64(dataArray, dataOffset) : c));

            }
            else
            {
                // len > 16
                c = HashLen16(BitConverter.ToUInt64(dataArray, endOffset - 8) + k1, a);
                d = HashLen16(b + (UInt64)dataCount, c + BitConverter.ToUInt64(dataArray, endOffset - 16));
                a += d;

                var groupEndOffset = dataOffset + dataCount - 16;

                for (var groupCurrentOffset = dataOffset; groupCurrentOffset < groupEndOffset; groupCurrentOffset += 16)
                {
                    a ^= Mix(BitConverter.ToUInt64(dataArray, groupCurrentOffset) * k1) * k1;
                    a *= k1;
                    b ^= a;
                    c ^= Mix(BitConverter.ToUInt64(dataArray, groupCurrentOffset + 8) * k1) * k1;
                    c *= k1;
                    d ^= c;
                }
            }

            a = HashLen16(a, c);
            b = HashLen16(d, b);
            return new UInt128(a ^ b, HashLen16(b, a));
        }


        private UInt64 HashLen16(UInt64 u, UInt64 v)
        {
            return Hash128to64(
                new UInt128(u, v));
        }

        private static UInt64 HashLen16(UInt64 u, UInt64 v, UInt64 mul)
        {
            UInt64 a = (u ^ v) * mul;
            a ^= (a >> 47);

            UInt64 b = (v ^ a) * mul;
            b ^= (b >> 47);
            b *= mul;

            return b;
        }

        private UInt64 HashLen0to16(ArraySegment<byte> data)
        {
            var dataArray = data.Array;
            var dataOffset = data.Offset;
            var dataCount = data.Count;

            var endOffset = dataOffset + dataCount;

            if (dataCount >= 8)
            {
                UInt64 mul = k2 + (UInt64)dataCount * 2;
                UInt64 a = BitConverter.ToUInt64(dataArray, dataOffset) + k2;
                UInt64 b = BitConverter.ToUInt64(dataArray, endOffset - 8);
                UInt64 c = RotateRight(b, 37) * mul + a;
                UInt64 d = (RotateRight(a, 25) + b) * mul;

                return HashLen16(c, d, mul);
            }

            if (dataCount >= 4)
            {
                UInt64 mul = k2 + (UInt64)dataCount * 2;
                UInt64 a = BitConverter.ToUInt32(dataArray, dataOffset);
                return HashLen16((UInt64)dataCount + (a << 3), BitConverter.ToUInt32(dataArray, endOffset - 4), mul);
            }

            if (dataCount > 0)
            {
                byte a = dataArray[dataOffset];
                byte b = dataArray[dataOffset + (dataCount >> 1)];
                byte c = dataArray[endOffset - 1];

                UInt32 y = (UInt32)a + ((UInt32)b << 8);
                UInt32 z = (UInt32)dataCount + ((UInt32)c << 2);

                return Mix((UInt64)(y * k2 ^ z * k0)) * k2;
            }

            return k2;
        }
        
        private static UInt64 Hash128to64(UInt128 x)
        {
            const UInt64 kMul = 0x9ddfea08eb382d69;

            UInt64 a = (x.Low ^ x.High) * kMul;
            a ^= (a >> 47);

            UInt64 b = (x.High ^ a) * kMul;
            b ^= (b >> 47);
            b *= kMul;

            return b;
        }

        // Return a 16-byte hash for 48 bytes.  Quick and dirty.
        // Callers do best to use "random-looking" values for a and b.
        private UInt128 WeakHashLen32WithSeeds(
            UInt64 w, UInt64 x, UInt64 y, UInt64 z, UInt64 a, UInt64 b)
        {
            a += w;
            b = RotateRight(b + a + z, 21);

            UInt64 c = a;
            a += x;
            a += y;

            b += RotateRight(a, 44);

            return new UInt128(a + z, b + c);
        }

        // Return a 16-byte hash for s[0] ... s[31], a, and b.  Quick and dirty.
        private UInt128 WeakHashLen32WithSeeds(byte[] data, int startIndex, UInt64 a, UInt64 b)
        {
            return WeakHashLen32WithSeeds(
                BitConverter.ToUInt64(data, startIndex),
                BitConverter.ToUInt64(data, startIndex + 8),
                BitConverter.ToUInt64(data, startIndex + 16),
                BitConverter.ToUInt64(data, startIndex + 24),
                a,
                b);
        }

        #region Utilities

        private static UInt64 RotateRight(UInt64 operand, int shiftCount)
        {
            shiftCount &= 0x3f;

            return
                (operand >> shiftCount) |
                (operand << (64 - shiftCount));
        }

        private static UInt64 Mix(UInt64 value) =>
            value ^ (value >> 47);

        #endregion
    }
}
