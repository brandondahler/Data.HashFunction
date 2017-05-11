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
    /// Implementation of Bob Jenkins' One-at-a-Time hash function as specified at http://www.burtleburtle.net/bob/hash/doobs.html (function named "one_at_a_time").
    /// 
    /// This hash function has been superseded by JenkinsLookup2 and JenkinsLookup3.
    /// </summary>
    public class JenkinsOneAtATime
        : HashFunctionAsyncBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="JenkinsOneAtATime" /> class.
        /// </summary>
        /// <inheritdoc cref="HashFunctionBase(int)" />
        public JenkinsOneAtATime()
            : base(32)
        {

        }


        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(UnifiedData data)
        {
            UInt32 hash = 0;
            
            data.ForEachRead((dataBytes, position, length) => {
                ProcessBytes(ref hash, dataBytes, position, length);
            });
            
            hash += hash << 3;
            hash ^= hash >> 11;
            hash += hash << 15;

            return BitConverter.GetBytes(hash);
        }
        
        /// <inheritdoc />
        protected override async Task<byte[]> ComputeHashAsyncInternal(UnifiedData data)
        {
            UInt32 hash = 0;

            await data.ForEachReadAsync((dataBytes, position, length) => {
                ProcessBytes(ref hash, dataBytes, position, length);
            }).ConfigureAwait(false);

            hash += hash << 3;
            hash ^= hash >> 11;
            hash += hash << 15;

            return BitConverter.GetBytes(hash);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
