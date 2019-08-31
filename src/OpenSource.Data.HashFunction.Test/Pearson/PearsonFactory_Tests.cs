using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.Pearson;
using System.Text;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.Pearson
{
    public class PearsonFactory_Tests
    {
        [Fact]
        public void PearsonFactory_Instance_IsDefined()
        {
            Assert.NotNull(PearsonFactory.Instance);
            Assert.IsType<PearsonFactory>(PearsonFactory.Instance);
        }

        [Fact]
        public void PearsonFactory_Create_Works()
        {
            var defaultPearsonConfig = new WikipediaPearsonConfig();

            var pearsonFactory = PearsonFactory.Instance;
            var pearson = pearsonFactory.Create();

            Assert.NotNull(pearson);
            Assert.IsType<Pearson_Implementation>(pearson);


            var resultingPearsonConfig = pearson.Config;

            Assert.Equal(defaultPearsonConfig.Table, resultingPearsonConfig.Table);
            Assert.Equal(defaultPearsonConfig.HashSizeInBits, resultingPearsonConfig.HashSizeInBits);
        }


        [Fact]
        public void PearsonFactory_Create_Config_IsNull_Throws()
        {
            var pearsonFactory = PearsonFactory.Instance;

            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => pearsonFactory.Create(null))
                    .ParamName);
        }

        [Fact]
        public void PearsonFactory_Create_Config_Works()
        {
            var pearsonConfig = new WikipediaPearsonConfig() {
                HashSizeInBits = 32,
            };

            var pearsonFactory = PearsonFactory.Instance;
            var pearson = pearsonFactory.Create(pearsonConfig);

            Assert.NotNull(pearson);
            Assert.IsType<Pearson_Implementation>(pearson);


            var resultingPearsonConfig = pearson.Config;

            Assert.Equal(pearsonConfig.Table, resultingPearsonConfig.Table);
            Assert.Equal(pearsonConfig.HashSizeInBits, resultingPearsonConfig.HashSizeInBits);
        }
    }
}
