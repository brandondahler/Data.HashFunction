using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Data.HashFunction.Utilities.IntegerManipulation;
using System.Data.HashFunction.Utilities.UnifiedData;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Implementation of CityHash as specified at https://code.google.com/p/cityhash/.
    /// 
    /// "
    /// CityHash provides hash functions for strings. The functions mix the 
    ///   input bits thoroughly but are not suitable for cryptography. 
    ///  
    /// [Hash size of 128-bits is] tuned for strings of at least a few hundred bytes. 
    /// Depending on your compiler and hardware, it's likely faster than [the hash size of 64-bits] on 
    ///   sufficiently long strings. 
    /// It's slower than necessary on shorter strings, but we expect that case to be relatively unimportant.
    /// "
    /// </summary>
    public class CityHash 
#if NET45
        : HashFunctionAsyncBase
#else
        : HashFunctionBase
#endif
    {
        /// <summary>
        /// The list of possible hash sizes that can be provided to the <see cref="CityHash" /> constructor.
        /// </summary>
        /// <value>
        /// The list of valid hash sizes.
        /// </value>
        public static IEnumerable<int> ValidHashSizes { get { return _ValidHashSizes; } }


        /// <inheritdoc />
        protected override bool RequiresSeekableStream { get { return true; } }

        /// <summary>
        /// Constant k0 as defined by CityHash specification.
        /// </summary>
        protected const UInt64 k0 = 0xc3a5c85c97cb3127;

        /// <summary>
        /// Constant k1 as defined by CityHash specification.
        /// </summary>
        protected const UInt64 k1 = 0xb492b66fbe98f273;

        /// <summary>
        /// Constant k2 as defined by CityHash specification.
        /// </summary>
        protected const UInt64 k2 = 0x9ae16a3b2f90404f;


        /// <summary>
        /// Constant c1 as defined by CityHash specification.
        /// </summary>
        protected const UInt32 c1 = 0xcc9e2d51;

        /// <summary>
        /// Constant c2 as defined by CityHash specification.
        /// </summary>
        protected const UInt32 c2 = 0x1b873593;


        private static readonly IEnumerable<int> _ValidHashSizes = new[] { 32, 64, 128 };



        /// <remarks>
        /// Defaults <see cref="HashFunctionBase.HashSize" /> to 32. <inheritdoc cref="CityHash(int)" />
        /// </remarks>
        /// <inheritdoc cref="CityHash(int)" />
        public CityHash()
            : this(32)
        { 
        
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CityHash"/> class.
        /// </summary>
        /// <param name="hashSize"><inheritdoc cref="HashFunctionBase(int)" select="param[name=hashSize]" /></param>
        /// <exception cref="System.ArgumentOutOfRangeException">hashSize;hashSize must be contained within CityHash.ValidHashSizes.</exception>
        /// <inheritdoc cref="HashFunctionBase(int)" />
        public CityHash(int hashSize)
            : base(hashSize)
        {
            if (!ValidHashSizes.Contains(hashSize))
                throw new ArgumentOutOfRangeException("hashSize", "hashSize must be contained within CityHash.ValidHashSizes.");
        }

        
        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(UnifiedData data)
        {
            byte[] hash = null;
            var dataArray = data.ToArray();
            
            switch (HashSize)
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
            }

            return hash;
        }
        
#if NET45
        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override async Task<byte[]> ComputeHashAsyncInternal(UnifiedData data)
        {
            byte[] hash = null;
            var dataArray = await data.ToArrayAsync()
                .ConfigureAwait(false);

            switch (HashSize)
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
            }

            return hash;
        }
