using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    public class SpookyHashV2
        : HashFunctionBase
    {
        public override IEnumerable<int> ValidHashSizes { get { return new[] { 32, 64, 128 }; } }

        public UInt64 InitVal1 { get; set; }

        public UInt64 InitVal2 { get; set; }


        public SpookyHashV2()
            : base(128)
        {
            InitVal1 = 0;
            InitVal2 = 0;
        }


        public override byte[] ComputeHash(byte[] data)
        {
            UInt64 h0, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11;
            UInt64[] buf = new UInt64[12];

            h0 = h3 = h6 = h9 = InitVal1;
            h1 = h4 = h7 = h10 = (HashSize == 128 ? InitVal2 : InitVal1);
            h2 = h5 = h8 = h11 = 0XDEADBEEFDEADBEEF;

            for (int x = 0; x < data.Length / 96; ++x)
            {
                Mix(data, x * 96, 
                    ref h0, ref h1, ref h2, ref h3, ref h4, ref h5,
                    ref h6, ref h7, ref h8, ref h9, ref h10, ref h11);
            }
        
            // Handle last partial block
            var remainderData = new byte[96];

            Array.Copy(data, data.Length - (data.Length % 96), remainderData, 0, data.Length % 96);
            Array.Clear(remainderData, data.Length % 96, remainderData.Length - (data.Length % 96));

            remainderData[remainderData.Length - 1] = (byte)(data.Length % 96);

            // do some final mixing 
            End(remainderData, 0,
                ref h0, ref h1, ref h2, ref h3, ref h4, ref h5,
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
                    throw new ArgumentOutOfRangeException("HashSize set to unexpected value.");
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static UInt64  Rot64(UInt64  x, int k)
        {
            return (x << k) | (x >> (64 - k));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static void Mix(
            byte[] data, int startIndex,
            ref UInt64  s0, ref UInt64  s1, ref UInt64  s2, ref UInt64  s3,
            ref UInt64  s4, ref UInt64  s5, ref UInt64  s6, ref UInt64  s7,
            ref UInt64  s8, ref UInt64  s9, ref UInt64  s10,ref UInt64  s11)
        {

            s0  += BitConverter.ToUInt64(data, startIndex + ( 0 * 8));     s2 ^= s10;   s11 ^= s0;      s0 = Rot64(s0, 11);     s11 += s1;
            s1  += BitConverter.ToUInt64(data, startIndex + ( 1 * 8));     s3 ^= s11;    s0 ^=  s1;     s1 = Rot64( s1, 32);     s0 +=  s2;
            s2  += BitConverter.ToUInt64(data, startIndex + ( 2 * 8));     s4 ^=  s0;    s1 ^=  s2;     s2 = Rot64( s2, 43);     s1 +=  s3;
            s3  += BitConverter.ToUInt64(data, startIndex + ( 3 * 8));     s5 ^=  s1;    s2 ^=  s3;     s3 = Rot64( s3, 31);     s2 +=  s4;
            s4  += BitConverter.ToUInt64(data, startIndex + ( 4 * 8));     s6 ^=  s2;    s3 ^=  s4;     s4 = Rot64( s4, 17);     s3 +=  s5;
            s5  += BitConverter.ToUInt64(data, startIndex + ( 5 * 8));     s7 ^=  s3;    s4 ^=  s5;     s5 = Rot64( s5, 28);     s4 +=  s6;
            s6  += BitConverter.ToUInt64(data, startIndex + ( 6 * 8));     s8 ^=  s4;    s5 ^=  s6;     s6 = Rot64( s6, 39);     s5 +=  s7;
            s7  += BitConverter.ToUInt64(data, startIndex + ( 7 * 8));     s9 ^=  s5;    s6 ^=  s7;     s7 = Rot64( s7, 57);     s6 +=  s8;
            s8  += BitConverter.ToUInt64(data, startIndex + ( 8 * 8));    s10 ^=  s6;    s7 ^=  s8;     s8 = Rot64( s8, 55);     s7 +=  s9;
            s9  += BitConverter.ToUInt64(data, startIndex + ( 9 * 8));    s11 ^=  s7;    s8 ^=  s9;     s9 = Rot64( s9, 54);     s8 += s10;
            s10 += BitConverter.ToUInt64(data, startIndex + (10 * 8));     s0 ^=  s8;    s9 ^= s10;    s10 = Rot64(s10, 22);     s9 += s11;
            s11 += BitConverter.ToUInt64(data, startIndex + (11 * 8));     s1 ^=  s9;   s10 ^= s11;    s11 = Rot64(s11, 46);    s10 +=  s0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static void EndPartial(
            ref UInt64  h0, ref UInt64  h1, ref UInt64  h2, ref UInt64  h3,
            ref UInt64  h4, ref UInt64  h5, ref UInt64  h6, ref UInt64  h7, 
            ref UInt64  h8, ref UInt64  h9, ref UInt64  h10,ref UInt64  h11)
        {
            h11 +=  h1;     h2 ^= h11;     h1 = Rot64( h1, 44);
            h0  +=  h2;     h3 ^=  h0;     h2 = Rot64( h2, 15);
            h1  +=  h3;     h4 ^=  h1;     h3 = Rot64( h3, 34);
            h2  +=  h4;     h5 ^=  h2;     h4 = Rot64( h4, 21);
            h3  +=  h5;     h6 ^=  h3;     h5 = Rot64( h5, 38);
            h4  +=  h6;     h7 ^=  h4;     h6 = Rot64( h6, 33);
            h5  +=  h7;     h8 ^=  h5;     h7 = Rot64( h7, 10);
            h6  +=  h8;     h9 ^=  h6;     h8 = Rot64( h8, 13);
            h7  +=  h9;    h10 ^=  h7;     h9 = Rot64( h9, 38);
            h8  += h10;    h11 ^=  h8;    h10 = Rot64(h10, 53);
            h9  += h11;     h0 ^=  h9;    h11 = Rot64(h11, 42);
            h10 +=  h0;     h1 ^= h10;     h0 = Rot64( h0, 54);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static void End(
            byte[] data, int startIndex,
            ref UInt64  h0, ref UInt64  h1, ref UInt64  h2, ref UInt64  h3,
            ref UInt64  h4, ref UInt64  h5, ref UInt64  h6, ref UInt64  h7, 
            ref UInt64  h8, ref UInt64  h9, ref UInt64  h10,ref UInt64  h11)
        {
            h0  += BitConverter.ToUInt64(data, startIndex + (0  * 8));
            h1  += BitConverter.ToUInt64(data, startIndex + (1  * 8));
            h2  += BitConverter.ToUInt64(data, startIndex + (2  * 8));
            h3  += BitConverter.ToUInt64(data, startIndex + (3  * 8));
            h4  += BitConverter.ToUInt64(data, startIndex + (4  * 8));
            h5  += BitConverter.ToUInt64(data, startIndex + (5  * 8));
            h6  += BitConverter.ToUInt64(data, startIndex + (6  * 8));
            h7  += BitConverter.ToUInt64(data, startIndex + (7  * 8));
            h8  += BitConverter.ToUInt64(data, startIndex + (8  * 8));
            h9  += BitConverter.ToUInt64(data, startIndex + (9  * 8));
            h10 += BitConverter.ToUInt64(data, startIndex + (10 * 8));
            h11 += BitConverter.ToUInt64(data, startIndex + (11 * 8));
             
            EndPartial(ref h0, ref h1, ref h2, ref h3, ref h4, ref h5, ref h6, ref h7, ref h8, ref h9, ref h10, ref h11);
            EndPartial(ref h0, ref h1, ref h2, ref h3, ref h4, ref h5, ref h6, ref h7, ref h8, ref h9, ref h10, ref h11);
            EndPartial(ref h0, ref h1, ref h2, ref h3, ref h4, ref h5, ref h6, ref h7, ref h8, ref h9, ref h10, ref h11);
        }
    }
}
