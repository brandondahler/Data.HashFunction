using System;
using System.Collections.Generic;
using System.Data.HashFunction.Jenkins;
using System.Data.HashFunction.Test._Utilities;
using System.Linq;
using System.Text;

namespace System.Data.HashFunction.Test.Jenkins
{
    public class JenkinsLookup2_Implementation_Tests
    {
        
        public class IHashFunctionAsync_Tests
            : IHashFunctionAsync_TestBase<IJenkinsLookup2>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0xbd49d10d),
                    new KnownValue(32, TestConstants.FooBar, 0x9d3ffa02),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(15), 0x0c39787e),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(19), 0x2a06cf89),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(23), 0xa4fd862c),
                };

            protected override IJenkinsLookup2 CreateHashFunction(int hashSize) =>
                new JenkinsLookup2_Implementation(
                    new JenkinsLookup2Config());
        }
    

        public class IHashFunctionAsync_Tests_WithInitVal
            : IHashFunctionAsync_TestBase<IJenkinsLookup2>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0x6117ff85),
                };

            protected override IJenkinsLookup2 CreateHashFunction(int hashSize) =>
                new JenkinsLookup2_Implementation(
                    new JenkinsLookup2Config() { 
                        Seed = 0x7da236b9U
                    });
        }
    
    }
}
