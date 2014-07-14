using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Implementation of SpookyHash V1 as specified at http://burtleburtle.net/bob/hash/spooky.html.
    /// 
    /// This hash function has been superseded by <see cref="SpookyHashV2"/> due to a loss of entropy from a 
    ///   coding error in the original specification.  It still passes the hash function tests the creator set for it,
    ///   but it is preferred that SpookyHash V2 is used.
    /// </summary>
    [Obsolete("SpookyHashV1 has known issues, use SpookyHashV2.")]
    public class SpookyHashV1
        : HashFunctionBase
    {
        /// <inheritdoc/>
        public override IEnumerable<int> ValidHashSizes { get { return new[] { 32, 64, 128 }; } }

        /// <summary>
        /// First seed value for hash calculation.
        /// </summary>
        public UInt64 InitVal1 { get; set; }

        /// <summary>
        /// Second seed value for hash calculation.
        /// </summary>
        /// <remarks>
        /// Not used for 32-bit and 64-bit modes, used as second seed for 128-bit mode.
        /// </remarks>
        public UInt64 InitVal2 { get; set; }


        /// <summary>
        /// Constructs new <see cref="SpookyHashV1"/> instance.
        /// </summary>
        public SpookyHashV1()
            : base(128)
        {
            InitVal1 = 0;
            InitVal2 = 0;
        }


        /// <inheritdoc/>
        public override byte[] ComputeHash(byte[] data)
        {
            UInt64 h0, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11;
            UInt64[] buf = new UInt64[12];
    
            h0=h3=h6=h9  = InitVal1;
            h1=h4=h7=h10 = (HashSize == 128 ? InitVal2 : InitVal1);
            h2=h5=h8=h11 = 0XDEADBEEFDEADBEEF;

            for (int x = 0; x < data.Length / 96; ++x)
            {
                Mix(data, x * 96,
                    ref h0, ref h1, ref h2, ref h3, ref h4, ref h5,
                    ref h6, ref h7, ref h8, ref h9, ref h10, ref h11);
            }

            // Handle last partial block
            var remainderData = new byte[96];
            var remainderStartIndex = data.Length - (data.Length % 96);

            for (int x = 0; x < data.Length % 96; ++x)
                remainderData[x] = data[remainderStartIndex + x];

            for (int x = data.Length % 96; x < 95; ++x)
                remainderData[x] = (byte) 0;

            remainderData[95] = (byte)(data.Length - remainderStartIndex);

            Mix(remainderData, 0,
                ref h0, ref h1, ref h2, ref h3, ref h4, ref h5,
                ref h6, ref h7, ref h8, ref h9, ref h10, ref h11);

            // do some final mixing 
            End(ref h0, ref h1, ref h2, ref h3, ref h4, ref h5, 
                ref h6, ref h7, ref h8, ref h9, ref h10, ref h11);

            switch (HashSize)
            {
                case 32:
                    return BitConverter.GetBytes((UInt32) h0);
                case 64:
                    return BitConverter.GetBytes(h0);

                case 128:
                    var results = new byte[16];
                    BitConverter.GetBytes(h0).CopyTo(results, 0);
                    BitConverter.GetBytes(h1).CopyTo(results, 8);

                    return results;

                default:
                    throw new ArgumentOutOfRangeException("HashSize");
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Mix(
            byte[] data, int startIndex,
            ref UInt64  s0, ref UInt64  s1, ref UInt64  s2, ref UInt64  s3,
            ref UInt64  s4, ref UInt64  s5, ref UInt64  s6, ref UInt64  s7,
            ref UInt64  s8, ref UInt64  s9, ref UInt64  s10,ref UInt64  s11)
        {
            s0  += BitConverter.ToUInt64(data, startIndex);          s2 ^= s10;   s11 ^=  s0;     s0 =  s0.RotateLeft(11);    s11 +=  s1;
            s1  += BitConverter.ToUInt64(data, startIndex +  8);     s3 ^= s11;    s0 ^=  s1;     s1 =  s1.RotateLeft(32);     s0 +=  s2;
            s2  += BitConverter.ToUInt64(data, startIndex + 16);     s4 ^=  s0;    s1 ^=  s2;     s2 =  s2.RotateLeft(43);     s1 +=  s3;
            s3  += BitConverter.ToUInt64(data, startIndex + 24);     s5 ^=  s1;    s2 ^=  s3;     s3 =  s3.RotateLeft(31);     s2 +=  s4;
            s4  += BitConverter.ToUInt64(data, startIndex + 32);     s6 ^=  s2;    s3 ^=  s4;     s4 =  s4.RotateLeft(17);     s3 +=  s5;
            s5  += BitConverter.ToUInt64(data, startIndex + 40);     s7 ^=  s3;    s4 ^=  s5;     s5 =  s5.RotateLeft(28);     s4 +=  s6;
            s6  += BitConverter.ToUInt64(data, startIndex + 48);     s8 ^=  s4;    s5 ^=  s6;     s6 =  s6.RotateLeft(39);     s5 +=  s7;
            s7  += BitConverter.ToUInt64(data, startIndex + 56);     s9 ^=  s5;    s6 ^=  s7;     s7 =  s7.RotateLeft(57);     s6 +=  s8;
            s8  += BitConverter.ToUInt64(data, startIndex + 64);    s10 ^=  s6;    s7 ^=  s8;     s8 =  s8.RotateLeft(55);     s7 +=  s9;
            s9  += BitConverter.ToUInt64(data, startIndex + 72);    s11 ^=  s7;    s8 ^=  s9;     s9 =  s9.RotateLeft(54);     s8 += s10;
            s10 += BitConverter.ToUInt64(data, startIndex + 80);     s0 ^=  s8;    s9 ^= s10;    s10 = s10.RotateLeft(22);     s9 += s11;
            s11 += BitConverter.ToUInt64(data, startIndex + 88);     s1 ^=  s9;   s10 ^= s11;    s11 = s11.RotateLeft(46);    s10 +=  s0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void EndPartial(
            ref UInt64  h0, ref UInt64  h1, ref UInt64  h2, ref UInt64  h3,
            ref UInt64  h4, ref UInt64  h5, ref UInt64  h6, ref UInt64  h7, 
            ref UInt64  h8, ref UInt64  h9, ref UInt64  h10,ref UInt64  h11)
        {
            h11 +=  h1;     h2 ^= h11;     h1 =  h1.RotateLeft(44);
            h0  +=  h2;     h3 ^=  h0;     h2 =  h2.RotateLeft(15);
            h1  +=  h3;     h4 ^=  h1;     h3 =  h3.RotateLeft(34);
            h2  +=  h4;     h5 ^=  h2;     h4 =  h4.RotateLeft(21);
            h3  +=  h5;     h6 ^=  h3;     h5 =  h5.RotateLeft(38);
            h4  +=  h6;     h7 ^=  h4;     h6 =  h6.RotateLeft(33);
            h5  +=  h7;     h8 ^=  h5;     h7 =  h7.RotateLeft(10);
            h6  +=  h8;     h9 ^=  h6;     h8 =  h8.RotateLeft(13);
            h7  +=  h9;    h10 ^=  h7;     h9 =  h9.RotateLeft(38);
            h8  += h10;    h11 ^=  h8;    h10 = h10.RotateLeft(53);
            h9  += h11;     h0 ^=  h9;    h11 = h11.RotateLeft(42);
            h10 +=  h0;     h1 ^= h10;     h0 =  h0.RotateLeft(54);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void End(
            ref UInt64  h0, ref UInt64  h1, ref UInt64  h2, ref UInt64  h3,
            ref UInt64  h4, ref UInt64  h5, ref UInt64  h6, ref UInt64  h7, 
            ref UInt64  h8, ref UInt64  h9, ref UInt64  h10,ref UInt64  h11)
        {
            EndPartial(ref h0, ref h1, ref h2, ref h3, ref h4, ref h5, ref h6, ref h7, ref h8, ref h9, ref h10, ref h11);
            EndPartial(ref h0, ref h1, ref h2, ref h3, ref h4, ref h5, ref h6, ref h7, ref h8, ref h9, ref h10, ref h11);
            EndPartial(ref h0, ref h1, ref h2, ref h3, ref h4, ref h5, ref h6, ref h7, ref h8, ref h9, ref h10, ref h11);
        }
    }
}
