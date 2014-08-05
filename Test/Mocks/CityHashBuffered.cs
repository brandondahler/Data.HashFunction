using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Test.Mocks
{
    public class CityHashBuffered 
        : CityHash
    {
        public CityHashBuffered()
            : base()
        {

        }

        public CityHashBuffered(int hashSize)
            : base(hashSize)
        {

        }

        protected override byte[] ComputeHashInternal(Stream data)
        {
            using (var bs = new BufferedStream(data))
                return base.ComputeHashInternal(bs);
        }
    }
}
