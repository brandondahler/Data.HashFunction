using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.CityHash.Utilities;
using OpenSource.Data.HashFunction.Core;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenSource.Data.HashFunction.Core.Utilities;

namespace OpenSource.Data.HashFunction.CityHash
{
    internal class CityHash_Implementation
        : HashFunctionBase,
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
        protected override IHashValue ComputeHashInternal(ArraySegment<byte> data, CancellationToken cancellationToken)
        {   
            switch (_config.HashSizeInBits)
            {
                case 32:
                    return ComputeHash32(data, cancellationToken);

                case 64:
                    return ComputeHash64(data, cancellationToken);

                case 128:
                    return ComputeHash128(data, cancellationToken);

                default:
                    throw new NotImplementedException();
            }
        }

        #region ComputeHash32

        /// <summary>32-bit implementation of ComputeHash.</summary>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>UInt32 value representing the hash value.</returns>
        protected virtual IHashValue ComputeHash32(ArraySegment<byte> data, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var dataCount = data.Count;

            UInt32 hashValue;

            if (dataCount > 24)
            {
                hashValue = Hash32Len25Plus(data, cancellationToken);

            } else if (dataCount > 12) {
                hashValue = Hash32Len13to24(data);

            } else if (dataCount > 4) {
                hashValue = Hash32Len5to12(data);

            } else {
                hashValue = Hash32Len0to4(data);
            }

            return new HashValue(
                BitConverter.GetBytes(hashValue),
                32);
        }


        private UInt32 Hash32Len0to4(ArraySegment<byte> data) 
        {
            var dataArray = data.Array;
            var dataOffset = data.Offset;
            var dataCount = data.Count;

            var endOffset = dataOffset + dataCount;

            UInt32 b = 0;
            UInt32 c = 9;

            for (var currentOffset = dataOffset; currentOffset < endOffset; currentOffset += 1)
            {
                b = b * c1 + dataArray[currentOffset];
                c ^= b;
            }

            return Mix(Mur(b, Mur((UInt32) dataCount, c)));
        }

        private UInt32 Hash32Len5to12(ArraySegment<byte> data) 
        {
            var dataArray = data.Array;
            var dataOffset = data.Offset;
            var dataCount = data.Count;

            UInt32 a = (UInt32) dataCount;
            UInt32 b = (UInt32) dataCount * 5;

            UInt32 c = 9;
            UInt32 d = b;

            a += BitConverter.ToUInt32(dataArray, dataOffset);
            b += BitConverter.ToUInt32(dataArray, dataOffset + dataCount - 4);
            c += BitConverter.ToUInt32(dataArray, dataOffset + ((dataCount >> 1) & 4));

            return Mix(Mur(c, Mur(b, Mur(a, d))));
        }

        private UInt32 Hash32Len13to24(ArraySegment<byte> data) 
        {
            var dataArray = data.Array;
            var dataOffset = data.Offset;
            var dataCount = data.Count;


            UInt32 a = BitConverter.ToUInt32(dataArray, dataOffset + (dataCount >> 1) - 4);
            UInt32 b = BitConverter.ToUInt32(dataArray, dataOffset + 4);
            UInt32 c = BitConverter.ToUInt32(dataArray, dataOffset + dataCount - 8);
            UInt32 d = BitConverter.ToUInt32(dataArray, dataOffset + (dataCount >> 1));
            UInt32 e = BitConverter.ToUInt32(dataArray, dataOffset);
            UInt32 f = BitConverter.ToUInt32(dataArray, dataOffset + dataCount - 4);
            UInt32 h = (UInt32) dataCount;
            
            return Mix(Mur(f, Mur(e, Mur(d, Mur(c, Mur(b, Mur(a, h)))))));
        }

