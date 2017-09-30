using System;
using System.Collections.Generic;
using System.Data.HashFunction.Core.Utilities.UnifiedData;
using System.Data.HashFunction.Jenkins;
using System.Data.HashFunction.Test._Utilities;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.Jenkins
{
    public class JenkinsLookup3Tests
    {
        [Fact]
        public void JenkinsLookup3_32bit_ComputeHash_ExtremelyLongStream_Works()
        {
            byte[] knownValue;

            {
                var loremIpsumRepeatCount = 800;
                var loremIpsumLength = TestConstants.LoremIpsum.Length;


                knownValue = new byte[loremIpsumLength * loremIpsumRepeatCount];

                for (var x = 0; x < loremIpsumRepeatCount; ++x)
                    Array.Copy(TestConstants.LoremIpsum, 0, knownValue, loremIpsumLength * x, loremIpsumLength);
            }


            var jenkinsLookup3 = new JenkinsLookup3_Implementation(
                new JenkinsLookup3Config() { 
                    HashSizeInBits = 32 
                });

            using (var ms = new MemoryStream(knownValue))
            {
                var resultBytes = jenkinsLookup3.ComputeHash(ms);

                Assert.Equal(
                    0x85c64fdU,
                    BitConverter.ToUInt32(resultBytes.Hash, 0));
            }
        }
    }
}
