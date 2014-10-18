using System;
using System.Collections.Generic;
using System.Data.HashFunction.Test;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var st = new SpeedTestOverride();

            
            st.IHashFunction_SpeedTest_MultipleItems_ComputeHash_Stream_NonSeekable();
            st.TestFunc();
        }
    }

    class SpeedTestOverride
        : IHashFunction_SpeedTests_HashFunctionBase
    {
        protected override IReadOnlyDictionary<string, System.Data.HashFunction.IHashFunction> TestHashFunctions
        {
            get
            {
                return _TestHashFunctions;
            }
        }

        private readonly IReadOnlyDictionary<string, System.Data.HashFunction.IHashFunction> _TestHashFunctions;

        
        public SpeedTestOverride()
        {
            _TestHashFunctions = base.TestHashFunctions
                .Where(kvp => kvp.Key.StartsWith("CityHash"))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);


        }

        public void TestFunc()
        {
            IHashFunction_SpeedTest_MultipleItems((sw, testHashFunction, count, testBytes) =>
            {
                

                using (var ms = new TestStream(testBytes))
                {
                    sw.Start();

                    for (int x = 0; x < count; ++x)
                    {
                        testHashFunction.ComputeHash(ms);

                        ms.Seek(0, SeekOrigin.Begin);
                    }

                    sw.Stop();
                }

                return true;
            });
        }

        private class TestStream 
            : MemoryStream
        {
            public override bool CanSeek
            {
                get
                {
                    return false;
                }
            }

            public override long Length
            {
                get
                {
                    throw new NotSupportedException("Seeking not supported.  Hash function likely needs RequiresSeekableStream override.");
                }
            }

            public TestStream(byte[] data)
                : base(data)
            { 

}
        }
    }
}
