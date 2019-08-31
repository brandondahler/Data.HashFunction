using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.FNV;
using System.Numerics;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.FNV
{
    public class FNVConfig_Tests
    {
        [Fact]
        public void FNVConfig_Defaults_HaventChanged()
        {
            var fnvConfig = new FNVConfig();


            Assert.Equal(0, fnvConfig.HashSizeInBits);
            Assert.Equal(BigInteger.Zero, fnvConfig.Prime);
            Assert.Equal(BigInteger.Zero, fnvConfig.Offset);
        }

        [Fact]
        public void FNVConfig_Clone_Works()
        {
            var fnvConfig = new FNVConfig() {
                HashSizeInBits = 32,
                Prime = new BigInteger(1337),
                Offset = new BigInteger(7331)
            };

            var fnvConfigClone = fnvConfig.Clone();

            Assert.IsType<FNVConfig>(fnvConfigClone);

            Assert.Equal(fnvConfig.HashSizeInBits, fnvConfigClone.HashSizeInBits);
            Assert.Equal(fnvConfig.Prime, fnvConfigClone.Prime);
            Assert.Equal(fnvConfig.Offset, fnvConfigClone.Offset);
        }


        [Fact]
        public void FNVConfig_GetPredefinedConfig_InvalidSizes_Throw()
        {
            var invalidHashSizes = new[] { 0, 1, 8, 16, 31, 33, 63, 65, 127, 255, 1023, 2048, 4096 };

            foreach (var invalidHashSize in invalidHashSizes)
            {
                Assert.Equal(
                    "hashSizeInBits",
                    Assert.Throws<ArgumentOutOfRangeException>(
                            () => FNVConfig.GetPredefinedConfig(invalidHashSize))
                        .ParamName);
            }
        }

        [Fact]
        public void FNVConfig_GetPredefinedConfig_ExpectedValues_Work()
        {
            foreach (var expectedPredefinedConfig in _expectedPredefinedConfigs)
            {
                var fnvConfig = FNVConfig.GetPredefinedConfig(expectedPredefinedConfig.HashSizeInBits);

                Assert.Equal(expectedPredefinedConfig.HashSizeInBits, fnvConfig.HashSizeInBits);
                Assert.Equal(expectedPredefinedConfig.Prime, fnvConfig.Prime);
                Assert.Equal(expectedPredefinedConfig.Offset, fnvConfig.Offset);
            }
        }


        private static readonly IEnumerable<IFNVConfig> _expectedPredefinedConfigs =
            new[] {
                new FNVConfig() {
                    HashSizeInBits = 32,
                    Prime = new BigInteger(16777619),
                    Offset = new BigInteger(2166136261)
                },
                new FNVConfig() {
                    HashSizeInBits = 64,
                    Prime = new BigInteger(1099511628211),
                    Offset = new BigInteger(14695981039346656037)
                },
                new FNVConfig() {
                    HashSizeInBits = 128,
                    Prime = BigInteger.Parse("309485009821345068724781371"),
                    Offset = BigInteger.Parse("144066263297769815596495629667062367629")
                },
                new FNVConfig() {
                    HashSizeInBits = 256,
                    Prime = BigInteger.Parse("374144419156711147060143317175368453031918731002211"),
                    Offset = BigInteger.Parse("100029257958052580907070968620625704837092796014241193945225284501741471925557")
                },
                new FNVConfig() {
                    HashSizeInBits = 512,
                    Prime = BigInteger.Parse("35835915874844867368919076489095108449946327955754392558399825615420669938882575126094039892345713852759"),
                    Offset = BigInteger.Parse("9659303129496669498009435400716310466090418745672637896108374329434462657994582932197716438449813051892206539805784495328239340083876191928701583869517785")
                },
                new FNVConfig() {
                    HashSizeInBits = 1024,
                    Prime = BigInteger.Parse("5016456510113118655434598811035278955030765345404790744303017523831112055108147451509157692220295382716162651878526895249385292291816524375083746691371804094271873160484737966720260389217684476157468082573"),
                    Offset = BigInteger.Parse("14197795064947621068722070641403218320880622795441933960878474914617582723252296732303717722150864096521202355549365628174669108571814760471015076148029755969804077320157692458563003215304957150157403644460363550505412711285966361610267868082893823963790439336411086884584107735010676915")
                }
            };

    }
}
