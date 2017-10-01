using System;
using System.Collections.Generic;
using System.Data.HashFunction.MurmurHash;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.MurmurHash
{
    public class MurmurHash1Factory_Tests
    {
        [Fact]
        public void MurmurHash1Factory_Instance_IsDefined()
        {
            Assert.NotNull(MurmurHash1Factory.Instance);
            Assert.IsType<MurmurHash1Factory>(MurmurHash1Factory.Instance);
        }

        [Fact]
        public void MurmurHash1Factory_Create_Works()
        {
            var defaultMurmurHash1Config = new MurmurHash1Config();

            var murmurHash1Factory = MurmurHash1Factory.Instance;
            var murmurHash1 = murmurHash1Factory.Create();

            Assert.NotNull(murmurHash1);
            Assert.IsType<MurmurHash1_Implementation>(murmurHash1);


            var resultingMurmurHash1Config = murmurHash1.Config;

            Assert.Equal(defaultMurmurHash1Config.Seed, resultingMurmurHash1Config.Seed);
        }


        [Fact]
        public void MurmurHash1Factory_Create_Config_IsNull_Throws()
        {
            var murmurHash1Factory = MurmurHash1Factory.Instance;

            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => murmurHash1Factory.Create(null))
                    .ParamName);
        }

        [Fact]
        public void MurmurHash1Factory_Create_Config_Works()
        {
            var murmurHash1Config = new MurmurHash1Config() {
                Seed = 1337U
            };

            var murmurHash1Factory = MurmurHash1Factory.Instance;
            var murmurHash1 = murmurHash1Factory.Create(murmurHash1Config);

            Assert.NotNull(murmurHash1);
            Assert.IsType<MurmurHash1_Implementation>(murmurHash1);


            var resultingMurmurHash1Config = murmurHash1.Config;

            Assert.Equal(murmurHash1Config.Seed, resultingMurmurHash1Config.Seed);
        }
    }
}
