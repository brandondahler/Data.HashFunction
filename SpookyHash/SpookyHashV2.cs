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
    /// Implements SpookyHash V2 as specified at http://burtleburtle.net/bob/hash/spooky.html.
    /// </summary>
    public class SpookyHashV2
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
        /// Constructs new <see cref="SpookyHashV2"/> instance.
        /// </summary>
        public SpookyHashV2()
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

            s0  += BitConverter.ToUInt64(data, startIndex + ( 0 * 8));     s2 ^= s10;   s11 ^= s0;      s0 =  s0.RotateLeft(11);     s11 += s1;
            s1  += BitConverter.ToUInt64(data, startIndex + ( 1 * 8));     s3 ^= s11;    s0 ^=  s1;     s1 =  s1.RotateLeft(32);     s0 +=  s2;
            s2  += BitConverter.ToUInt64(data, startIndex + ( 2 * 8));     s4 ^=  s0;    s1 ^=  s2;     s2 =  s2.RotateLeft(43);     s1 +=  s3;
            s3  += BitConverter.ToUInt64(data, startIndex + ( 3 * 8));     s5 ^=  s1;    s2 ^=  s3;     s3 =  s3.RotateLeft(31);     s2 +=  s4;
            s4  += BitConverter.ToUInt64(data, startIndex + ( 4 * 8));     s6 ^=  s2;    s3 ^=  s4;     s4 =  s4.RotateLeft(17);     s3 +=  s5;
            s5  += BitConverter.ToUInt64(data, startIndex + ( 5 * 8));     s7 ^=  s3;    s4 ^=  s5;     s5 =  s5.RotateLeft(28);     s4 +=  s6;
            s6  += BitConverter.ToUInt64(data, startIndex + ( 6 * 8));     s8 ^=  s4;    s5 ^=  s6;     s6 =  s6.RotateLeft(39);     s5 +=  s7;
            s7  += BitConverter.ToUInt64(data, startIndex + ( 7 * 8));     s9 ^=  s5;    s6 ^=  s7;     s7 =  s7.RotateLeft(57);     s6 +=  s8;
            s8  += BitConverter.ToUInt64(data, startIndex + ( 8 * 8));    s10 ^=  s6;    s7 ^=  s8;     s8 =  s8.RotateLeft(55);     s7 +=  s9;
            s9  += BitConverter.ToUInt64(data, startIndex + ( 9 * 8));    s11 ^=  s7;    s8 ^=  s9;     s9 =  s9.RotateLeft(54);     s8 += s10;
            s10 += BitConverter.ToUInt64(data, startIndex + (10 * 8));     s0 ^=  s8;    s9 ^= s10;    s10 = s10.RotateLeft(22);     s9 += s11;
            s11 += BitConverter.ToUInt64(data, startIndex + (11 * 8));     s1 ^=  s9;   s10 ^= s11;    s11 = s11.RotateLeft(46);    s10 +=  s0;
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
