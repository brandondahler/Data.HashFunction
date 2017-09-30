using System;
using System.Collections.Generic;
using System.Data.HashFunction.Jenkins;
using System.Data.HashFunction.Test._Utilities;
using System.Linq;
using System.Text;

namespace System.Data.HashFunction.Test.Jenkins
{
    public class JenkinsLookup3_Implementation_Tests
    {
        
        public class IHashFunctionAsync_Tests
            : IHashFunctionAsync_TestBase<IJenkinsLookup3>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0xaeb72b0c),
                    new KnownValue(64, TestConstants.FooBar, 0xacf2fd4caeb72b0c),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(12), 0x8e663aee),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(12), 0xb6d69c8c8e663aee),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(15), 0x9b484ed5),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(15), 0x14b71e4d9b484ed5),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(19), 0xad9cba6f),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(19), 0x2807920ead9cba6f),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(23), 0x57c46621),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(23), 0x01e860f357c46621),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(24), 0xf2b662ef),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(24), 0xcad6d781f2b662ef),
                };

            protected override IJenkinsLookup3 CreateHashFunction(int hashSize) =>
                new JenkinsLookup3_Implementation(
                    new JenkinsLookup3Config() { 
                        HashSizeInBits = hashSize
                    });
        }
    

        public class IHashFunctionAsync_Tests_DefaultConstructor
            : IHashFunctionAsync_TestBase<IJenkinsLookup3>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0xaeb72b0c),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(12), 0x8e663aee),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(15), 0x9b484ed5),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(19), 0xad9cba6f),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(23), 0x57c46621),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(24), 0xf2b662ef),
                };

            protected override IJenkinsLookup3 CreateHashFunction(int hashSize) =>
                new JenkinsLookup3_Implementation(
                    new JenkinsLookup3Config());
        }
    

        public class IHashFunctionAsync_Tests_WithInitVals
            : IHashFunctionAsync_TestBase<IJenkinsLookup3>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0x7306b2fc),
                    new KnownValue(64, TestConstants.FooBar, 0x28b9e020444d6de2),
                };

            protected override IJenkinsLookup3 CreateHashFunction(int hashSize) =>
                new JenkinsLookup3_Implementation(
                    new JenkinsLookup3Config()
                    {
                        HashSizeInBits = hashSize,
                        Seed = 0x7da236b9U,
                        Seed2 = 0x87930b75U
                    });
        }
    

        public class IHashFunctionAsync_Tests_WithInitVals_DefaultHashSize
            : IHashFunctionAsync_TestBase<IJenkinsLookup3>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0x7306b2fc),
                };

            protected override IJenkinsLookup3 CreateHashFunction(int hashSize) =>
                new JenkinsLookup3_Implementation(
                    new JenkinsLookup3Config() {
                        Seed = 0x7da236b9U,
                        Seed2 = 0x87930b75U
                    });
        }
    
    }
}
