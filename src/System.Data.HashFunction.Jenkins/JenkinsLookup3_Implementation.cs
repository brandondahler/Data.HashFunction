using System;
using System.Collections.Generic;
using System.Data.HashFunction.Core;
using System.Data.HashFunction.Core.Utilities;
using System.Data.HashFunction.Core.Utilities.UnifiedData;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Jenkins
{
    /// <summary>
    /// Implementation of Bob Jenkins' Lookup3 hash function as specified at http://burtleburtle.net/bob/c/lookup3.c.
    /// </summary>
    internal class JenkinsLookup3_Implementation
        : HashFunctionAsyncBase,
            IJenkinsLookup3
    {

        public IJenkinsLookup3Config Config => _config.Clone();

        private readonly IJenkinsLookup3Config _config;

        private static readonly IEnumerable<int> _validHashSizes = new HashSet<int>() { 32, 64 };

        
        /// <summary>
        /// Initializes a new instance of the <see cref="JenkinsLookup3_Implementation"/> class.
        /// </summary>
        /// <param name="hashSize"><inheritdoc cref="HashFunctionBase(int)"/></param>
        /// <param name="initVal1"><inheritdoc cref="InitVal1"/></param>
        /// <param name="initVal2"><inheritdoc cref="InitVal2"/></param>
        /// <exception cref="System.ArgumentOutOfRangeException">hashSize;hashSize must be contained within SpookyHashV2.ValidHashSizes.</exception>
        /// <inheritdoc cref="HashFunctionBase(int)"/>
        public JenkinsLookup3_Implementation(IJenkinsLookup3Config config)
            : base(config.HashSizeInBits)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Clone();


            if (!_validHashSizes.Contains(_config.HashSizeInBits))
                throw new ArgumentOutOfRangeException($"{nameof(config)}.{nameof(config.HashSizeInBits)}", $"{nameof(config)}.{nameof(config.HashSizeInBits)} must be contained within JenkinsLookup3.ValidHashSizes.");
        }



        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(IUnifiedData data, CancellationToken cancellationToken)
        {
            UInt32 a = 0xdeadbeef + (UInt32) data.Length + _config.Seed;
            UInt32 b = a;
            UInt32 c = a;

            if (_config.HashSizeInBits == 64)
                c += _config.Seed2;


            int dataCount = 0;

            data.ForEachGroup(12, 
                (dataGroup, position, length) => {
                    ProcessGroup(ref a, ref b, ref c, ref dataCount, dataGroup, position, length);
                },
                (remainder, position, length) => {
                    ProcessRemainder(ref a, ref b, ref c, ref dataCount, remainder, position, length);
                },
                cancellationToken);
    
            if (dataCount > 0)
                Final(ref a, ref b, ref c);


            byte[] hash;

            switch (_config.HashSizeInBits)
            {
                case 32:
                    hash = BitConverter.GetBytes(c);
                    break;

                case 64:
                    hash = BitConverter.GetBytes((((UInt64) b) << 32) | c);
                    break;

                default:
                    throw new NotImplementedException();
            }

            return hash;
        }

        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override async Task<byte[]> ComputeHashAsyncInternal(IUnifiedDataAsync data, CancellationToken cancellationToken)
        {
            UInt32 a = 0xdeadbeef + (UInt32) data.Length + _config.Seed;
            UInt32 b = a;
            UInt32 c = a;

            if (_config.HashSizeInBits == 64)
                c += _config.Seed2;


            int dataCount = 0;

            await data.ForEachGroupAsync(12, 
                    (dataGroup, position, length) => {
                        ProcessGroup(ref a, ref b, ref c, ref dataCount, dataGroup, position, length);
                    },
                    (remainder, position, length) => {
                        ProcessRemainder(ref a, ref b, ref c, ref dataCount, remainder, position, length);
                    },
                    cancellationToken)
                .ConfigureAwait(false);
    
            if (dataCount > 0)
                Final(ref a, ref b, ref c);


            byte[] hash;

            switch (_config.HashSizeInBits)
            {
                case 32:
                    hash = BitConverter.GetBytes(c);
                    break;

                case 64:
                    hash = BitConverter.GetBytes((((UInt64) b) << 32) | c);
                    break;
        
                default:
                    throw new NotImplementedException();
            }

            return hash;
        }


        private void ProcessGroup(ref UInt32 a, ref UInt32 b, ref UInt32 c, ref int dataCount, byte[] dataGroup, int position, int length)
        {
            for (int x = position; x < position + length; x += 12)
            {
                // Mix at beginning of subsequent group to handle special case of length <= 12
                if (dataCount > 0 || x > position)
                    Mix(ref a, ref b, ref c);

                a += BitConverter.ToUInt32(dataGroup, x + 0);
                b += BitConverter.ToUInt32(dataGroup, x + 4);
                c += BitConverter.ToUInt32(dataGroup, x + 8);
            }

            dataCount += length;
        }

        private void ProcessRemainder(ref UInt32 a, ref UInt32 b, ref UInt32 c, ref int dataCount, byte[] remainder, int position, int length)
        {
            // Mix at beginning of subsequent group to handle special case of length <= 12
            if (dataCount > 0)
                Mix(ref a, ref b, ref c);

            switch (length)
            {
                case 11: c += (UInt32) remainder[position + 10] << 16;  goto case 10;
                case 10: c += (UInt32) remainder[position +  9] <<  8;  goto case 9;
                case  9: c += (UInt32) remainder[position +  8];        goto case 8;

                case 8:
                    b += BitConverter.ToUInt32(remainder, position + 4);
                    goto case 4;

                case 7: b += (UInt32) remainder[position + 6] << 16;    goto case 6;
                case 6: b += (UInt32) remainder[position + 5] <<  8;    goto case 5;
                case 5: b += (UInt32) remainder[position + 4];          goto case 4;

                case 4:
                    a += BitConverter.ToUInt32(remainder, position);
                    break;

                case 3: a += (UInt32) remainder[position + 2] << 16;    goto case 2;
                case 2: a += (UInt32) remainder[position + 1] << 8;     goto case 1;
                case 1: 
                    a += (UInt32) remainder[position]; 
                    break;
                    
                default:
                    throw new NotImplementedException();
            }

            dataCount += length;
        }


        private void Mix(ref UInt32 a, ref UInt32 b, ref UInt32 c)
        {
            a -= c; a ^= RotateLeft(c, 4); c += b;
            b -= a; b ^= RotateLeft(a,  6); a += c;
            c -= b; c ^= RotateLeft(b,  8); b += a;

            a -= c; a ^= RotateLeft(c, 16); c += b;
            b -= a; b ^= RotateLeft(a, 19); a += c;
            c -= b; c ^= RotateLeft(b,  4); b += a;
        }

        private void Final(ref UInt32 a, ref UInt32 b, ref UInt32 c)
        {
            c ^= b; c -= RotateLeft(b, 14);
            a ^= c; a -= RotateLeft(c, 11);
            b ^= a; b -= RotateLeft(a, 25);

            c ^= b; c -= RotateLeft(b, 16);
            a ^= c; a -= RotateLeft(c,  4);
            b ^= a; b -= RotateLeft(a, 14);

            c ^= b; c -= RotateLeft(b, 24);
        }


        private static UInt32 RotateLeft(UInt32 operand, int shiftCount)
        {
            shiftCount &= 0x1f;

            return
                (operand << shiftCount) |
                (operand >> (32 - shiftCount));
        }
    }
}
