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
    /// Implementation of Bob Jenkins' One-at-a-Time hash function as specified at http://www.burtleburtle.net/bob/hash/doobs.html (function named "one_at_a_time").
    /// 
    /// This hash function has been superseded by JenkinsLookup2 and JenkinsLookup3.
    /// </summary>
    internal class JenkinsOneAtATime_Implementation
        : HashFunctionAsyncBase,
            IJenkinsOneAtATime
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="JenkinsOneAtATime_Implementation" /> class.
        /// </summary>
        /// <inheritdoc cref="HashFunctionBase(int)" />
        public JenkinsOneAtATime_Implementation()
            : base(32)
        {

        }


        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(IUnifiedData data, CancellationToken cancellationToken)
        {
            UInt32 hash = 0;
            
            data.ForEachRead(
                (dataBytes, position, length) => {
                    ProcessBytes(ref hash, dataBytes, position, length);
                },
                cancellationToken);
            
            hash += hash << 3;
            hash ^= hash >> 11;
            hash += hash << 15;

            return BitConverter.GetBytes(hash);
        }
        
        /// <inheritdoc />
        protected override async Task<byte[]> ComputeHashAsyncInternal(IUnifiedDataAsync data, CancellationToken cancellationToken)
        {
            UInt32 hash = 0;

            await data.ForEachReadAsync(
                    (dataBytes, position, length) => {
                        ProcessBytes(ref hash, dataBytes, position, length);
                    },
                    cancellationToken)
                .ConfigureAwait(false);

            hash += hash << 3;
            hash ^= hash >> 11;
            hash += hash << 15;

            return BitConverter.GetBytes(hash);
        }


        private static void ProcessBytes(ref UInt32 hash, byte[] dataBytes, int position, int length)
        {
            for (var x = position; x < position + length; ++x)
            {
                hash += dataBytes[x];
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }
        }
    }
}
