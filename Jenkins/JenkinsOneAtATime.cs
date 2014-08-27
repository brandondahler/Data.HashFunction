using System;
using System.Collections.Generic;
using System.Data.HashFunction.Utilities;
using System.Data.HashFunction.Utilities.IntegerManipulation;
using System.IO;
using System.Linq;
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
        : HashFunctionBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="JenkinsOneAtATime" /> class.
        /// </summary>
        /// <inheritdoc cref="HashFunctionBase(int)" />
        public JenkinsOneAtATime()
            : base(32)
        {

        }


        /// <exception cref="System.InvalidOperationException">HashSize set to an invalid value.</exception>
        /// <inheritdoc />
        protected override byte[] ComputeHashInternal(Stream data)
        {
            if (HashSize != 32)
                throw new InvalidOperationException("HashSize set to an invalid value.");


            UInt32 hash = 0;
            
            foreach (var dataByte in data.AsEnumerable())
            {
                hash += dataByte;
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }
            
            hash += hash << 3;
            hash ^= hash >> 11;
            hash += hash << 15;

            return BitConverter.GetBytes(hash);
        }
    }
}
