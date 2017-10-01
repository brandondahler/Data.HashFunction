using System;
using System.Collections.Generic;
using System.Data.HashFunction.Pearson;
using System.Linq;
using System.Text;
using Xunit;

namespace System.Data.HashFunction.Test.Pearson
{
    public class PearsonConfig_Tests
    {
        [Fact]
        public void PearsonConfig_Defaults_HaventChanged()
        {
            var pearsonConfig = new PearsonConfig();

            Assert.Null(pearsonConfig.Table);

            Assert.Equal(8, pearsonConfig.HashSizeInBits);
        }

        [Fact]
        public void PearsonConfig_Clone_Works()
        {
            var pearsonConfig = new PearsonConfig() {
                Table = new byte[256],
                HashSizeInBits = 16,
            };

            var pearsonConfigClone = pearsonConfig.Clone();

            Assert.IsType<PearsonConfig>(pearsonConfigClone);

            Assert.Equal(pearsonConfig.Table, pearsonConfigClone.Table);
        }

        [Fact]
        public void PearsonConfig_Clone_WithNullArrays_Works()
        {
            var pearsonConfig = new PearsonConfig() {
                Table = null,
                HashSizeInBits = 16,
            };

            var pearsonConfigClone = pearsonConfig.Clone();

            Assert.IsType<PearsonConfig>(pearsonConfigClone);

            Assert.Equal(pearsonConfig.Table, pearsonConfigClone.Table);
            Assert.Equal(pearsonConfig.HashSizeInBits, pearsonConfigClone.HashSizeInBits);
        }
    }
}
