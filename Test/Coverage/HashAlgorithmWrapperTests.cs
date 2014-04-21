using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.Coverage
{
    public class HashAlgorithmWrapperTests
    {
        [Fact]
        public void HashAlgorithmWrapper_HashSize_Get_ReportsCorrectly()
        {
            using (var hfb = new HashAlgorithmWrapper(new SHA256Managed()))
                Assert.Equal(256, hfb.HashSize);
        }

        [Fact]
        public void HashAlgorithmWrapperT_HashSize_Get_ReportsCorrectly()
        {
            using (var hfb = new HashAlgorithmWrapper<SHA256Managed>())
                Assert.Equal(256, hfb.HashSize);
        }


        [Fact]
        public void HashAlgorithmWrapper_ValidHashSizes_Get_ReportsCorrectly()
        {
            using (var hfb = new HashAlgorithmWrapper(new SHA256Managed()))
                Assert.Equal(new[] { 256 }, hfb.ValidHashSizes);
        }

        [Fact]
        public void HashAlgorithmWrapperT_ValidHashSizes_Get_ReportsCorrectly()
        {
            using (var hfb = new HashAlgorithmWrapper<SHA256Managed>())
                Assert.Equal(new[] { 256 }, hfb.ValidHashSizes);
        }


        [Fact]
        public void HashAlgorithmWrapper_HashSize_SetValid_DoesNotThrows()
        {
            using (var hfb = new HashAlgorithmWrapper(new SHA256Managed()))
                Assert.DoesNotThrow(() => hfb.HashSize = 256);

        }

        [Fact]
        public void HashAlgorithmWrapperT_HashSize_SetValid_DoesNotThrows()
        {
            using (var hfb = new HashAlgorithmWrapper<SHA256Managed>())
                Assert.DoesNotThrow(() => hfb.HashSize = 256);
        }


        [Fact]
        public void HashAlgorithmWrapper_HashSize_SetInvalid_Throws()
        {
            using (var hfb = new HashAlgorithmWrapper(new SHA256Managed()))
                Assert.Throws<ArgumentOutOfRangeException>(() => hfb.HashSize = 1);
        }

        [Fact]
        public void HashAlgorithmWrapperT_HashSize_SetInvalid_Throws()
        {
            using (var hfb = new HashAlgorithmWrapper<SHA256Managed>())
                Assert.Throws<ArgumentOutOfRangeException>(() => hfb.HashSize = 1);
        }
    }
}
