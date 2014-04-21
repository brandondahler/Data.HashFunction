using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    public class JenkinsLookup3
        : HashFunctionBase
    {
        public override IEnumerable<int> ValidHashSizes { get { return new[] { 32, 64}; } }

        public UInt32 InitVal1 { get; set; }

        public UInt32 InitVal2 { get; set; }


        public JenkinsLookup3()
            : base(64)
        {
            InitVal1 = 0;
            InitVal2 = 0;
        }

        public override byte[] ComputeHash(byte[] data)
        {
            UInt32 a = 0xdeadbeef + (UInt32) data.Length + InitVal1;
            UInt32 b = 0xdeadbeef + (UInt32) data.Length + InitVal1;
            UInt32 c = 0xdeadbeef + (UInt32) data.Length + InitVal1;

            if (HashSize == 64)
                c += InitVal2;

            for (int x = 0; x < data.Length / 12; ++x)
            {
                a += BitConverter.ToUInt32(data, (x * 12));
                b += BitConverter.ToUInt32(data, (x * 12) + 4);
                c += BitConverter.ToUInt32(data, (x * 12) + 8);

                Mix(ref a, ref b, ref c);
            }

            var remainderStartIndex = data.Length - (data.Length % 12);

            switch (data.Length % 12)
            {
                case 11: c += (UInt32) data[remainderStartIndex + 10] << 16;    goto case 10;
                case 10: c += (UInt32) data[remainderStartIndex +  9] << 8;     goto case  9;
                case  9: c += (UInt32) data[remainderStartIndex +  8];           goto case 8;   

                case 8:
                    b += BitConverter.ToUInt32(data, remainderStartIndex + 4);
                    goto case 4;

                case 7: b += (UInt32) data[remainderStartIndex + 6] << 16;  goto case 6;
                case 6: b += (UInt32) data[remainderStartIndex + 5] <<  8;  goto case 5;
                case 5: b += (UInt32) data[remainderStartIndex + 4];        goto case 4;

                case 4:
                    a += BitConverter.ToUInt32(data, remainderStartIndex);
                    break;
                    
                case 3: a += (UInt32) data[remainderStartIndex + 2] << 16;  goto case 2;
                case 2: a += (UInt32) data[remainderStartIndex + 1] <<  8;  goto case 1;
                case 1: a += (UInt32) data[remainderStartIndex];        break;
            }

            if (data.Length <= 12 || remainderStartIndex != 0)
                Final(ref a, ref b, ref c);

            switch (HashSize)
            {
                case 32:
                    return BitConverter.GetBytes(c);
                case 64:
                    return BitConverter.GetBytes((((UInt64)b) << 32) | c);
                default:
                    throw new ArgumentOutOfRangeException("HashSize is set to an invalid value.");
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Mix(ref UInt32 a, ref UInt32 b, ref UInt32 c)
        {
            a -= c; a ^= Rot(c,  4); c += b;
            b -= a; b ^= Rot(a,  6); a += c;
            c -= b; c ^= Rot(b,  8); b += a;

            a -= c; a ^= Rot(c, 16); c += b;
            b -= a; b ^= Rot(a, 19); a += c;
            c -= b; c ^= Rot(b,  4); b += a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Final(ref UInt32 a, ref UInt32 b, ref UInt32 c)
        {
            c ^= b; c -= Rot(b, 14);
            a ^= c; a -= Rot(c, 11);
            b ^= a; b -= Rot(a, 25);

            c ^= b; c -= Rot(b, 16);
            a ^= c; a -= Rot(c,  4);
            b ^= a; b -= Rot(a, 14);

            c ^= b; c -= Rot(b, 24);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private UInt32 Rot(UInt32 value, int positions)
        {
            return (value << positions) | (value >> (32 - positions));
        }

    }
}