        private UInt32 Hash32Len25Plus(ArraySegment<byte> data, CancellationToken cancellationToken)
        {
            
            var dataArray = data.Array;
            var dataOffset = data.Offset;
            var dataCount = data.Count;

            var endOffset = dataOffset + dataCount;

            cancellationToken.ThrowIfCancellationRequested();

            // dataCount > 24
            UInt32 h = (UInt32) dataCount;
            UInt32 g = (UInt32) dataCount * c1;
            UInt32 f = g;
            {
                UInt32 a0 = RotateRight(BitConverter.ToUInt32(dataArray, endOffset - 4) * c1, 17) * c2;
                UInt32 a1 = RotateRight(BitConverter.ToUInt32(dataArray, endOffset - 8) * c1, 17) * c2;
                UInt32 a2 = RotateRight(BitConverter.ToUInt32(dataArray, endOffset - 16) * c1, 17) * c2;
                UInt32 a3 = RotateRight(BitConverter.ToUInt32(dataArray, endOffset - 12) * c1, 17) * c2;
                UInt32 a4 = RotateRight(BitConverter.ToUInt32(dataArray, endOffset - 20) * c1, 17) * c2;

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


            var groupsToProcess = (dataCount - 1) / 20;
            var groupEndOffset = dataOffset + (groupsToProcess * 20);

            for (int groupOffset = dataOffset; groupOffset < groupEndOffset; groupOffset += 20)
            {
                cancellationToken.ThrowIfCancellationRequested();

                UInt32 a0 = RotateRight(BitConverter.ToUInt32(dataArray, groupOffset + 0) * c1, 17) * c2;
                UInt32 a1 =  BitConverter.ToUInt32(dataArray, groupOffset + 4);
                UInt32 a2 = RotateRight(BitConverter.ToUInt32(dataArray, groupOffset + 8) * c1, 17) * c2;
                UInt32 a3 = RotateRight(BitConverter.ToUInt32(dataArray, groupOffset + 12) * c1, 17) * c2;
                UInt32 a4 =  BitConverter.ToUInt32(dataArray, groupOffset + 16);

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

            cancellationToken.ThrowIfCancellationRequested();

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

        #endregion

        #region ComputeHash64

        /// <summary>64-bit implementation of ComputeHash.</summary>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>UInt64 value representing the hash value.</returns>
        protected virtual IHashValue ComputeHash64(ArraySegment<byte> data, CancellationToken cancellationToken) 
        {
            cancellationToken.ThrowIfCancellationRequested();

            var dataCount = data.Count;
            UInt64 hashValue;

            if (dataCount > 64)
            {
                hashValue = Hash64Len65Plus(data, cancellationToken);

            } else if (dataCount > 32) {
                hashValue = Hash64Len33to64(data);

            } else if (dataCount > 16) {
                hashValue = Hash64Len17to32(data);

            } else {
                hashValue = Hash64Len0to16(data);
            }
            
            return new HashValue(
                BitConverter.GetBytes(hashValue),
                64);
        }


        private UInt64 Hash64Len16(UInt64 u, UInt64 v) {
          return Hash128to64(
              new UInt128(u, v));
        }

        private static UInt64 Hash64Len16(UInt64 u, UInt64 v, UInt64 mul) 
        {
            UInt64 a = (u ^ v) * mul;
            a ^= (a >> 47);

            UInt64 b = (v ^ a) * mul;
            b ^= (b >> 47);
            b *= mul;

            return b;
        }

        private UInt64 Hash64Len0to16(ArraySegment<byte> data) 
        {
            var dataArray = data.Array;
            var dataOffset = data.Offset;
            var dataCount = data.Count;

            var endOffset = dataOffset + dataCount;

            if (dataCount >= 8) 
            {
                UInt64 mul = k2 + (UInt64) dataCount * 2;
                UInt64 a = BitConverter.ToUInt64(dataArray, dataOffset) + k2;
                UInt64 b = BitConverter.ToUInt64(dataArray, endOffset - 8);
                UInt64 c = RotateRight(b, 37) * mul + a;
                UInt64 d = (RotateRight(a, 25) + b) * mul;

                return Hash64Len16(c, d, mul);
            }

            if (dataCount >= 4) 
            {
                UInt64 mul = k2 + (UInt64) dataCount * 2;
                UInt64 a = BitConverter.ToUInt32(dataArray, dataOffset);
                return Hash64Len16((UInt64) dataCount + (a << 3), BitConverter.ToUInt32(dataArray, endOffset - 4), mul);
            }

            if (dataCount > 0) 
            {
                byte a = dataArray[dataOffset];
                byte b = dataArray[dataOffset + (dataCount >> 1)];
                byte c = dataArray[endOffset - 1];

                UInt32 y = (UInt32) a + ((UInt32) b << 8);
                UInt32 z = (UInt32) dataCount + ((UInt32) c << 2);

                return Mix((UInt64) (y * k2 ^ z * k0)) * k2;
            }

            return k2;
        }

        // This probably works well for 16-byte strings as well, but it may be overkill
        // in that case.
        private UInt64 Hash64Len17to32(ArraySegment<byte> data) 
        {
            var dataArray = data.Array;
            var dataOffset = data.Offset;
            var dataCount = data.Count;

            var endOffset = dataOffset + dataCount;

            UInt64 mul = k2 + (UInt64) dataCount * 2;
            UInt64 a = BitConverter.ToUInt64(dataArray, dataOffset) * k1;
            UInt64 b = BitConverter.ToUInt64(dataArray, dataOffset + 8);
            UInt64 c = BitConverter.ToUInt64(dataArray, endOffset - 8) * mul;
            UInt64 d = BitConverter.ToUInt64(dataArray, endOffset - 16) * k2;

            return Hash64Len16(
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
        private UInt64 Hash64Len33to64(ArraySegment<byte> data) 
        {
            var dataArray = data.Array;
            var dataOffset = data.Offset;
            var dataCount = data.Count;

            var endOffset = dataOffset + dataCount;

            UInt64 mul = k2 + (UInt64) dataCount * 2;
            UInt64 a = BitConverter.ToUInt64(dataArray, dataOffset) * k2;
            UInt64 b = BitConverter.ToUInt64(dataArray, dataOffset + 8);
            UInt64 c = BitConverter.ToUInt64(dataArray, endOffset - 24);
            UInt64 d = BitConverter.ToUInt64(dataArray, endOffset - 32);
            UInt64 e = BitConverter.ToUInt64(dataArray, dataOffset + 16) * k2;
            UInt64 f = BitConverter.ToUInt64(dataArray, dataOffset + 24) * 9;
            UInt64 g = BitConverter.ToUInt64(dataArray, endOffset - 8);
            UInt64 h = BitConverter.ToUInt64(dataArray, endOffset - 16) * mul;

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

        private UInt64 Hash64Len65Plus(ArraySegment<byte> data, CancellationToken cancellationToken)
        {
            var dataArray = data.Array;
            var dataOffset = data.Offset;
            var dataCount = data.Count;

            var endOffset = dataOffset + dataCount;

            // For strings over 64 bytes we hash the end first, and then as we
            // loop we keep 56 bytes of state: v, w, x, y, and z.
            UInt64 x = BitConverter.ToUInt64(dataArray, endOffset - 40);
            UInt64 y = BitConverter.ToUInt64(dataArray, endOffset - 16) + BitConverter.ToUInt64(dataArray, endOffset - 56);
            UInt64 z = Hash64Len16(
                BitConverter.ToUInt64(dataArray, endOffset - 48) + (UInt64) dataCount, 
                BitConverter.ToUInt64(dataArray, endOffset - 24));
            
            UInt128 v = WeakHashLen32WithSeeds(dataArray, endOffset - 64, (UInt64) dataCount, z);
            UInt128 w = WeakHashLen32WithSeeds(dataArray, endOffset - 32, y + k1, x);

            x = x * k1 + BitConverter.ToUInt64(dataArray, 0);

            // For each 64-byte chunk
            var groupEndOffset = dataOffset + (dataCount - (dataCount % 64));

            for (var currentOffset = dataOffset; currentOffset < groupEndOffset; currentOffset += 64)
            {
                cancellationToken.ThrowIfCancellationRequested();

                x = RotateRight(x + y + v.Low + BitConverter.ToUInt64(dataArray, currentOffset + 8), 37) * k1;
                y = RotateRight(y + v.High + BitConverter.ToUInt64(dataArray, currentOffset + 48), 42) * k1;
                x ^= w.High;
                y += v.Low + BitConverter.ToUInt64(dataArray, currentOffset + 40);
                z = RotateRight(z + w.Low, 33) * k1;
                v = WeakHashLen32WithSeeds(dataArray, currentOffset, v.High * k1, x + w.Low);
                w = WeakHashLen32WithSeeds(dataArray, currentOffset + 32, z + w.High, y + BitConverter.ToUInt64(dataArray, currentOffset + 16));
                
                UInt64 temp = x;
                x = z;
                z = temp;
            }

            return Hash64Len16(Hash64Len16(v.Low, w.Low) + Mix(y) * k1 + z,
                            Hash64Len16(v.High, w.High) + x);
        }


        #endregion

        #region ComputeHash128

        /// <summary>128-bit implementation of ComputeHash.</summary>
        /// <param name="data">Data to be hashed.</param>
        /// <returns>UInt128 value representing the hash value.</returns>
        private IHashValue ComputeHash128(ArraySegment<byte> data, CancellationToken cancellationToken)
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
                RotateRight(seed.High + ((UInt64) dataCount * k1), 35) * k1 + seed.Low,
                RotateRight(seed.Low + BitConverter.ToUInt64(dataArray, dataOffset + 88), 53) * k1);

            UInt64 x = seed.Low;
            UInt64 y = seed.High;
            UInt64 z = (UInt64) dataCount * k1;



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
            x = Hash64Len16(x, v.Low);
            y = Hash64Len16(y + z, w.Low);

            return new UInt128(
                Hash64Len16(x + v.High, w.High) + y,
                Hash64Len16(x + w.High, y + v.High));
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
                c = b * k1 + Hash64Len0to16(data);
                d = Mix(a + (dataCount >= 8 ? BitConverter.ToUInt64(dataArray, dataOffset) : c));

            } else {  
                // len > 16
                c = Hash64Len16(BitConverter.ToUInt64(dataArray, endOffset - 8) + k1, a);
                d = Hash64Len16(b + (UInt64) dataCount, c + BitConverter.ToUInt64(dataArray, endOffset - 16));
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

            a = Hash64Len16(a, c);
            b = Hash64Len16(d, b);
            return new UInt128(a ^ b, Hash64Len16(b, a));
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

        private static UInt64 Mix(UInt64 value) =>
            value ^ (value >> 47);

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
