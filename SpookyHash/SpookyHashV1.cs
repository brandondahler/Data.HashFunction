using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Data.HashFunction.Utilities.IntegerManipulation;
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
        protected override byte[] ComputeHashInternal(Stream data)
        {
            UInt64[] h = new UInt64[12];
            
            h[0]=h[3]=h[6]=h[9]  = InitVal1;
            h[1]=h[4]=h[7]=h[10] = (HashSize == 128 ? InitVal2 : InitVal1);
            h[2]=h[5]=h[8]=h[11] = 0XDEADBEEFDEADBEEF;


            var dataGroups = data.AsGroupedStreamData(96);


            foreach (var dataGroup in dataGroups)
                Mix(dataGroup, 0, h);


            var remainder = dataGroups.Remainder;
            var remainderData = new byte[96];

            Array.Copy(remainder, remainderData, remainder.Length);
            remainderData[95] = (byte) remainder.Length;


            Mix(remainderData, 0, h);
            End(h);


            switch (HashSize)
            {
                case 32:
                    return BitConverter.GetBytes((UInt32) h[0]);
                case 64:
                    return BitConverter.GetBytes(h[0]);

                case 128:
                    var results = new byte[16];
                    BitConverter.GetBytes(h[0]).CopyTo(results, 0);
                    BitConverter.GetBytes(h[1]).CopyTo(results, 8);

                    return results;

                default:
                    throw new ArgumentOutOfRangeException("HashSize");
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Mix(byte[] data, int startIndex, UInt64[] s)
        {
            s[0]  += BitConverter.ToUInt64(data, startIndex);          s[2] ^= s[10];   s[11] ^=  s[0];     s[0] =  s[0].RotateLeft(11);    s[11] +=  s[1];
            s[1]  += BitConverter.ToUInt64(data, startIndex +  8);     s[3] ^= s[11];    s[0] ^=  s[1];     s[1] =  s[1].RotateLeft(32);     s[0] +=  s[2];
            s[2]  += BitConverter.ToUInt64(data, startIndex + 16);     s[4] ^=  s[0];    s[1] ^=  s[2];     s[2] =  s[2].RotateLeft(43);     s[1] +=  s[3];
            s[3]  += BitConverter.ToUInt64(data, startIndex + 24);     s[5] ^=  s[1];    s[2] ^=  s[3];     s[3] =  s[3].RotateLeft(31);     s[2] +=  s[4];
            s[4]  += BitConverter.ToUInt64(data, startIndex + 32);     s[6] ^=  s[2];    s[3] ^=  s[4];     s[4] =  s[4].RotateLeft(17);     s[3] +=  s[5];
            s[5]  += BitConverter.ToUInt64(data, startIndex + 40);     s[7] ^=  s[3];    s[4] ^=  s[5];     s[5] =  s[5].RotateLeft(28);     s[4] +=  s[6];
            s[6]  += BitConverter.ToUInt64(data, startIndex + 48);     s[8] ^=  s[4];    s[5] ^=  s[6];     s[6] =  s[6].RotateLeft(39);     s[5] +=  s[7];
            s[7]  += BitConverter.ToUInt64(data, startIndex + 56);     s[9] ^=  s[5];    s[6] ^=  s[7];     s[7] =  s[7].RotateLeft(57);     s[6] +=  s[8];
            s[8]  += BitConverter.ToUInt64(data, startIndex + 64);    s[10] ^=  s[6];    s[7] ^=  s[8];     s[8] =  s[8].RotateLeft(55);     s[7] +=  s[9];
            s[9]  += BitConverter.ToUInt64(data, startIndex + 72);    s[11] ^=  s[7];    s[8] ^=  s[9];     s[9] =  s[9].RotateLeft(54);     s[8] += s[10];
            s[10] += BitConverter.ToUInt64(data, startIndex + 80);     s[0] ^=  s[8];    s[9] ^= s[10];    s[10] = s[10].RotateLeft(22);     s[9] += s[11];
            s[11] += BitConverter.ToUInt64(data, startIndex + 88);     s[1] ^=  s[9];   s[10] ^= s[11];    s[11] = s[11].RotateLeft(46);    s[10] +=  s[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void EndPartial(UInt64[] h)
        {
            h[11] +=  h[1];     h[2] ^= h[11];     h[1] =  h[1].RotateLeft(44);
            h[0]  +=  h[2];     h[3] ^=  h[0];     h[2] =  h[2].RotateLeft(15);
            h[1]  +=  h[3];     h[4] ^=  h[1];     h[3] =  h[3].RotateLeft(34);
            h[2]  +=  h[4];     h[5] ^=  h[2];     h[4] =  h[4].RotateLeft(21);
            h[3]  +=  h[5];     h[6] ^=  h[3];     h[5] =  h[5].RotateLeft(38);
            h[4]  +=  h[6];     h[7] ^=  h[4];     h[6] =  h[6].RotateLeft(33);
            h[5]  +=  h[7];     h[8] ^=  h[5];     h[7] =  h[7].RotateLeft(10);
            h[6]  +=  h[8];     h[9] ^=  h[6];     h[8] =  h[8].RotateLeft(13);
            h[7]  +=  h[9];    h[10] ^=  h[7];     h[9] =  h[9].RotateLeft(38);
            h[8]  += h[10];    h[11] ^=  h[8];    h[10] = h[10].RotateLeft(53);
            h[9]  += h[11];     h[0] ^=  h[9];    h[11] = h[11].RotateLeft(42);
            h[10] +=  h[0];     h[1] ^= h[10];     h[0] =  h[0].RotateLeft(54);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void End(UInt64[] h)
        {
            EndPartial(h);
            EndPartial(h);
            EndPartial(h);
        }
    }
}