#endif


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
                UInt32 a0 = (BitConverter.ToUInt32(data, data.Length - 4) * c1).RotateRight(17) * c2;
                UInt32 a1 = (BitConverter.ToUInt32(data, data.Length - 8) * c1).RotateRight(17) * c2;
                UInt32 a2 = (BitConverter.ToUInt32(data, data.Length - 16) * c1).RotateRight(17) * c2;
                UInt32 a3 = (BitConverter.ToUInt32(data, data.Length - 12) * c1).RotateRight(17) * c2;
                UInt32 a4 = (BitConverter.ToUInt32(data, data.Length - 20) * c1).RotateRight(17) * c2;

                h ^= a0;
                h = h.RotateRight(19);
                h = h * 5 + 0xe6546b64;
                h ^= a2;
                h = h.RotateRight(19);
                h = h * 5 + 0xe6546b64;

                g ^= a1;
                g = g.RotateRight(19);
                g = g * 5 + 0xe6546b64;
                g ^= a3;
                g = g.RotateRight(19);
                g = g * 5 + 0xe6546b64;

                f += a4;
                f = f.RotateRight(19);
                f = f * 5 + 0xe6546b64;
            }

            for (int x = 0; x < (data.Length - 1) / 20; ++x)
            {
                UInt32 a0 = (BitConverter.ToUInt32(data, 20 * x + 0) * c1).RotateRight(17) * c2;
                UInt32 a1 =  BitConverter.ToUInt32(data, 20 * x + 4);
                UInt32 a2 = (BitConverter.ToUInt32(data, 20 * x + 8) * c1).RotateRight(17) * c2;
                UInt32 a3 = (BitConverter.ToUInt32(data, 20 * x + 12) * c1).RotateRight(17) * c2;
                UInt32 a4 =  BitConverter.ToUInt32(data, 20 * x + 16);

                h ^= a0;
                h = h.RotateRight(18);
                h = h * 5 + 0xe6546b64;

                f += a1;
                f = f.RotateRight(19);
                f = f * c1;

                g += a2;
                g = g.RotateRight(18);
                g = g * 5 + 0xe6546b64;

                h ^= a3 + a1;
                h = h.RotateRight(19);
                h = h * 5 + 0xe6546b64;

                g ^= a4;
                g = g.ReverseByteOrder() * 5;

                h += a4 * 5;
                h = h.ReverseByteOrder();

                f += a0;

                Permute3(ref f, ref h, ref g);
            }

            g = g.RotateRight(11) * c1;
            g = g.RotateRight(17) * c1;

            f = f.RotateRight(11) * c1;
            f = f.RotateRight(17) * c1;

            h = (h + g).RotateRight(19);
            h = h * 5 + 0xe6546b64;
            h = h.RotateRight(17) * c1;
            h = (h + f).RotateRight(19);
            h = h * 5 + 0xe6546b64;
            h = h.RotateRight(17) * c1;

            return h;
        }


#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static UInt32 Hash32Len0to4(byte[] data) 
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

#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static UInt32 Hash32Len5to12(byte[] data) 
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

#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static UInt32 Hash32Len13to24(byte[] data) 
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
            
            for (int i = 0; i < data.Length >> 6; ++i)
            {
                x = (x + y + v.Low + BitConverter.ToUInt64(data, 64 * i + 8)).RotateRight(37) * k1;
                y = (y + v.High + BitConverter.ToUInt64(data, 64 * i + 48)).RotateRight(42) * k1;
                x ^= w.High;
                y += v.Low + BitConverter.ToUInt64(data, 64 * i + 40);
                z = (z + w.Low).RotateRight(33) * k1;
                v = WeakHashLen32WithSeeds(data, 64 * i, v.High * k1, x + w.Low);
                w = WeakHashLen32WithSeeds(data, 64 * i + 32, z + w.High, y + BitConverter.ToUInt64(data, 64 * i + 16));
                
                UInt64 temp = x;
                x = z;
                z = temp;
            }

            return HashLen16(HashLen16(v.Low, w.Low) + Mix(y) * k1 + z,
                            HashLen16(v.High, w.High) + x);
        }


#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static UInt64 HashLen16(UInt64 u, UInt64 v) {
          return Hash128to64(new UInt128() { Low = u, High = v });
        }

