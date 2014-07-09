using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction
{
    public class JenkinsOneAtATime
        : HashFunctionBase
    {
        public override IEnumerable<int> ValidHashSizes { get { return new[] { 32 }; } }
        

        public JenkinsOneAtATime()
            : base(32)
        {

        }


        public override byte[] ComputeHash(byte[] data)
        {
            if (HashSize != 32)
                throw new ArgumentOutOfRangeException("HashSize");

            UInt32 hash = 0;

            foreach (var dataByte in data)
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
