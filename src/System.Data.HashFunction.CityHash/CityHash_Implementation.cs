using System;
using System.Collections.Generic;
using System.Data.HashFunction.CityHash.Utilities;
using System.Data.HashFunction.Core;
using System.Data.HashFunction.Core.Utilities.UnifiedData;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.HashFunction.CityHash
{
    internal class CityHash_Implementation
        : HashFunctionAsyncBase,
            ICityHash
    {

        /// <summary>
        /// Configuration used when creating this instance.
        /// </summary>
        /// <value>
        /// A clone of configuration that was used when creating this instance.
        /// </value>
        public ICityHashConfig Config => _config.Clone();


        public override int HashSizeInBits => _config.HashSizeInBits;



        /// <summary>
        /// Constant k0 as defined by CityHash specification.
        /// </summary>
        private const UInt64 k0 = 0xc3a5c85c97cb3127;

        /// <summary>
        /// Constant k1 as defined by CityHash specification.
        /// </summary>
        private const UInt64 k1 = 0xb492b66fbe98f273;

        /// <summary>
        /// Constant k2 as defined by CityHash specification.
        /// </summary>
        private const UInt64 k2 = 0x9ae16a3b2f90404f;


        /// <summary>
        /// Constant c1 as defined by CityHash specification.
        /// </summary>
        private const UInt32 c1 = 0xcc9e2d51;

        /// <summary>
        /// Constant c2 as defined by CityHash specification.
        /// </summary>
        private const UInt32 c2 = 0x1b873593;


        private readonly ICityHashConfig _config;


        private static readonly IEnumerable<int> _validHashSizes = new HashSet<int>() { 32, 64, 128 };


        /// <summary>
        /// Initializes a new instance of the <see cref="CityHash_Implementation"/> class.
        /// </summary>
        /// <param name="config">Configuration for this instance</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="config"/></exception>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="config"/>.<see cref="ICityHashConfig.HashSizeInBits">HashSizeInBits</see>;<paramref name="config"/>.<see cref="ICityHashConfig.HashSizeInBits">HashSizeInBits</see> must be contained within CityHash.ValidHashSizes.</exception>
        public CityHash_Implementation(ICityHashConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Clone();


            if (!_validHashSizes.Contains(_config.HashSizeInBits))
                throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.HashSizeInBits)}", _config.HashSizeInBits, $"{nameof(config)}.{nameof(config.HashSizeInBits)} must be contained within CityHash.ValidHashSizes.");

        }


        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(IUnifiedData data, CancellationToken cancellationToken)
        {
            byte[] hash = null;
            var dataArray = data.ToArray(cancellationToken);
            
            switch (_config.HashSizeInBits)
            {
                case 32:
                    hash = BitConverter.GetBytes(
                        ComputeHash32(dataArray));

                    break;

                case 64:
                    hash = BitConverter.GetBytes(
                        ComputeHash64(dataArray));

                    break;

                case 128:
                    var result = ComputeHash128(dataArray);


                    hash = new byte[16];

                    BitConverter.GetBytes(result.Low)
                        .CopyTo(hash, 0);

                    BitConverter.GetBytes(result.High)
                        .CopyTo(hash, 8);

                    break;

                default:
                    throw new NotImplementedException();
            }

            return hash;
        }

        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override async Task<byte[]> ComputeHashAsyncInternal(IUnifiedDataAsync data, CancellationToken cancellationToken)
        {
            byte[] hash = null;
            var dataArray = await data.ToArrayAsync(cancellationToken)
                .ConfigureAwait(false);

            switch (_config.HashSizeInBits)
            {
                case 32:
                    hash = BitConverter.GetBytes(
                        ComputeHash32(dataArray));

                    break;

                case 64:
                    hash = BitConverter.GetBytes(
                        ComputeHash64(dataArray));

                    break;

                case 128:
                    var result = ComputeHash128(dataArray);


                    hash = new byte[16];
                    
                    BitConverter.GetBytes(result.Low)
                        .CopyTo(hash, 0);

                    BitConverter.GetBytes(result.High)
                        .CopyTo(hash, 8);

                    break;

                default:
                    throw new NotImplementedException();
            }

            return hash;
        }


        #region ComputeHash32

        /// <summary>32-bit implementation of ComputeHash.</summary>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>UInt32 value representing the hash value.</returns>
        protected virtual UInt32 ComputeHash32(byte[] data)
        {
            if (data.Length <= 24)
            {
                if (data.Length <= 12)
                    return (data.Length <= 4 ? Hash32Len0to4(data) : Hash32Len5to12(data));
                else
                    return Hash32Len13to24(data);
            }

            // data.Length > 24
            UInt32 h = (UInt32) data.Length;
            UInt32 g = (UInt32) data.Length * c1;
            UInt32 f = g;

            {
                UInt32 a0 = RotateRight(BitConverter.ToUInt32(data, data.Length - 4) * c1, 17) * c2;
                UInt32 a1 = RotateRight(BitConverter.ToUInt32(data, data.Length - 8) * c1, 17) * c2;
                UInt32 a2 = RotateRight(BitConverter.ToUInt32(data, data.Length - 16) * c1, 17) * c2;
                UInt32 a3 = RotateRight(BitConverter.ToUInt32(data, data.Length - 12) * c1, 17) * c2;
                UInt32 a4 = RotateRight(BitConverter.ToUInt32(data, data.Length - 20) * c1, 17) * c2;

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
                f = RotateRight(f, 19);
                f = f * 5 + 0xe6546b64;
            }

            for (int x = 0; x < (data.Length - 1) / 20; ++x)
            {
                UInt32 a0 = RotateRight(BitConverter.ToUInt32(data, 20 * x + 0) * c1, 17) * c2;
                UInt32 a1 =  BitConverter.ToUInt32(data, 20 * x + 4);
                UInt32 a2 = RotateRight(BitConverter.ToUInt32(data, 20 * x + 8) * c1, 17) * c2;
                UInt32 a3 = RotateRight(BitConverter.ToUInt32(data, 20 * x + 12) * c1, 17) * c2;
                UInt32 a4 =  BitConverter.ToUInt32(data, 20 * x + 16);

                h ^= a0;
                h = RotateRight(h, 18);
                h = h * 5 + 0xe6546b64;

                f += a1;
                f = RotateRight(f, 19);
                f = f * c1;

                g += a2;
                g = RotateRight(g, 18);
                g = g * 5 + 0xe6546b64;

                h ^= a3 + a1;
                h = RotateRight(h, 19);
                h = h * 5 + 0xe6546b64;

                g ^= a4;
                g = ReverseByteOrder(g) * 5;

                h += a4 * 5;
                h = ReverseByteOrder(h);

                f += a0;

                Permute3(ref f, ref h, ref g);
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


        private UInt32 Hash32Len0to4(byte[] data) 
        {
            UInt32 b = 0;
            UInt32 c = 9;

            foreach (var v in data)
            {
                b = b * c1 + v;
                c ^= b;
            }

            return Mix(Mur(b, Mur((UInt32) data.Length, c)));
        }

        private UInt32 Hash32Len5to12(byte[] data) 
        {
            UInt32 a = (UInt32) data.Length;
            UInt32 b = (UInt32) data.Length * 5;

            UInt32 c = 9;
            UInt32 d = b;

            a += BitConverter.ToUInt32(data, 0);
            b += BitConverter.ToUInt32(data, data.Length - 4);
            c += BitConverter.ToUInt32(data, (data.Length >> 1) & 4);

            return Mix(Mur(c, Mur(b, Mur(a, d))));
        }

        private UInt32 Hash32Len13to24(byte[] data) 
        {
            UInt32 a = BitConverter.ToUInt32(data, (data.Length >> 1) - 4);
            UInt32 b = BitConverter.ToUInt32(data, 4);
            UInt32 c = BitConverter.ToUInt32(data, data.Length - 8);
            UInt32 d = BitConverter.ToUInt32(data, data.Length >> 1);
            UInt32 e = BitConverter.ToUInt32(data, 0);
            UInt32 f = BitConverter.ToUInt32(data, data.Length - 4);
            UInt32 h = (UInt32) data.Length;
            
            return Mix(Mur(f, Mur(e, Mur(d, Mur(c, Mur(b, Mur(a, h)))))));
        }

        #endregion

        #region ComputeHash64

        /// <summary>64-bit implementation of ComputeHash.</summary>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>UInt64 value representing the hash value.</returns>
        protected virtual UInt64 ComputeHash64(byte[] data) 
        {
            if (data.Length <= 32) 
            {
                if (data.Length <= 16) {
                    return HashLen0to16(data);
                } else {
                    return HashLen17to32(data);
                }
            } else if (data.Length <= 64) {
                return HashLen33to64(data);
            }

            // For strings over 64 bytes we hash the end first, and then as we
            // loop we keep 56 bytes of state: v, w, x, y, and z.
            UInt64 x = BitConverter.ToUInt64(data, data.Length - 40);
            UInt64 y = BitConverter.ToUInt64(data,  data.Length - 16) + BitConverter.ToUInt64(data,  data.Length - 56);
            UInt64 z = HashLen16(
                BitConverter.ToUInt64(data,  data.Length - 48) + (UInt64) data.Length, 
                BitConverter.ToUInt64(data,  data.Length - 24));
            
            UInt128 v = WeakHashLen32WithSeeds(data,  data.Length - 64, (UInt64) data.Length, z);
            UInt128 w = WeakHashLen32WithSeeds(data,  data.Length - 32, y + k1, x);

            x = x * k1 + BitConverter.ToUInt64(data, 0);
            
            for (int i = 0; i < (data.Length >> 6); ++i)
            {
                x = RotateRight(x + y + v.Low + BitConverter.ToUInt64(data, 64 * i + 8), 37) * k1;
                y = RotateRight(y + v.High + BitConverter.ToUInt64(data, 64 * i + 48), 42) * k1;
                x ^= w.High;
                y += v.Low + BitConverter.ToUInt64(data, 64 * i + 40);
                z = RotateRight(z + w.Low, 33) * k1;
                v = WeakHashLen32WithSeeds(data, 64 * i, v.High * k1, x + w.Low);
                w = WeakHashLen32WithSeeds(data, 64 * i + 32, z + w.High, y + BitConverter.ToUInt64(data, 64 * i + 16));
                
                UInt64 temp = x;
                x = z;
                z = temp;
            }

            return HashLen16(HashLen16(v.Low, w.Low) + Mix(y) * k1 + z,
                            HashLen16(v.High, w.High) + x);
        }


        private UInt64 HashLen16(UInt64 u, UInt64 v) {
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

        private UInt64 HashLen0to16(byte[] data) 
        {
            if (data.Length >= 8) 
            {
                UInt64 mul = k2 + (UInt64) data.Length * 2;
                UInt64 a = BitConverter.ToUInt64(data, 0) + k2;
                UInt64 b = BitConverter.ToUInt64(data, data.Length - 8);
                UInt64 c = RotateRight(b, 37) * mul + a;
                UInt64 d = (RotateRight(a, 25) + b) * mul;

                return HashLen16(c, d, mul);
            }

            if (data.Length >= 4) 
            {
                UInt64 mul = k2 + (UInt64) data.Length * 2;
                UInt64 a = BitConverter.ToUInt32(data, 0);
                return HashLen16((UInt64) data.Length + (a << 3), BitConverter.ToUInt32(data, data.Length - 4), mul);
            }

            if (data.Length > 0) 
            {
                byte a = data[0];
                byte b = data[data.Length >> 1];
                byte c = data[data.Length - 1];

                UInt32 y = (UInt32) a + ((UInt32) b << 8);
                UInt32 z = (UInt32) data.Length + ((UInt32) c << 2);

                return Mix((UInt64) (y * k2 ^ z * k0)) * k2;
            }

            return k2;
        }

        // This probably works well for 16-byte strings as well, but it may be overkill
        // in that case.
        private UInt64 HashLen17to32(byte[] data) 
        {
            UInt64 mul = k2 + (UInt64) data.Length * 2;
            UInt64 a = BitConverter.ToUInt64(data, 0) * k1;
            UInt64 b = BitConverter.ToUInt64(data, 8);
            UInt64 c = BitConverter.ToUInt64(data, data.Length - 8) * mul;
            UInt64 d = BitConverter.ToUInt64(data, data.Length - 16) * k2;

            return HashLen16(
                RotateRight(a + b, 43) +
                    RotateRight(c, 30) + d,
                a + RotateRight(b + k2, 18) + c, 
                mul);
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

        // Return an 8-byte hash for 33 to 64 bytes.
        private UInt64 HashLen33to64(byte[] data) 
        {
            UInt64 mul = k2 + (UInt64) data.Length * 2;
            UInt64 a = BitConverter.ToUInt64(data, 0) * k2;
            UInt64 b = BitConverter.ToUInt64(data, 8);
            UInt64 c = BitConverter.ToUInt64(data, data.Length - 24);
            UInt64 d = BitConverter.ToUInt64(data, data.Length - 32);
            UInt64 e = BitConverter.ToUInt64(data, 16) * k2;
            UInt64 f = BitConverter.ToUInt64(data, 24) * 9;
            UInt64 g = BitConverter.ToUInt64(data, data.Length - 8);
            UInt64 h = BitConverter.ToUInt64(data, data.Length - 16) * mul;

            UInt64 u = RotateRight(a + g,43) + (RotateRight(b, 30) + c) * 9;
            UInt64 v = ((a + g) ^ d) + f + 1;
            UInt64 w = ReverseByteOrder((u + v) * mul) + h;
            UInt64 x = RotateRight(e + f, 42) + c;
            UInt64 y = (ReverseByteOrder((v + w) * mul) + g) * mul;
            UInt64 z = e + f + c;

            a = ReverseByteOrder((x + z) * mul + y) + b;
            b = Mix((z + a) * mul + d + h) * mul;
            return b + x;
        }

        #endregion

        #region ComputeHash128

        /// <summary>128-bit implementation of ComputeHash.</summary>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>UInt128 value representing the hash value.</returns>
        private UInt128 ComputeHash128(byte[] data) {
            return 
                data.Length >= 16 ?
                CityHash128WithSeed(
                    data.Skip(16).ToArray(), 
                    new UInt128(
                        BitConverter.ToUInt64(data, 0), 
                        BitConverter.ToUInt64(data, 8) + k0)) :
                CityHash128WithSeed(
                    data,
                    new UInt128(k0, k1));
        }


        // A subroutine for CityHash128().  Returns a decent 128-bit hash for strings
        // of any length representable in signed long.  Based on City and Murmur.
        private UInt128 CityMurmur(byte[] data, UInt128 seed) {
            UInt64 a = seed.Low;
            UInt64 b = seed.High;
            UInt64 c = 0;
            UInt64 d = 0;

            int l = data.Length - 16;
            if (l <= 0) {  // len <= 16
                a = Mix(a * k1) * k1;
                c = b * k1 + HashLen0to16(data);
                d = Mix(a + (data.Length >= 8 ? BitConverter.ToUInt64(data, 0) : c));
            } else {  // len > 16
                c = HashLen16(BitConverter.ToUInt64(data, data.Length - 8) + k1, a);
                d = HashLen16(b + (UInt64) data.Length, c + BitConverter.ToUInt64(data, data.Length - 16));
                a += d;

                for (int i = 0; i < (data.Length - 1) / 16; ++i)
                {
                    a ^= Mix(BitConverter.ToUInt64(data, i * 16) * k1) * k1;
                    a *= k1;
                    b ^= a;
                    c ^= Mix(BitConverter.ToUInt64(data, i * 16 + 8) * k1) * k1;
                    c *= k1;
                    d ^= c;
                }

            }
            a = HashLen16(a, c);
            b = HashLen16(d, b);
            return new UInt128(a ^ b, HashLen16(b, a));
        }
        
        private UInt128 CityHash128WithSeed(byte[] data, UInt128 seed)
        {
            if (data.Length < 128) {
                return CityMurmur(data, seed);
            }

            // We expect len >= 128 to be the common case.  Keep 56 bytes of state:
            // v, w, x, y, and z.
            UInt128 v;
            {
                var vLow = RotateRight(seed.High ^ k1, 49) * k1 + BitConverter.ToUInt64(data, 0);
                v = new UInt128(
                    vLow,
                    RotateRight(vLow, 42) * k1 + BitConverter.ToUInt64(data, 8));
            }


            UInt128 w = new UInt128(
                RotateRight(seed.High + ((UInt64) data.Length * k1), 35) * k1 + seed.Low,
                RotateRight(seed.Low + BitConverter.ToUInt64(data, 88), 53) * k1);

            UInt64 x = seed.Low;
            UInt64 y = seed.High;
            UInt64 z = (UInt64) data.Length * k1;



            // This is the same inner loop as CityHash64(), manually unrolled.
            for (int i = 0; i < data.Length / 128; ++i)
            {
                x = RotateRight(x + y + v.Low + BitConverter.ToUInt64(data, (128 * i) + 8), 37) * k1;
                y = RotateRight(y + v.High + BitConverter.ToUInt64(data, (128 * i) + 48), 42) * k1;
                x ^= w.High;
                y += v.Low + BitConverter.ToUInt64(data, (128 * i) + 40);
                z = RotateRight(z + w.Low, 33) * k1;
                v = WeakHashLen32WithSeeds(data, 128 * i, v.High * k1, x + w.Low);
                w = WeakHashLen32WithSeeds(data, (128 * i) + 32, z + w.High, y + BitConverter.ToUInt64(data, (128 * i) + 16));

                {
                    UInt64 temp = z;
                    z = x;
                    x = temp;
                }

                x = RotateRight(x + y + v.Low + BitConverter.ToUInt64(data, (128 * i) + 72), 37) * k1;
                y = RotateRight(y + v.High + BitConverter.ToUInt64(data, (128 * i) + 112), 42) * k1;
                x ^= w.High;
                y += v.Low + BitConverter.ToUInt64(data, (128 * i) + 104);
                z = RotateRight(z + w.Low, 33) * k1;
                v = WeakHashLen32WithSeeds(data, (128 * i) + 64, v.High * k1, x + w.Low);
                w = WeakHashLen32WithSeeds(data, (128 * i) + 96, z + w.High, y + BitConverter.ToUInt64(data, (128 * i) + 80));

                {
                    UInt64 temp = z;
                    z = x;
                    x = temp;
                }


            }


            x += RotateRight(v.Low + z, 49) * k0;
            y = y * k0 + RotateRight(w.High, 37);
            z = z * k0 + RotateRight(w.Low, 27);
            w.Low *= 9;
            v.Low *= k0;


            
            // If 0 < len < 128, hash up to 4 chunks of 32 bytes each from the end of s.
            for (int i = 1; i <= (((data.Length % 128) + 31) / 32); ++i) 
            {
                y = RotateRight(x + y, 42) * k0 + v.High;
                w.Low += BitConverter.ToUInt64(data, data.Length - (32 * i) + 16);
                x = x * k0 + w.Low;
                z += w.High + BitConverter.ToUInt64(data, data.Length - (32 * i));
                w.High += v.Low;
                v = WeakHashLen32WithSeeds(data, data.Length - (32 * i), v.Low + z, v.High);
                v.Low *= k0;
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

        #endregion

        #region Shared Utilities

        private static UInt32 Mix(UInt32 h)
        {
            h ^= h >> 16;
            h *= 0x85ebca6b;
            h ^= h >> 13;
            h *= 0xc2b2ae35;
            h ^= h >> 16;
            return h;
        }

        private static UInt64 Mix(UInt64 val)
        {
            return val ^ (val >> 47);
        }


        private UInt32 Mur(UInt32 a, UInt32 h)
        {
            // Helper from Murmur3 for combining two 32-bit values.
            a *= c1;
            a = RotateRight(a, 17);
            a *= c2;
            h ^= a;
            h = RotateRight(h, 19);
            return h * 5 + 0xe6546b64;
        }

        private static void Permute3(ref UInt32 a, ref UInt32 b, ref UInt32 c)
        {
            UInt32 temp = a;

            a = c;
            c = b;
            b = temp;
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


        private static UInt32 RotateRight(UInt32 operand, int shiftCount)
        {
            shiftCount &= 0x1f;

            return
                (operand >> shiftCount) |
                (operand << (32 - shiftCount));
        }

        private static UInt64 RotateRight(UInt64 operand, int shiftCount)
        {
            shiftCount &= 0x3f;

            return
                (operand >> shiftCount) |
                (operand << (64 - shiftCount));
        }


        private static UInt32 ReverseByteOrder(UInt32 operand)
        {
            return
                (operand >> 24) |
                ((operand & 0x00ff0000) >> 8) |
                ((operand & 0x0000ff00) << 8) |
                (operand << 24);
        }

        private static UInt64 ReverseByteOrder(UInt64 operand)
        {
            return
                (operand >> 56) |
                ((operand & 0x00ff000000000000) >> 40) |
                ((operand & 0x0000ff0000000000) >> 24) |
                ((operand & 0x000000ff00000000) >> 8) |
                ((operand & 0x00000000ff000000) << 8) |
                ((operand & 0x0000000000ff0000) << 24) |
                ((operand & 0x000000000000ff00) << 40) |
                (operand << 56);
        }


        #endregion

    }
}
