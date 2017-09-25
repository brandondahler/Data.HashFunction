using System;
using System.Collections.Generic;
using System.Data.HashFunction.FNV;
using System.Text;

namespace System.Data.HashFunction.Test.FNV
{
    public class FNV1_Implementation_Tests
    {

        public class IHashFunctionAsync_Tests
            : IHashFunctionAsync_TestBase<IFNV1>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0x811c9dc5),
                    new KnownValue(32, TestConstants.FooBar, 0x31f0b262),
                    new KnownValue(32, TestConstants.LoremIpsum, 0xe1fb1efb),
                    new KnownValue(64, TestConstants.Empty, 0xcbf29ce484222325),
                    new KnownValue(64, TestConstants.FooBar, 0x340d8765a4dda9c2),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xe1d37010556d091b),
                    new KnownValue(128, TestConstants.Empty, "8dc595627521b8624201bb072e27626c"),
                    new KnownValue(128, TestConstants.FooBar, "aa93c2d25383c56dbf643c9ceabf9678"),
                    new KnownValue(128, TestConstants.LoremIpsum, "130c122234e097c352c819800a56ea75"),
                };

            protected override IFNV1 CreateHashFunction(int hashSize) =>
                new FNV1_Implementation(
                    FNVConfig.GetPredefinedConfig(hashSize));
        }
    }
}
