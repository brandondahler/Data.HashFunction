using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.HashFunction;
using System.Security.Cryptography;
using Xunit;
using System.IO;

namespace System.Data.HashFunction.Test
{

    public abstract class HashFunctionWrapperTests<HashAlgorithmT>
        where HashAlgorithmT : HashAlgorithm, new()
    {
        protected const string KnownValue = "password";

        /// <summary>
        /// The hex encoded result of running the HashAlgorithmT on the KnownValue string
        /// </summary>
        protected abstract string HashedKnownValue { get; }


        #region NonGeneric Tests

        #region Owner Tests

        [Fact]
        public void NonGeneric_Instance_Computes_Using_ByteArray()
        {
            var hfw = new HashAlgorithmWrapper(new HashAlgorithmT());

            var hashResults = hfw.ComputeHash(StringToBytes(KnownValue));

            Assert.Equal(HexStringToBytes(HashedKnownValue), hashResults);
        }


        [Fact]
        public void NonGeneric_Instance_Computes_Using_Stream()
        {
            var hfw = new HashAlgorithmWrapper(new HashAlgorithmT());
            byte[] hashResults;

            using (var ms = new MemoryStream(StringToBytes(KnownValue)))
                hashResults = hfw.ComputeHash(ms);

            Assert.Equal(HexStringToBytes(HashedKnownValue), hashResults);
        }

        #endregion

        #region NonOwner Tests

        [Fact]
        public void NonGeneric_Instance_NonOwner_Computes_Using_ByteArray()
        {
            var hfw = new HashAlgorithmWrapper(new HashAlgorithmT(), false);


            var hashResults = hfw.ComputeHash(StringToBytes(KnownValue));
            Assert.Equal(HexStringToBytes(HashedKnownValue), hashResults);


            hashResults = hfw.ComputeHash(StringToBytes(KnownValue));
            Assert.Equal(HexStringToBytes(HashedKnownValue), hashResults);
        }


        [Fact]
        public void NonGeneric_Instance_NonOwner_Computes_Using_Stream()
        {
            var hfw = new HashAlgorithmWrapper(new HashAlgorithmT(), false);
            byte[] hashResults;


            using (var ms = new MemoryStream(StringToBytes(KnownValue)))
                hashResults = hfw.ComputeHash(ms);

            Assert.Equal(HexStringToBytes(HashedKnownValue), hashResults);


            using (var ms = new MemoryStream(StringToBytes(KnownValue)))
                hashResults = hfw.ComputeHash(ms);

            Assert.Equal(HexStringToBytes(HashedKnownValue), hashResults);
        }

        #endregion

        #endregion

        #region Generic Tests

        [Fact]
        public void Generic_Computes_Using_ByteArray()
        {
            var hfw = new HashAlgorithmWrapper<HashAlgorithmT>();
            
            var hashResults = hfw.ComputeHash(StringToBytes(KnownValue));
            Assert.Equal(HexStringToBytes(HashedKnownValue), hashResults);


            hashResults = hfw.ComputeHash(StringToBytes(KnownValue));
            Assert.Equal(HexStringToBytes(HashedKnownValue), hashResults);
        }

        [Fact]
        public void Generic_Computes_Using_Stream()
        {
            var hfw = new HashAlgorithmWrapper<HashAlgorithmT>();
            byte[] hashResults;

            using (var ms = new MemoryStream(StringToBytes(KnownValue)))
                hashResults = hfw.ComputeHash(ms);

            Assert.Equal(HexStringToBytes(HashedKnownValue), hashResults);


            using (var ms = new MemoryStream(StringToBytes(KnownValue)))
                hashResults = hfw.ComputeHash(ms);

            Assert.Equal(HexStringToBytes(HashedKnownValue), hashResults);
        }

        #endregion


        #region Utility Functions

        private static byte[] StringToBytes(string inputString)
        {
            return Encoding.UTF8.GetBytes(inputString);
        }

        private static byte[] HexStringToBytes(string hexString)
        {
            var chars = hexString.ToCharArray();
            var bytes = new byte[chars.Length / 2];

            if (chars.Length % 2 == 1)
                throw new ArgumentException("Hex string contains invalid number of characters.");

            for (int x = 0; x < chars.Length; ++x)
            {
                if (x % 2 == 1)
                    bytes[x / 2] <<= 4;

                if (chars[x] >= '0' && chars[x] <= '9')
                    bytes[x / 2] |= (byte) (chars[x] - '0');
                else if (chars[x] >= 'a' && chars[x] <= 'f')
                    bytes[x / 2] |= (byte) (chars[x] - 'a' + 10);
                else if (chars[x] >= 'A' && chars[x] <= 'F')
                    bytes[x / 2] |= (byte)(chars[x] - 'A' + 10);
                else
                    throw new ArgumentException("Hex string contains invalid characters.");
            }

            return bytes;
        }

        #endregion
    }


