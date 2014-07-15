using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.HashFunction;
using System.Security.Cryptography;
using Xunit;
using System.IO;
using Moq;

namespace System.Data.HashFunction.Test
{

    public abstract class HashAlgorithmWrapperTests<HashAlgorithmT>
        where HashAlgorithmT : HashAlgorithm, new()
    {
        protected const string KnownValue = "password";

        /// <summary>
        /// The hex encoded result of running the HashAlgorithmT on the KnownValue string
        /// </summary>
        protected abstract string HashedKnownValue { get; }



        #region NonGeneric Tests

        [Fact]
        public void HashAlgorithmWrapper_NonGeneric_ComputeHash_Stream_InvalidHashSize_Throws()
        {
            var hawMock = new Mock<HashAlgorithmWrapper>((HashAlgorithm) new HashAlgorithmT(), true);
            hawMock.SetupGet(o => o.HashSize)
                .Returns(-1);

            var haw = hawMock.Object;

            using (var ms = new MemoryStream(new byte[0]))
            {
                Assert.Equal("HashSize", 
                    Assert.Throws<ArgumentOutOfRangeException>(() =>
                        haw.ComputeHash(ms))
                    .ParamName);
            }
        }

        #region Owner Tests

        [Fact]
        public void HashAlgorithmWrapper_NonGeneric_Owner_ComputeHash_ByteArray()
        {
            using(var haw = new HashAlgorithmWrapper(new HashAlgorithmT(), true))
            {
                var hashResults = haw.ComputeHash(KnownValue.ToBytes());

                Assert.Equal(HashedKnownValue.HexToBytes(), hashResults);
            }
        }


        [Fact]
        public void HashAlgorithmWrapper_NonGeneric_Owner_ComputeHash_Stream()
        {
            using(var haw = new HashAlgorithmWrapper(new HashAlgorithmT(), true))
            {
                byte[] hashResults;

                using (var ms = new MemoryStream(KnownValue.ToBytes()))
                    hashResults = haw.ComputeHash(ms);

                Assert.Equal(HashedKnownValue.HexToBytes(), hashResults);
            }
        }

        #endregion

        #region NonOwner Tests

        [Fact]
        public void HashAlgorithmWrapper_NonGeneric_NonOwner_ComputeHash_ByteArray()
        {
            var ha = new HashAlgorithmT();

            using (var haw = new HashAlgorithmWrapper(ha, false))
            {
                var hashResults = haw.ComputeHash(KnownValue.ToBytes());
                Assert.Equal(HashedKnownValue.HexToBytes(), hashResults);


                hashResults = haw.ComputeHash(KnownValue.ToBytes());
                Assert.Equal(HashedKnownValue.HexToBytes(), hashResults);
            }

            Assert.DoesNotThrow(() => ha.ComputeHash(new byte[0]));
        }


        [Fact]
        public void HashAlgorithmWrapper_NonGeneric_NonOwner_ComputeHash_Stream()
        {
            var ha = new HashAlgorithmT();

            using (var haw = new HashAlgorithmWrapper(new HashAlgorithmT(), false))
            {
                byte[] hashResults;


                using (var ms = new MemoryStream(KnownValue.ToBytes()))
                    hashResults = haw.ComputeHash(ms);

                Assert.Equal(HashedKnownValue.HexToBytes(), hashResults);


                using (var ms = new MemoryStream(KnownValue.ToBytes()))
                    hashResults = haw.ComputeHash(ms);

                Assert.Equal(HashedKnownValue.HexToBytes(), hashResults);
            }

            Assert.DoesNotThrow(() => ha.ComputeHash(new byte[0]));
        }

        #endregion

        #endregion

        #region Generic Tests

        [Fact]
        public void HashAlgorithmWrapper_Generic_ComputeHash_Stream_InvalidHashSize_Throws()
        {
            var hawMock = new Mock<HashAlgorithmWrapper<HashAlgorithmT>>();
            hawMock.SetupGet(o => o.HashSize)
                .Returns(-1);

            var haw = hawMock.Object;

            using (var ms = new MemoryStream(new byte[0]))
            {
                Assert.Equal("HashSize", 
                    Assert.Throws<ArgumentOutOfRangeException>(() =>
                        haw.ComputeHash(ms))
                    .ParamName);
            }
        }

        [Fact]
        public void HashAlgorithmWrapper_Generic_ComputeHash_ByteArray()
        {
            using (var haw = new HashAlgorithmWrapper<HashAlgorithmT>())
            {
                var hashResults = haw.ComputeHash(KnownValue.ToBytes());
                Assert.Equal(HashedKnownValue.HexToBytes(), hashResults);

                hashResults = haw.ComputeHash(KnownValue.ToBytes());
                Assert.Equal(HashedKnownValue.HexToBytes(), hashResults);
            }
        }

        [Fact]
        public void HashAlgorithmWrapper_Generic_ComputeHash_Stream()
        {
            using (var haw = new HashAlgorithmWrapper<HashAlgorithmT>())
            {
                byte[] hashResults;

                using (var ms = new MemoryStream(KnownValue.ToBytes()))
                    hashResults = haw.ComputeHash(ms);

                Assert.Equal(HashedKnownValue.HexToBytes(), hashResults);


                using (var ms = new MemoryStream(KnownValue.ToBytes()))
                    hashResults = haw.ComputeHash(ms);

                Assert.Equal(HashedKnownValue.HexToBytes(), hashResults);
            }
        }

        #endregion

    }


