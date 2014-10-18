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
    /// Implements SpookyHash V2 as specified at http://burtleburtle.net/bob/hash/spooky.html.
    /// </summary>
    public class SpookyHashV2
        : HashFunctionAsyncBase
    {
        /// <summary>
        /// First seed value for hash calculation.
        /// </summary>
        /// <value>
        /// The first seed value for hash calculation.
        /// </value>
        public UInt64 InitVal1 { get { return _InitVal1; } }

        /// <summary>
        /// Second seed value for hash calculation.
        /// </summary>
        /// <value>
        /// The second seed value for hash calculation.
        /// </value>
        /// <remarks>
        /// Not used for 32-bit and 64-bit modes, used as second seed for 128-bit mode.
        /// </remarks>
        public UInt64 InitVal2 { get { return _InitVal2; } }


        /// <summary>
        /// The list of possible hash sizes that can be provided to the <see cref="SpookyHashV2" /> constructor.
        /// </summary>
        /// <value>
        /// The list of valid hash sizes.
        /// </value>
        public static IEnumerable<int> ValidHashSizes { get { return _validHashSizes; } }



        private readonly UInt64 _InitVal1;
        private readonly UInt64 _InitVal2;

        private static readonly IEnumerable<int> _validHashSizes = new[] { 32, 64, 128 };



        /// <remarks>
        /// Defaults <see cref="InitVal1" /> to 0. <inheritdoc cref="SpookyHashV2(UInt64)" />
        /// </remarks>
        /// <inheritdoc cref="SpookyHashV2(UInt64)" />
        public SpookyHashV2()
            : this(0U)
        {

        }

        /// <remarks>
        /// Defaults <see cref="InitVal1" /> to 0. <inheritdoc cref="SpookyHashV2(int, UInt64)" />
        /// </remarks>
        /// <inheritdoc cref="SpookyHashV2(int, UInt64)" />
        public SpookyHashV2(int hashSize)
            : this(hashSize, 0U)
        {

        }

        /// <remarks>
        /// Defaults <see cref="InitVal2" /> to 0. <inheritdoc cref="SpookyHashV2(UInt64, UInt64)" />
        /// </remarks>
        /// <inheritdoc cref="SpookyHashV2(UInt64, UInt64)" />
        public SpookyHashV2(UInt64 initVal1)
            : this(initVal1, 0U)
        { 
        
        }

        /// <remarks>
        /// Defaults <see cref="HashFunctionBase.HashSize" /> to 128.
        /// </remarks>
        /// <inheritdoc cref="SpookyHashV2(int, UInt64, UInt64)" />
        public SpookyHashV2(UInt64 initVal1, UInt64 initVal2)
            : this(128, initVal1, initVal2)
        {

        }

        /// <remarks>
        /// Defaults <see cref="InitVal2" /> to 0.
        /// </remarks>
        /// <inheritdoc cref="SpookyHashV2(int, UInt64, UInt64)" />
        public SpookyHashV2(int hashSize, UInt64 initVal1)
            : this(hashSize, initVal1, 0U)
        {

        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SpookyHashV2"/> class.
        /// </summary>
        /// <param name="hashSize"><inheritdoc cref="HashFunctionBase(int)" /></param>
        /// <param name="initVal1"><inheritdoc cref="InitVal1" /></param>
        /// <param name="initVal2"><inheritdoc cref="InitVal2" /></param>
        /// <exception cref="System.ArgumentOutOfRangeException">hashSize;hashSize must be contained within SpookyHashV2.ValidHashSizes.</exception>
        /// <inheritdoc cref="HashFunctionBase(int)" />
        public SpookyHashV2(int hashSize, UInt64 initVal1, UInt64 initVal2)
            : base(hashSize)
        {
            if (!ValidHashSizes.Contains(hashSize))
                throw new ArgumentOutOfRangeException("hashSize", "hashSize must be contained within SpookyHashV2.ValidHashSizes.");

            _InitVal1 = initVal1;
            _InitVal2 = initVal2;
        }



        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(UnifiedData data)
        {
            UInt64[] h = new UInt64[12];

            h[0] = h[3] = h[6] = h[9] = InitVal1;
            h[1] = h[4] = h[7] = h[10] = (HashSize == 128 ? InitVal2 : InitVal1);
            h[2] = h[5] = h[8] = h[11] = 0XDEADBEEFDEADBEEF;


            var remainderData = new byte[96];

            data.ForEachGroup(96, 
                dataGroup => {
                    Mix(dataGroup, 0, h);
                },
                remainder => {
                    remainder.CopyTo(remainderData, 0);
                    remainderData[95] = (byte) remainder.Length;
                });


            End(remainderData, 0, h);

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
                    throw new InvalidOperationException("HashSize set to an invalid value.");
            }
        }

        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override async Task<byte[]> ComputeHashAsyncInternal(UnifiedData data)
        {
            UInt64[] h = new UInt64[12];

            h[0] = h[3] = h[6] = h[9] = InitVal1;
            h[1] = h[4] = h[7] = h[10] = (HashSize == 128 ? InitVal2 : InitVal1);
            h[2] = h[5] = h[8] = h[11] = 0XDEADBEEFDEADBEEF;


            var remainderData = new byte[96];

            await data.ForEachGroupAsync(96, 
                dataGroup => {
                    Mix(dataGroup, 0, h);
                },
                remainder => {
                    remainder.CopyTo(remainderData, 0);
                    remainderData[95] = (byte) remainder.Length;
                }).ConfigureAwait(false);


            End(remainderData, 0, h);

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
                    throw new InvalidOperationException("HashSize set to an invalid value.");
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Mix(byte[] data, int startIndex, UInt64[] s)
        {

            s[0]  += BitConverter.ToUInt64(data, startIndex + ( 0 * 8));     s[2] ^= s[10];   s[11] ^= s[0];      s[0] =  s[0].RotateLeft(11);     s[11] += s[1];
            s[1]  += BitConverter.ToUInt64(data, startIndex + ( 1 * 8));     s[3] ^= s[11];    s[0] ^=  s[1];     s[1] =  s[1].RotateLeft(32);     s[0] +=  s[2];
            s[2]  += BitConverter.ToUInt64(data, startIndex + ( 2 * 8));     s[4] ^=  s[0];    s[1] ^=  s[2];     s[2] =  s[2].RotateLeft(43);     s[1] +=  s[3];
            s[3]  += BitConverter.ToUInt64(data, startIndex + ( 3 * 8));     s[5] ^=  s[1];    s[2] ^=  s[3];     s[3] =  s[3].RotateLeft(31);     s[2] +=  s[4];
            s[4]  += BitConverter.ToUInt64(data, startIndex + ( 4 * 8));     s[6] ^=  s[2];    s[3] ^=  s[4];     s[4] =  s[4].RotateLeft(17);     s[3] +=  s[5];
            s[5]  += BitConverter.ToUInt64(data, startIndex + ( 5 * 8));     s[7] ^=  s[3];    s[4] ^=  s[5];     s[5] =  s[5].RotateLeft(28);     s[4] +=  s[6];
            s[6]  += BitConverter.ToUInt64(data, startIndex + ( 6 * 8));     s[8] ^=  s[4];    s[5] ^=  s[6];     s[6] =  s[6].RotateLeft(39);     s[5] +=  s[7];
            s[7]  += BitConverter.ToUInt64(data, startIndex + ( 7 * 8));     s[9] ^=  s[5];    s[6] ^=  s[7];     s[7] =  s[7].RotateLeft(57);     s[6] +=  s[8];
            s[8]  += BitConverter.ToUInt64(data, startIndex + ( 8 * 8));    s[10] ^=  s[6];    s[7] ^=  s[8];     s[8] =  s[8].RotateLeft(55);     s[7] +=  s[9];
            s[9]  += BitConverter.ToUInt64(data, startIndex + ( 9 * 8));    s[11] ^=  s[7];    s[8] ^=  s[9];     s[9] =  s[9].RotateLeft(54);     s[8] += s[10];
            s[10] += BitConverter.ToUInt64(data, startIndex + (10 * 8));     s[0] ^=  s[8];    s[9] ^= s[10];    s[10] = s[10].RotateLeft(22);     s[9] += s[11];
            s[11] += BitConverter.ToUInt64(data, startIndex + (11 * 8));     s[1] ^=  s[9];   s[10] ^= s[11];    s[11] = s[11].RotateLeft(46);    s[10] +=  s[0];
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
        private static void End(byte[] data, int startIndex, UInt64[] h)
        {
            h[0]  += BitConverter.ToUInt64(data, startIndex + (0  * 8));
            h[1]  += BitConverter.ToUInt64(data, startIndex + (1  * 8));
            h[2]  += BitConverter.ToUInt64(data, startIndex + (2  * 8));
            h[3]  += BitConverter.ToUInt64(data, startIndex + (3  * 8));
            h[4]  += BitConverter.ToUInt64(data, startIndex + (4  * 8));
            h[5]  += BitConverter.ToUInt64(data, startIndex + (5  * 8));
            h[6]  += BitConverter.ToUInt64(data, startIndex + (6  * 8));
            h[7]  += BitConverter.ToUInt64(data, startIndex + (7  * 8));
            h[8]  += BitConverter.ToUInt64(data, startIndex + (8  * 8));
            h[9]  += BitConverter.ToUInt64(data, startIndex + (9  * 8));
            h[10] += BitConverter.ToUInt64(data, startIndex + (10 * 8));
            h[11] += BitConverter.ToUInt64(data, startIndex + (11 * 8));
             
            EndPartial(h);
            EndPartial(h);
            EndPartial(h);
        }
    }
}
