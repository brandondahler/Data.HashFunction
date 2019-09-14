using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Core;
using OpenSource.Data.HashFunction.Core.Utilities.UnifiedData;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenSource.Data.HashFunction.FarmHash
{
    /// <summary>
    /// Implementation of FarmHash's Fingerprint32 method as specified at https://github.com/google/farmhash.
    /// </summary>
    internal class FarmHashFingerprint32_Implementation
        : HashFunctionAsyncBase,
            IFarmHashFingerprint32
    {
        private const UInt32 c1 = 0xcc9e2d51;
        private const UInt32 c2 = 0x1b873593;


        public override int HashSizeInBits { get; } = 32;

        


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


        private static UInt32 ComputeHashFromArray(byte[] dataArray)
        {
            var dataLength = dataArray.Length;

            if (dataLength <= 24)
            {
                if (dataLength >= 13)
                    return ComputeHash13To24(dataArray);

                if (dataLength >= 5)
                    return ComputeHash5To12(dataArray);

                return ComputeHash0To4(dataArray);
            }



            // dataLength > 24
            var h = (UInt32) dataLength;
            var g = (UInt32) (c1 * dataLength);
            var f = g;

            var a0 = RotateRight(BitConverter.ToUInt32(dataArray, dataLength - 4) * c1, 17) * c2;
            var a1 = RotateRight(BitConverter.ToUInt32(dataArray, dataLength - 8) * c1, 17) * c2;
            var a2 = RotateRight(BitConverter.ToUInt32(dataArray, dataLength - 16) * c1, 17) * c2;
            var a3 = RotateRight(BitConverter.ToUInt32(dataArray, dataLength - 12) * c1, 17) * c2;
            var a4 = RotateRight(BitConverter.ToUInt32(dataArray, dataLength - 20) * c1, 17) * c2;

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

            for (int x = 0; x < dataLength - 20; x += 20)
            {

                var a = BitConverter.ToUInt32(dataArray, x);
                var b = BitConverter.ToUInt32(dataArray, x + 4);
                var c = BitConverter.ToUInt32(dataArray, x + 8);
                var d = BitConverter.ToUInt32(dataArray, x + 12);
                var e = BitConverter.ToUInt32(dataArray, x + 16);

                h += a;
                g += b;
                f += c;
                h = Mur(d, h) + e;
                g = Mur(c, g) + a;
                f = Mur(b + e * c1, f) + d;
                f += g;
                g += f;
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


        private static UInt32 ComputeHash13To24(byte[] dataArray)
        {
            var dataLength = dataArray.Length;

            var a = BitConverter.ToUInt32(dataArray, (dataLength >> 1) - 4);
            var b = BitConverter.ToUInt32(dataArray, 4);
            var c = BitConverter.ToUInt32(dataArray, dataLength - 8);
            var d = BitConverter.ToUInt32(dataArray, (dataLength >> 1));
            var e = BitConverter.ToUInt32(dataArray, 0);
            var f = BitConverter.ToUInt32(dataArray, dataLength - 4);
            var h = (d * c1) + (UInt32) dataLength;


            a = RotateRight(a, 12) + f;
            h = Mur(c, h) + a;
            a = RotateRight(a, 3) + c;
            h = Mur(e, h) + a;
            a = RotateRight(a + f, 12) + d;
            h = Mur(b, h) + a;

            return FMix(h);
        }

        private static UInt32 ComputeHash0To4(byte[] dataArray) 
        {
            var dataLength = dataArray.Length;

            var b = 0U;
            var c = 9U;

            for (var x = 0; x < dataLength; ++x)
            {
                var v = (sbyte) dataArray[x];

                b = (UInt32) ((b * c1) + v);
                c ^= b;
            }

            return FMix(Mur(b, Mur((UInt32) dataLength, c)));
        }

        private static UInt32 ComputeHash5To12(byte[] dataArray) 
        {
            var dataLength = dataArray.Length;

            var a = (UInt32) dataLength;
            var b = (UInt32) (dataLength * 5);
            var c = 9U;
            var d = b;

            a += BitConverter.ToUInt32(dataArray, 0);
            b += BitConverter.ToUInt32(dataArray, dataLength - 4);
            c += BitConverter.ToUInt32(dataArray, (dataLength >> 1) & 4);

            return FMix(Mur(c, Mur(b, Mur(a, d))));
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