    #region Type Tests

    #region Abstract Type Tests

    public abstract class HashAlgorithmWrapperTests_SHA1<SHA1T>
        : HashAlgorithmWrapperTests<SHA1T>
        where SHA1T : SHA1, new()
    {
        protected override string HashedKnownValue { get { return "5BAA61E4C9B93F3F0682250B6CF8331B7EE68FD8"; } }
    }

    public abstract class HashAlgorithmWrapperTests_SHA256<SHA256T>
        : HashAlgorithmWrapperTests<SHA256T>
        where SHA256T : SHA256, new()
    {
        protected override string HashedKnownValue { get { return "5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8"; } }
    }

    public abstract class HashAlgorithmWrapperTests_SHA384<SHA384T>
        : HashAlgorithmWrapperTests<SHA384T>
        where SHA384T : SHA384, new()
    {
        protected override string HashedKnownValue { get { return "A8B64BABD0ACA91A59BDBB7761B421D4F2BB38280D3A75BA0F21F2BEBC45583D446C598660C94CE680C47D19C30783A7"; } }
    }

    public abstract class HashAlgorithmWrapperTests_SHA512<SHA512T>
        : HashAlgorithmWrapperTests<SHA512T>
        where SHA512T : SHA512, new()
    {
        protected override string HashedKnownValue { get { return "B109F3BBBC244EB82441917ED06D618B9008DD09B3BEFD1B5E07394C706A8BB980B1D7785E5976EC049B46DF5F1326AF5A2EA6D103FD07C95385FFAB0CACBC86"; } }
    }

    public abstract class HashAlgorithmWrapperTests_MD5<MD5T>
        : HashAlgorithmWrapperTests<MD5T>
        where MD5T : MD5, new()
    {
        protected override string HashedKnownValue { get { return "5F4DCC3B5AA765D61D8327DEB882CF99"; } }
    }

    public abstract class HashAlgorithmWrapperTests_RIPEMD160<RIPEMD160T>
        : HashAlgorithmWrapperTests<RIPEMD160T>
        where RIPEMD160T : RIPEMD160, new()
    {
        protected override string HashedKnownValue { get { return "2C08E8F5884750A7B99F6F2F342FC638DB25FF31"; } }
    }

    #endregion

    #region Concrete Type Tests

    public class HashAlgorithmWrapperTests_SHA1CryptoServiceProvider
        : HashAlgorithmWrapperTests_SHA1<SHA1CryptoServiceProvider>
    { }

    public class HashAlgorithmWrapperTests_SHA1Cng
        : HashAlgorithmWrapperTests_SHA1<SHA1Cng>
    { }

    public class HashAlgorithmWrapperTests_SHA1Managed
        : HashAlgorithmWrapperTests_SHA1<SHA1Managed>
    { }


    public class HashAlgorithmWrapperTests_SHA256CryptoServiceProvider
        : HashAlgorithmWrapperTests_SHA256<SHA256CryptoServiceProvider>
    { }

    public class HashAlgorithmWrapperTests_SHA256Cng
        : HashAlgorithmWrapperTests_SHA256<SHA256Cng>
    { }

    public class HashAlgorithmWrapperTests_SHA256Managed
        : HashAlgorithmWrapperTests_SHA256<SHA256Managed>
    { }


    public class HashAlgorithmWrapperTests_SHA384CryptoServiceProvider
        : HashAlgorithmWrapperTests_SHA384<SHA384CryptoServiceProvider>
    { }

    public class HashAlgorithmWrapperTests_SHA384Cng
        : HashAlgorithmWrapperTests_SHA384<SHA384Cng>
    { }

    public class HashAlgorithmWrapperTests_SHA384Managed
        : HashAlgorithmWrapperTests_SHA384<SHA384Managed>
    { }


    public class HashAlgorithmWrapperTests_SHA512CryptoServiceProvider
        : HashAlgorithmWrapperTests_SHA512<SHA512CryptoServiceProvider>
    { }

    public class HashAlgorithmWrapperTests_SHA512Cng
        : HashAlgorithmWrapperTests_SHA512<SHA512Cng>
    { }

    public class HashAlgorithmWrapperTests_SHA512Managed
        : HashAlgorithmWrapperTests_SHA512<SHA512Managed>
    { }
    

    public class HashAlgorithmWrapperTests_MD5CryptoServiceProvider
        : HashAlgorithmWrapperTests_MD5<MD5CryptoServiceProvider>
    { }

    public class HashAlgorithmWrapperTests_MD5Cng
        : HashAlgorithmWrapperTests_MD5<MD5Cng>
    { }


    public class HashAlgorithmWrapperTests_RIPEMD160
        : HashAlgorithmWrapperTests_RIPEMD160<RIPEMD160Managed>
    { }
    
    #endregion
    
    #endregion

}
