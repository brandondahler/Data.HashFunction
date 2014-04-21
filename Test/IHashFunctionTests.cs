using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test
{
    internal sealed class TestConstants
    {
        // Constant values avaliable for KnownValues to use.
        public static readonly byte[] Empty = new byte[0];
        public static readonly byte[] FooBar = "foobar".ToBytes();

        public static readonly byte[] LoremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.  Ut ornare aliquam mauris, at volutpat massa.  Phasellus pulvinar purus eu venenatis commodo.".ToBytes();
        
        public static readonly byte[] RandomShort = "55d0e01ec669dc69".HexToBytes();
        public static readonly byte[] RandomLong = "1122eeba86d52989b26b0efd2be8d091d3ad307b771ff8d1208104f9aa40b12ab057a0d78656ba037e475178c159bf3ee64dcd279610d64bb7888a97211884c7a894378263135124720ef6ef560da6c85fb491cb732b331e89bcb00e7daef271e127483e91b189ceeaf2f6711394e2eca07fb4db62c5a8fd8195ae3b39da63".HexToBytes();
    }

    public abstract class IHashFunctionTests<IHashFunctionT>
        where IHashFunctionT : IHashFunction, new()
    {
        protected class KnownValue
        {
            public int HashSize;
            public byte[] TestValue;
            public string HashHex;

            public KnownValue(int size, IEnumerable<byte> value, string hash)
            {
                HashSize = size;
                TestValue = value.ToArray();
                HashHex = hash;
            }
        }

        protected abstract IEnumerable<KnownValue> KnownValues { get; }

        protected abstract long SpeedTestCount { get; }
        
        private const double SpeedTestCountMultiplier = 1.0;

        [Fact]
        public void IHashFunction_Manipulate_HashSizes()
        {
            var hf = new IHashFunctionT();

            Assert.NotNull(hf.ValidHashSizes);

            Assert.Equal(hf.ValidHashSizes.Take(10000), hf.ValidHashSizes.Take(10000).Distinct());


            var validHashSizes = hf.ValidHashSizes.Take(10000);

            Assert.NotEqual(0, validHashSizes.Count());

            if (validHashSizes.Count() == 10000)
                validHashSizes = Enumerable.Range(0, 31).Select(x => 1 << x).ToArray();

            foreach (var hashSize in validHashSizes)
            {
                Assert.DoesNotThrow(() => {
                    hf.HashSize = hashSize;
                });

                Assert.Equal(hashSize, hf.HashSize);
            }
        }

        [Fact]
        public void IHashFunction_Computes_KnownValues()
        {
            var hf = new IHashFunctionT();

            foreach (var knownValue in KnownValues)
            {
                hf.HashSize = knownValue.HashSize;

                var hashResults = hf.ComputeHash(knownValue.TestValue);

                Assert.Equal(knownValue.HashHex.HexToBytes(), hashResults);
            }
        }

        [Fact(Skip = "SpeedTest is for relative benchmarking only.")]
        public void IHashFunction_SpeedTest_ByteArray()
        {
            var hf = new IHashFunctionT();

            var kvBytes = KnownValues
                .Select(kv => kv.TestValue)
                .ToArray();

            foreach (var hashSize in hf.ValidHashSizes)
            {
                var sw = new Stopwatch();
                hf.HashSize = hashSize;
                
                long finalSpeedTestCount = (long) Math.Round(SpeedTestCount * SpeedTestCountMultiplier);

                sw.Start();

                Parallel.For(0, finalSpeedTestCount, (x) => {
                    hf.ComputeHash(kvBytes[x % kvBytes.Length]);
                });

                sw.Stop();

                Console.WriteLine("{0} bits - {1} hashes/sec ({2} in {3}ms)", 
                    hashSize,
                    (finalSpeedTestCount / (sw.ElapsedMilliseconds / 1000.0d)).ToString("N3"),
                    finalSpeedTestCount, 
                    sw.ElapsedMilliseconds);
            }
        }
    }


    #region Concrete Tests

    #region Data.HashFunction.HashFunctionBase

    public class IHashFunctionTests_HashAlgorithmWrapper_SHA1
        : IHashFunctionTests<HashAlgorithmWrapper<SHA1Managed>>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new[] {
                    new KnownValue(160, TestConstants.Empty,        "da39a3ee5e6b4b0d3255bfef95601890afd80709"),
                    new KnownValue(160, TestConstants.FooBar,       "8843d7f92416211de9ebb963ff4ce28125932878"),
                    new KnownValue(160, TestConstants.LoremIpsum,   "2dd4010f15f21c9e26e31a693ba31c6ab78a5a4c" ),
                    new KnownValue(160, TestConstants.RandomShort,  "d64df40c72068b01e7dfb5ceb2b519ad3b483eb0" ),
                    new KnownValue(160, TestConstants.RandomLong,   "e5901cb4679133729c5555210c3cfe3e5851a2aa" )
                };
            }
        }

        protected override long SpeedTestCount { get { return 100000; } }
    }

    public class IHashFunctionTests_HashAlgorithmWrapper_SHA256
        : IHashFunctionTests<HashAlgorithmWrapper<SHA256Managed>>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new[] {
                    new KnownValue(256, TestConstants.FooBar,        "C3AB8FF13720E8AD9047DD39466B3C8974E592C2FA383D4A3960714CAEF0C4F2"),
                };
            }
        }

        protected override long SpeedTestCount { get { return 100000; } }
    }

    public class IHashFunctionTests_HashAlgorithmWrapper_SHA384
        : IHashFunctionTests<HashAlgorithmWrapper<SHA384Managed>>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new[] {
                    new KnownValue(384, TestConstants.FooBar,        "3C9C30D9F665E74D515C842960D4A451C83A0125FD3DE7392D7B37231AF10C72EA58AEDFCDF89A5765BF902AF93ECF06")
                };
            }
        }

        protected override long SpeedTestCount { get { return 100000; } }
    }

    public class IHashFunctionTests_HashAlgorithmWrapper_SHA512
        : IHashFunctionTests<HashAlgorithmWrapper<SHA512Managed>>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new[] {
                    new KnownValue(512, TestConstants.FooBar,       "0A50261EBD1A390FED2BF326F2673C145582A6342D523204973D0219337F81616A8069B012587CF5635F6925F1B56C360230C19B273500EE013E030601BF2425")
                };
            }
        }

        protected override long SpeedTestCount { get { return 100000; } }
    }

    public class IHashFunctionTests_HashAlgorithmWrapper_MD5
        : IHashFunctionTests<HashAlgorithmWrapper<MD5CryptoServiceProvider>>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new[] {
                    new KnownValue(128, TestConstants.FooBar,       "3858F62230AC3C915F300C664312C63F")
                };
            }
        }

        protected override long SpeedTestCount { get { return 100000; } }
    }

    public class IHashFunctionTests_HashAlgorithmWrapper_RIPEMD160
        : IHashFunctionTests<HashAlgorithmWrapper<RIPEMD160Managed>>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new[] {
                    new KnownValue(160, TestConstants.FooBar,       "A06E327EA7388C18E4740E350ED4E60F2E04FC41")
                };
            }
        }

        protected override long SpeedTestCount { get { return 100000; } }
    }

    #endregion

    #region Data.HashFunction.Jenkins

    public class IHashFunctionTests_JenkinsOneAtATime
        : IHashFunctionTests<JenkinsOneAtATime>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new[] {
                    new KnownValue(32, TestConstants.FooBar, "e7fd52f9")
                };
            }
        }

        protected override long SpeedTestCount { get { return 20000000; } }
    }
        
        
    public class IHashFunctionTests_JenkinsLookup2
        : IHashFunctionTests<JenkinsLookup2>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new[] {
                    new KnownValue(32, TestConstants.FooBar, "02fa3f9d"),

                    new KnownValue(32, TestConstants.LoremIpsum.Take(15), "7e78390c"),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(19), "89cf062a"),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(23), "2c86fda4")
                };
            }
        }

        protected override long SpeedTestCount { get { return 10000000; } }
    }

    public class IHashFunctionTests_JenkinsLookup2_WithInitVal
        : IHashFunctionTests<IHashFunctionTests_JenkinsLookup2_WithInitVal.JenkinsLookup2_WithInitVal>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new[] {
                    new KnownValue(32, TestConstants.FooBar, "85ff1761")
                };
            }
        }

        protected override long SpeedTestCount { get { return 10000000; } }


        public class JenkinsLookup2_WithInitVal
            : JenkinsLookup2
        {
            public JenkinsLookup2_WithInitVal()
            {
                InitVal = 0x7da236b9;
            }
        }
    }
        
        
    public class IHashFunctionTests_JenkinsLookup3
        : IHashFunctionTests<JenkinsLookup3>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new[] {
                    new KnownValue(32, TestConstants.FooBar, "0c2bb7ae"),
                    new KnownValue(64, TestConstants.FooBar, "0c2bb7ae4cfdf2ac"),

                    
                    new KnownValue(32, TestConstants.LoremIpsum.Take(15), "d54e489b"),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(15), "d54e489b4d1eb714"),

                    new KnownValue(32, TestConstants.LoremIpsum.Take(19), "6fba9cad"),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(19), "6fba9cad0e920728"),

                    new KnownValue(32, TestConstants.LoremIpsum.Take(23), "2166c457"),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(23), "2166c457f360e801")
                };
            }
        }

        protected override long SpeedTestCount { get { return 10000000; } }
    }

    public class IHashFunctionTests_JenkinsLookup3_WithInitVals
        : IHashFunctionTests<IHashFunctionTests_JenkinsLookup3_WithInitVals.JenkinsLookup3_WithInitVals>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new[] {
                    new KnownValue(32, TestConstants.FooBar, "fcb20673"),
                    new KnownValue(64, TestConstants.FooBar, "e26d4d4420e0b928")
                };
            }
        }

        protected override long SpeedTestCount { get { return 10000000; } }


        public class JenkinsLookup3_WithInitVals
            : JenkinsLookup3
        {
            public JenkinsLookup3_WithInitVals()
            {
                InitVal1 = 0x7da236b9;
                InitVal2 = 0x87930b75;
            }
        }
    }

    #endregion

    #region Data.HashFunction.SpookyHash

    #pragma warning disable 0618 // Ignore Obsolete warnings for SpookyHashV1

    public class IHashFunctionTests_SpookyHashV1
        : IHashFunctionTests<SpookyHashV1>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new[] {
                    new KnownValue(32,  TestConstants.FooBar,       "2da519d0"),
                    new KnownValue(64,  TestConstants.FooBar,       "2da519d008929152"),
                    new KnownValue(128, TestConstants.FooBar,       "2da519d0089291529c22f24a80017a5e"),

                    new KnownValue(32,  TestConstants.LoremIpsum,   "7ecd79cc"),
                    new KnownValue(64,  TestConstants.LoremIpsum,   "7ecd79cc4cfd7e1c"),
                    new KnownValue(128, TestConstants.LoremIpsum,   "7ecd79cc4cfd7e1c5c15710c2d261311")
                };
            }
        }

        protected override long SpeedTestCount { get { return 2000000; } }
    }

    public class IHashFunctionTests_SpookyHashV1_WithInitVals
        : IHashFunctionTests<IHashFunctionTests_SpookyHashV1_WithInitVals.SpookyHashV1_WithInitVals>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new[] {
                    new KnownValue(32,  TestConstants.FooBar,       "4b89f2dd"),
                    new KnownValue(64,  TestConstants.FooBar,       "4b89f2dd6c4fd035"),
                    new KnownValue(128, TestConstants.FooBar,       "2ffa3a68544614fc258f142b35dfb07a")
                };
            }
        }

        protected override long SpeedTestCount { get { return 2000000; } }


        public class SpookyHashV1_WithInitVals
            : SpookyHashV1
        {
            public SpookyHashV1_WithInitVals()
            {
                InitVal1 = 0x7da236b987930b75;
                InitVal2 = 0x2eb994a3851d2f54;
            }

        }
    }

    #pragma warning restore 0618 // End ignoring Obsolete warnings


    public class IHashFunctionTests_SpookyHashV2
        : IHashFunctionTests<SpookyHashV2>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new[] {
                    new KnownValue(32,  TestConstants.FooBar,        "7040f0ce"),
                    new KnownValue(64,  TestConstants.FooBar,        "7040f0ce799f9b1f"),
                    new KnownValue(128, TestConstants.FooBar,        "7040f0ce799f9b1f22020c7b2be86797"),

                    new KnownValue(32,  TestConstants.LoremIpsum,   "844cd5de"),
                    new KnownValue(64,  TestConstants.LoremIpsum,   "844cd5de8b986bbb"),
                    new KnownValue(128, TestConstants.LoremIpsum,   "844cd5de8b986bbb1062913785ea1fa2")
                };
            }
        }

        protected override long SpeedTestCount { get { return 2000000; } }
    }

    public class IHashFunctionTests_SpookyHashV2_WithInitVals
        : IHashFunctionTests<IHashFunctionTests_SpookyHashV2_WithInitVals.SpookyHashV2_WithInitVals>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new[] {
                    new KnownValue(32,  TestConstants.FooBar,        "2fffca7a"),
                    new KnownValue(64,  TestConstants.FooBar,        "2fffca7a19df0c08"),
                    new KnownValue(128, TestConstants.FooBar,        "275e9e8cb0cc53c1d604509a253730a9")
                };
            }
        }

        protected override long SpeedTestCount { get { return 2000000; } }


        public class SpookyHashV2_WithInitVals
            : SpookyHashV2
        {
            public SpookyHashV2_WithInitVals()
                : base()
            {
                InitVal1 = 0x7DA236B987930B75;
                InitVal2 = 0x2EB994A3851D2F54;
            }

        }
    }

    #endregion

    #region Data.HashFunction.MurmurHash

    public class IHashFunctionTests_MurmurHash1
        : IHashFunctionTests<MurmurHash1>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new[] {
                    new KnownValue(32, TestConstants.FooBar, "9bf4455c"),
                    
                    new KnownValue(32, TestConstants.LoremIpsum.Take(7), "aa373aff"),
                };
            }
        }

        protected override long SpeedTestCount { get { return 20000000; } }
    }

    public class IHashFunctionTests_MurmurHash2
        : IHashFunctionTests<MurmurHash2>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new[] {
                    new KnownValue(32, TestConstants.FooBar, "2ea91567"),
                    new KnownValue(64, TestConstants.FooBar, "96a1d72017469fd4"),

                    
                    new KnownValue(32, TestConstants.LoremIpsum.Take(15), "b5254038"),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(15), "71c73d0ad5fe38fa")
                };
            }
        }

        protected override long SpeedTestCount { get { return 20000000; } }
    }

    public class IHashFunctionTests_MurmurHash3
        : IHashFunctionTests<MurmurHash3>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new[] {
                    new KnownValue(32,  TestConstants.FooBar, "bdd4c4a4"),
                    new KnownValue(128, TestConstants.FooBar, "455ac81671aed2bdafd6f8bae055a274"),

                    new KnownValue(32,  TestConstants.LoremIpsum.Take(7), "78731d5a"),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(7), "1689190f13f3290b3c5ead34c751ea8a"),

                    new KnownValue(32,  TestConstants.LoremIpsum.Take(31), "30f5508d"),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(31), "5c769a439b78878e8640e16335e4313f")

                };
            }
        }

        protected override long SpeedTestCount { get { return 15000000; } }
    }

    #endregion

    #region Data.HashFunction.CityHash

    public class IHashFunctionTests_CityHash
        : IHashFunctionTests<CityHash>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new[] {
                    new KnownValue(32,  TestConstants.Empty,        "7ad156dc"),
                    new KnownValue(32,  TestConstants.FooBar,       "df4cf3e2"),
                    new KnownValue(32,  TestConstants.LoremIpsum,   "4ed6ebc2"),
                    new KnownValue(32,  TestConstants.RandomShort,  "79a7ce1f"),
                    new KnownValue(32,  TestConstants.RandomLong,   "d044ba9d"),
                    
                    new KnownValue(64,  TestConstants.Empty,        "4f40902f3b6ae19a"),
                    new KnownValue(64,  TestConstants.FooBar,       "fefcefb59ab23fc4"),
                    new KnownValue(64,  TestConstants.LoremIpsum,   "ebd1927de1f14d76"),
                    new KnownValue(64,  TestConstants.RandomShort,  "161b65ae8e69f83e"),
                    new KnownValue(64,  TestConstants.RandomLong,   "b07999a6dbfce939"),

                    new KnownValue(128, TestConstants.Empty,        "2b9ac064fc9df03d291ee592c340b53c"),
                    new KnownValue(128, TestConstants.FooBar,       "5064c017cf2c1672daa1f13a15b78c98"),
                    new KnownValue(128, TestConstants.LoremIpsum,   "31dd5cb57a6c29dc0826565eeb0cf6a4"),
                    new KnownValue(128, TestConstants.RandomShort,  "67cbf6f803487e7e09bffce371172c13"),
                    new KnownValue(128, TestConstants.RandomLong,   "98999f077f446a5ee962148d86279ea0"),


                    // Specific length tests
                    new KnownValue(32,  TestConstants.LoremIpsum.Take(3), "a02f3cd8"),
                    new KnownValue(64,  TestConstants.LoremIpsum.Take(3), "dd32f90b78ac10fa"),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(3), "8b5529b80301a1c414cc313959fd5255"),

                    new KnownValue(32,  TestConstants.LoremIpsum.Take(23), "ae0a48a6"),
                    new KnownValue(64,  TestConstants.LoremIpsum.Take(23), "86425c1021aa033a"),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(23), "658f52c24d66d71d844823de90c3d9ac"),

                    
                    new KnownValue(32,  TestConstants.LoremIpsum.Take(64), "1a2ace8a"),
                    new KnownValue(64,  TestConstants.LoremIpsum.Take(64), "4df961aa8dbe6721"),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(64), "50e89788e0dbb5d6784fbcbdf57264d1"),

                };
            }
        }

        protected override long SpeedTestCount { get { return 20000000; } }
    }
    
    #endregion

    #endregion

}
