using System;
using System.Collections.Generic;
using System.Data.HashFunction.Core;
using System.Data.HashFunction.Core.Utilities;
using System.Data.HashFunction.Core.Utilities.UnifiedData;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Jenkins
{
    /// <summary>
    /// Implementation of Bob Jenkins' Lookup2 hash function as specified at http://burtleburtle.net/bob/c/lookup2.c and http://www.burtleburtle.net/bob/hash/doobs.html.
    /// 
    /// This hash function has been superseded by JenkinsLookup3.
    /// </summary>
    internal class JenkinsLookup2_Implementation
        : HashFunctionAsyncBase, 
            IJenkinsLookup2
    {
        public IJenkinsLookup2Config Config => _config.Clone();


        private IJenkinsLookup2Config _config;


        
        /// <summary>
        /// Initializes a new instance of the <see cref="JenkinsLookup2_Implementation"/> class.
        /// </summary>
        /// <param name="config">Configuration</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="config"/></exception>
        /// <inheritdoc cref="HashFunctionBase(int)" />
        public JenkinsLookup2_Implementation(IJenkinsLookup2Config config)
            : base(32)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _config = config.Clone();
        }



        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(IUnifiedData data, CancellationToken cancellationToken)
        {
            UInt32 a = 0x9e3779b9;
            UInt32 b = 0x9e3779b9;
            UInt32 c = _config.Seed;

            int dataCount = 0;

            data.ForEachGroup(12, 
                (dataGroup, position, length) => {
                    ProcessGroup(ref a, ref b, ref c, dataGroup, position, length);

                    dataCount += length;
                }, 
                (remainder, position, length) => {
                    ProcessRemainder(ref a, ref b, ref c, remainder, position, length);

                    dataCount += length;
                },
                cancellationToken);

            c += (UInt32) dataCount;

            Mix(ref a, ref b, ref c);


            return BitConverter.GetBytes(c);
        }
        
        /// <inheritdoc />
        protected override async Task<byte[]> ComputeHashAsyncInternal(IUnifiedDataAsync data, CancellationToken cancellationToken)
        {
            UInt32 a = 0x9e3779b9;
            UInt32 b = 0x9e3779b9;
            UInt32 c = _config.Seed;

            int dataCount = 0;

            await data.ForEachGroupAsync(12, 
                    (dataGroup, position, length) => {
                        ProcessGroup(ref a, ref b, ref c, dataGroup, position, length);

                        dataCount += length;
                    }, 
                    (remainder, position, length) => {
                        ProcessRemainder(ref a, ref b, ref c, remainder, position, length);

                        dataCount += length;
                    },
                    cancellationToken)
                .ConfigureAwait(false);

            c += (UInt32) dataCount;

            Mix(ref a, ref b, ref c);


            return BitConverter.GetBytes(c);
        }


        private static void ProcessGroup(ref UInt32 a, ref UInt32 b, ref UInt32 c, byte[] dataGroup, int position, int length)
        {
            for (var x = position; x < position + length; x += 12)
            {
                a += BitConverter.ToUInt32(dataGroup, x);
                b += BitConverter.ToUInt32(dataGroup, x + 4);
                c += BitConverter.ToUInt32(dataGroup, x + 8);

                Mix(ref a, ref b, ref c);
            }
        }

        private static void ProcessRemainder(ref UInt32 a, ref UInt32 b, ref UInt32 c, byte[] remainder, int position, int length)
        {
            // All the case statements fall through on purpose
            switch (length)
            {
                case 11: c += (UInt32) remainder[position + 10] << 24;  goto case 10;
                case 10: c += (UInt32) remainder[position +  9] << 16;  goto case  9;
                case  9: c += (UInt32) remainder[position +  8] <<  8;  goto case  8;
                // the first byte of c is reserved for the length

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
                case 2: a += (UInt32) remainder[position + 1] <<  8;    goto case 1;
                case 1:
                    a += (UInt32) remainder[position];
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        private static void Mix(ref UInt32 a, ref UInt32 b, ref UInt32 c)
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