    #region Type Tests

    #region Abstract Type Tests

    public abstract class HashFunctionWrapperTests_SHA1<SHA1T>
        : HashFunctionWrapperTests<SHA1T>
        where SHA1T : SHA1, new()
    {
        protected override string HashedKnownValue { get { return "5BAA61E4C9B93F3F0682250B6CF8331B7EE68FD8"; } }
    }

    public abstract class HashFunctionWrapperTests_SHA256<SHA256T>
        : HashFunctionWrapperTests<SHA256T>
        where SHA256T : SHA256, new()
    {
        protected override string HashedKnownValue { get { return "5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8"; } }
    }

    public abstract class HashFunctionWrapperTests_SHA384<SHA384T>
        : HashFunctionWrapperTests<SHA384T>
        where SHA384T : SHA384, new()
    {
        protected override string HashedKnownValue { get { return "A8B64BABD0ACA91A59BDBB7761B421D4F2BB38280D3A75BA0F21F2BEBC45583D446C598660C94CE680C47D19C30783A7"; } }
    }

    public abstract class HashFunctionWrapperTests_SHA512<SHA512T>
        : HashFunctionWrapperTests<SHA512T>
        where SHA512T : SHA512, new()
    {
        protected override string HashedKnownValue { get { return "B109F3BBBC244EB82441917ED06D618B9008DD09B3BEFD1B5E07394C706A8BB980B1D7785E5976EC049B46DF5F1326AF5A2EA6D103FD07C95385FFAB0CACBC86"; } }
    }

    public abstract class HashFunctionWrapperTests_MD5<MD5T>
        : HashFunctionWrapperTests<MD5T>
        where MD5T : MD5, new()
    {
        protected override string HashedKnownValue { get { return "5F4DCC3B5AA765D61D8327DEB882CF99"; } }
    }

    public abstract class HashFunctionWrapperTests_RIPEMD160<RIPEMD160T>
        : HashFunctionWrapperTests<RIPEMD160T>
        where RIPEMD160T : RIPEMD160, new()
    {
        protected override string HashedKnownValue { get { return "2C08E8F5884750A7B99F6F2F342FC638DB25FF31"; } }
    }

    #endregion

    #region Concrete Type Tests

    public class HashFunctionWrapperTests_SHA1CryptoServiceProvider
        : HashFunctionWrapperTests_SHA1<SHA1CryptoServiceProvider>
    { }

    public class HashFunctionWrapperTests_SHA1Cng
        : HashFunctionWrapperTests_SHA1<SHA1Cng>
    { }

    public class HashFunctionWrapperTests_SHA1Managed
        : HashFunctionWrapperTests_SHA1<SHA1Managed>
    { }


    public class HashFunctionWrapperTests_SHA256CryptoServiceProvider
        : HashFunctionWrapperTests_SHA256<SHA256CryptoServiceProvider>
    { }

    public class HashFunctionWrapperTests_SHA256Cng
        : HashFunctionWrapperTests_SHA256<SHA256Cng>
    { }

    public class HashFunctionWrapperTests_SHA256Managed
        : HashFunctionWrapperTests_SHA256<SHA256Managed>
    { }


    public class HashFunctionWrapperTests_SHA384CryptoServiceProvider
        : HashFunctionWrapperTests_SHA384<SHA384CryptoServiceProvider>
    { }

    public class HashFunctionWrapperTests_SHA384Cng
        : HashFunctionWrapperTests_SHA384<SHA384Cng>
    { }

    public class HashFunctionWrapperTests_SHA384Managed
        : HashFunctionWrapperTests_SHA384<SHA384Managed>
    { }


    public class HashFunctionWrapperTests_SHA512CryptoServiceProvider
        : HashFunctionWrapperTests_SHA512<SHA512CryptoServiceProvider>
    { }

    public class HashFunctionWrapperTests_SHA512Cng
        : HashFunctionWrapperTests_SHA512<SHA512Cng>
    { }

    public class HashFunctionWrapperTests_SHA512Managed
        : HashFunctionWrapperTests_SHA512<SHA512Managed>
    { }
    

    public class HashFunctionWrapperTests_MD5CryptoServiceProvider
        : HashFunctionWrapperTests_MD5<MD5CryptoServiceProvider>
    { }

    public class HashFunctionWrapperTests_MD5Cng
        : HashFunctionWrapperTests_MD5<MD5Cng>
    { }


    public class HashFunctionWrapperTests_RIPEMD160
        : HashFunctionWrapperTests_RIPEMD160<RIPEMD160Managed>
    { }
    
    #endregion
    
    #endregion

}
