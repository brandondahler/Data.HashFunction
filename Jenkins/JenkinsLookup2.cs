using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    /// <summary>
    /// Implementation of Bob Jenkins' Lookup2 hash function as specified at http://burtleburtle.net/bob/c/lookup2.c and http://www.burtleburtle.net/bob/hash/doobs.html.
    /// 
    /// This hash function has been superseded by JenkinsLookup3.
    /// </summary>
    public class JenkinsLookup2
        : HashFunctionBase
    {
        /// <inheritdoc/>
        public override IEnumerable<int> ValidHashSizes { get { return new[] { 32 }; } }

        /// <summary>
        /// Seed value for hash calculation.
        /// </summary>
        public UInt32 InitVal { get; set; }
        

        /// <summary>
        /// Constructs new <see cref="JenkinsLookup2"/> instance.
        /// </summary>
        public JenkinsLookup2()
            : base(32)
        {
            InitVal = 0;
        }


        /// <inheritdoc/>
        public override byte[] ComputeHash(byte[] data)
        {
            if (HashSize != 32)
                throw new ArgumentOutOfRangeException("HashSize");

            UInt32 a = 0x9e3779b9;
            UInt32 b = 0x9e3779b9;
            UInt32 c = InitVal;

            for (int x = 0; x < data.Length / 12; ++x)
            {
                a += BitConverter.ToUInt32(data, (x * 12) + 0);
                b += BitConverter.ToUInt32(data, (x * 12) + 4);
                c += BitConverter.ToUInt32(data, (x * 12) + 8);

                Mix(ref a, ref b, ref c);
            }

            c += (UInt32) data.Length;

            var remainderStartIndex = data.Length - (data.Length % 12);

            // All the case statements fall through on purpose
            switch (data.Length % 12)
            {
                case 11: c += (UInt32) data[remainderStartIndex + 10] << 24;    goto case 10;
                case 10: c += (UInt32) data[remainderStartIndex +  9] << 16;    goto case  9;
                case  9: c += (UInt32) data[remainderStartIndex +  8] <<  8;    goto case  8;
                // the first byte of c is reserved for the length

                case 8:
                    b += BitConverter.ToUInt32(data, remainderStartIndex + 4);
                    goto case 4;

                case 7: b += (UInt32) data[remainderStartIndex +  6] << 16; goto case 6;
                case 6: b += (UInt32) data[remainderStartIndex +  5] <<  8; goto case 5;
                case 5: b += (UInt32) data[remainderStartIndex +  4];       goto case 4;

                case 4:
                    a += BitConverter.ToUInt32(data, remainderStartIndex); 
                    break;

                case  3: a += (UInt32) data[remainderStartIndex + 2] << 16; goto case  2;
                case  2: a += (UInt32) data[remainderStartIndex + 1] <<  8; goto case  1;
                case  1: 
                    a += (UInt32) data[remainderStartIndex];         
                    break;

                case 0:
                    break;
            }

            Mix(ref a, ref b, ref c);

            return BitConverter.GetBytes(c);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Mix(ref UInt32 a, ref UInt32 b, ref UInt32 c)
        {
            a -= b; a -= c; a ^= (c >> 13);
            b -= c; b -= a; b ^= (a << 8);
            c -= a; c -= b; c ^= (b >> 13);

            a -= b; a -= c; a ^= (c >> 12);
            b -= c; b -= a; b ^= (a << 16);
            c -= a; c -= b; c ^= (b >> 5);

            a -= b; a -= c; a ^= (c >> 3);
            b -= c; b -= a; b ^= (a << 10);
            c -= a; c -= b; c ^= (b >> 15);
        }
    }
}
