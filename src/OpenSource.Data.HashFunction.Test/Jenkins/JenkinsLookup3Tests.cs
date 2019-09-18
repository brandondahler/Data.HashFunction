using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Jenkins;
using OpenSource.Data.HashFunction.Test._Utilities;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.Jenkins
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

            var resultBytes = jenkinsLookup3.ComputeHash(knownValue);

            Assert.Equal(
                0x85c64fdU,
                BitConverter.ToUInt32(resultBytes.Hash, 0));
        }
    }
}