#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static UInt64 HashLen16(UInt64 u, UInt64 v, UInt64 mul) 
        {
            UInt64 a = (u ^ v) * mul;
            a ^= (a >> 47);

            UInt64 b = (v ^ a) * mul;
            b ^= (b >> 47);
            b *= mul;

            return b;
        }

#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static UInt64 HashLen0to16(byte[] data) 
        {
            if (data.Length >= 8) 
            {
                UInt64 mul = k2 + (UInt64) data.Length * 2;
                UInt64 a = BitConverter.ToUInt64(data, 0) + k2;
                UInt64 b = BitConverter.ToUInt64(data, data.Length - 8);
                UInt64 c = b.RotateRight(37) * mul + a;
                UInt64 d = (a.RotateRight(25) + b) * mul;

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
#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static UInt64 HashLen17to32(byte[] data) 
        {
          UInt64 mul = k2 + (UInt64) data.Length * 2;
          UInt64 a = BitConverter.ToUInt64(data, 0) * k1;
          UInt64 b = BitConverter.ToUInt64(data, 8);
          UInt64 c = BitConverter.ToUInt64(data, data.Length - 8) * mul;
          UInt64 d = BitConverter.ToUInt64(data, data.Length - 16) * k2;

          return HashLen16((a + b).RotateRight(43) + c.RotateRight(30) + d,
                           a + (b + k2).RotateRight(18) + c, mul);
        }

        // Return a 16-byte hash for 48 bytes.  Quick and dirty.
        // Callers do best to use "random-looking" values for a and b.
#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static UInt128 WeakHashLen32WithSeeds(
            UInt64 w, UInt64 x, UInt64 y, UInt64 z, UInt64 a, UInt64 b) 
        {
            a += w;
            b = (b + a + z).RotateRight(21);

            UInt64 c = a;
            a += x;
            a += y;

            b += a.RotateRight(44);

            return new UInt128() { Low = a + z, High = b + c };
        }

        // Return a 16-byte hash for s[0] ... s[31], a, and b.  Quick and dirty.
#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static UInt128 WeakHashLen32WithSeeds(byte[] data, int startIndex, UInt64 a, UInt64 b) 
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
#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static UInt64 HashLen33to64(byte[] data) 
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

            UInt64 u = (a + g).RotateRight(43) + (b.RotateRight(30) + c) * 9;
            UInt64 v = ((a + g) ^ d) + f + 1;
            UInt64 w = ((u + v) * mul).ReverseByteOrder() + h;
            UInt64 x = (e + f).RotateRight(42) + c;
            UInt64 y = (((v + w) * mul).ReverseByteOrder() + g) * mul;
            UInt64 z = e + f + c;

            a = ((x + z) * mul + y).ReverseByteOrder() + b;
            b = Mix((z + a) * mul + d + h) * mul;
            return b + x;
        }

        #endregion

        #region ComputeHash128

        /// <summary>128-bit implementation of ComputeHash.</summary>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>UInt128 value representing the hash value.</returns>
        protected virtual UInt128 ComputeHash128(byte[] data) {
            return 
                data.Length >= 16 ?
                CityHash128WithSeed(
                    data.Skip(16).ToArray(), 
                    new UInt128() { 
                        Low = BitConverter.ToUInt64(data, 0), 
                        High = BitConverter.ToUInt64(data, 8) + k0
                    }) :
                CityHash128WithSeed(
                    data, 
                    new UInt128() { 
                        Low = k0, 
                        High = k1 
                    });
        }


        // A subroutine for CityHash128().  Returns a decent 128-bit hash for strings
        // of any length representable in signed long.  Based on City and Murmur.
#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static UInt128 CityMurmur(byte[] data, UInt128 seed) {
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
            return new UInt128() { Low = a ^ b, High = HashLen16(b, a) };
        }

