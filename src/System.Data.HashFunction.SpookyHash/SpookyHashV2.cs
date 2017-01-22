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
#if !NET40 || INCLUDE_ASYNC
        : HashFunctionAsyncBase
#else
        : HashFunctionBase
#endif
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
                (dataGroup, position, length) => {
                    Mix(h, dataGroup, position, length);
                },
                (remainder, position, length) => {
                    Array.Copy(remainder, position, remainderData, 0, length);
                    remainderData[95] = (byte) length;
                });


            End(h, remainderData, 0);


            byte[] hash = null;

            switch (HashSize)
            {
                case 32:
                    hash = BitConverter.GetBytes((UInt32) h[0]);
                    break;

                case 64:
                    hash = BitConverter.GetBytes(h[0]);
                    break;

                case 128:
                    hash = new byte[16];

                    BitConverter.GetBytes(h[0])
                        .CopyTo(hash, 0);

                    BitConverter.GetBytes(h[1])
                        .CopyTo(hash, 8);

                    break;

                default:
                    throw new NotImplementedException();
            }

            return hash;
        }

#if !NET40 || INCLUDE_ASYNC
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
                (dataGroup, position, length) => {
                    Mix(h, dataGroup, position, length);
                },
                (remainder, position, length) => {
                    Array.Copy(remainder, position, remainderData, 0, length);
                    remainderData[95] = (byte) length;
                }).ConfigureAwait(false);


            End(h, remainderData, 0);


            byte[] hash = null;

            switch (HashSize)
            {
                case 32:
                    hash = BitConverter.GetBytes((UInt32)h[0]);
                    break;

                case 64:
                    hash = BitConverter.GetBytes(h[0]);
                    break;

                case 128:
                    hash = new byte[16];

                    BitConverter.GetBytes(h[0])
                        .CopyTo(hash, 0);

                    BitConverter.GetBytes(h[1])
                        .CopyTo(hash, 8);

                    break;
        
                default:
                    throw new NotImplementedException();
            }

            return hash;
        }
#endif


#if !NET40
        private static readonly IReadOnlyList<int> _MixRotationParameters = 
#else
        private static readonly IList<int> _MixRotationParameters = 
#endif
            new[] {
                11, 32, 43, 31, 17,28, 39, 57, 55, 54, 22, 46
            };

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static void Mix(UInt64[] h, byte[] data, int position, int length)
        {
            for (int x = position; x < position + length; x += 96)
            {
                for (var i = 0; i < 12; ++i)
                {
                    h[i]             += BitConverter.ToUInt64(data, x + (i * 8)); 
                    h[(i +  2) % 12] ^= h[(i + 10) % 12]; 
                    h[(i + 11) % 12] ^= h[i];
                    h[i]              = h[i].RotateLeft(_MixRotationParameters[i]); 
                    h[(i + 11) % 12] += h[(i + 1) % 12];
                }
            }
        }


#if !NET40
        private static readonly IReadOnlyList<int> _EndPartialRotationParameters = 
#else
        private static readonly IList<int> _EndPartialRotationParameters = 
#endif
            new[] {
                44, 15, 34, 21, 38, 33, 10, 13, 38, 53, 42, 54
            };

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static void EndPartial(UInt64[] h)
        {
            for (int i = 0; i < 12; ++i)
            {
                h[(i + 11) % 12] += h[(i + 1) % 12]; 
                h[(i +  2) % 12] ^= h[(i + 11) % 12]; 
                h[(i +  1) % 12] = h[(i + 1) % 12].RotateLeft(_EndPartialRotationParameters[i]);
            }
        }

#if !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static void End(UInt64[] h, byte[] data, int position)
        {
            for (int i = 0; i < 12; ++i)
            {
                h[i] += BitConverter.ToUInt64(data, position + (i * 8));
            }
             
            EndPartial(h);
            EndPartial(h);
            EndPartial(h);
        }
    }
}