#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static UInt128 CityHash128WithSeed(byte[] data, UInt128 seed)
        {
            if (data.Length < 128) {
                return CityMurmur(data, seed);
            }

            // We expect len >= 128 to be the common case.  Keep 56 bytes of state:
            // v, w, x, y, and z.
            UInt128 v = new UInt128();
            v.Low = (seed.High ^ k1).RotateRight(49) * k1 + BitConverter.ToUInt64(data, 0);
            v.High = (v.Low).RotateRight(42) * k1 + BitConverter.ToUInt64(data, 8);

            UInt128 w = new UInt128();
            w.Low = (seed.High + ((UInt64) data.LongLength * k1)).RotateRight(35) * k1 + seed.Low;
            w.High = (seed.Low + BitConverter.ToUInt64(data, 88)).RotateRight(53) * k1;

            UInt64 x = seed.Low;
            UInt64 y = seed.High;
            UInt64 z = (UInt64) data.LongLength * k1;



            // This is the same inner loop as CityHash64(), manually unrolled.
            for (int i = 0; i < data.Length / 128; ++i)
            {
                x = (x + y + v.Low + BitConverter.ToUInt64(data, (128 * i) + 8)).RotateRight(37) * k1;
                y = (y + v.High + BitConverter.ToUInt64(data, (128 * i) + 48)).RotateRight(42) * k1;
                x ^= w.High;
                y += v.Low + BitConverter.ToUInt64(data, (128 * i) + 40);
                z = (z + w.Low).RotateRight(33) * k1;
                v = WeakHashLen32WithSeeds(data, 128 * i, v.High * k1, x + w.Low);
                w = WeakHashLen32WithSeeds(data, (128 * i) + 32, z + w.High, y + BitConverter.ToUInt64(data, (128 * i) + 16));

                {
                    UInt64 temp = z;
                    z = x;
                    x = temp;
                }

                x = (x + y + v.Low + BitConverter.ToUInt64(data, (128 * i) + 72)).RotateRight(37) * k1;
                y = (y + v.High + BitConverter.ToUInt64(data, (128 * i) + 112)).RotateRight(42) * k1;
                x ^= w.High;
                y += v.Low + BitConverter.ToUInt64(data, (128 * i) + 104);
                z = (z + w.Low).RotateRight(33) * k1;
                v = WeakHashLen32WithSeeds(data, (128 * i) + 64, v.High * k1, x + w.Low);
                w = WeakHashLen32WithSeeds(data, (128 * i) + 96, z + w.High, y + BitConverter.ToUInt64(data, (128 * i) + 80));

                {
                    UInt64 temp = z;
                    z = x;
                    x = temp;
                }


            }


            x += (v.Low + z).RotateRight(49) * k0;
            y = y * k0 + (w.High).RotateRight(37);
            z = z * k0 + (w.Low).RotateRight(27);
            w.Low *= 9;
            v.Low *= k0;


            
            // If 0 < len < 128, hash up to 4 chunks of 32 bytes each from the end of s.
            for (int i = 1; i <= (((data.Length % 128) + 31) / 32); ++i) 
            {
                y = (x + y).RotateRight(42) * k0 + v.High;
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

            return new UInt128() {
                Low = HashLen16(x + v.High, w.High) + y,
                High = HashLen16(x + w.High, y + v.High)
            };
        }

        #endregion

        #region Shared Utilities

#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static UInt32 Mix(UInt32 h)
        {
            h ^= h >> 16;
            h *= 0x85ebca6b;
            h ^= h >> 13;
            h *= 0xc2b2ae35;
            h ^= h >> 16;
            return h;
        }

#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static UInt64 Mix(UInt64 val)
        {
            return val ^ (val >> 47);
        }


#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static UInt32 Mur(UInt32 a, UInt32 h)
        {
            // Helper from Murmur3 for combining two 32-bit values.
            a *= c1;
            a = a.RotateRight(17);
            a *= c2;
            h ^= a;
            h = h.RotateRight(19);
            return h * 5 + 0xe6546b64;
        }

#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static void Permute3(ref UInt32 a, ref UInt32 b, ref UInt32 c)
        {
            UInt32 temp = a;

            a = c;
            c = b;
            b = temp;
        }

#if NET45
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
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

        #endregion

    }
}
