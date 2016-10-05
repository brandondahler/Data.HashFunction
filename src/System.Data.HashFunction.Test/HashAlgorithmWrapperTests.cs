#if !NETCOREAPP1_0

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

    public class HashAlgorithmWrapper_Tests
    {
        [Fact]
        public void HashAlgorithmWrapper_Dispose_Owner_DoesNotThrow()
        {
            var haw = new HashAlgorithmWrapper(new SHA1Managed(), true);


            haw.Dispose();
        }

        [Fact]
        public void HashAlgorithmWrapper_Dispose_Owner_Invalidates()
        {
            var ha = new SHA1Managed();
            var haw = new HashAlgorithmWrapper(ha, true);


            haw.Dispose();


            // HashAlgorithm shouldn't be usable anymore.
            Assert.Throws<ObjectDisposedException>(() =>
                ha.ComputeHash(new byte[0]));

            Assert.Throws<ObjectDisposedException>(() =>
                ha.ComputeHash(new MemoryStream()));


            // HashAlgorithmWrapper should not be usable anymore either.
            Assert.Throws<ObjectDisposedException>(() =>
                haw.ComputeHash(new byte[0]));

            Assert.Throws<ObjectDisposedException>(() =>
                haw.ComputeHash(new MemoryStream()));
        }


        [Fact]
        public void HashAlgorithmWrapper_Dispose_NonOwner_DoesNotThrow()
        {
            var haw = new HashAlgorithmWrapper(new SHA1Managed(), false);


            haw.Dispose();
        }

        [Fact]
        public void HashAlgorithmWrapper_Dispose_NonOwner_Preserves_HashAlgorithm()
        {
            var ha = new SHA1Managed();
            var haw = new HashAlgorithmWrapper(ha, false);


            haw.Dispose();


            // HashAlgorithm should still be usable.
            ha.ComputeHash(new byte[0]);
            ha.ComputeHash(new MemoryStream());


            // HashAlgorithmWrapper's usability afterwards is an implementation detail 
            //   and shouldn't be tested.
        }



        [Fact]
        public void HashAlgorithmWrapper_Generic_Dispose_DoesNotThrow()
        {
            var haw = new HashAlgorithmWrapper<SHA1Managed>();

            haw.Dispose();
        }

        [Fact]
        public void HashAlgorithmWrapper_Generic_Dispose_Invalidates()
        {
            var haw = new HashAlgorithmWrapper<SHA1Managed>();

            haw.Dispose();


            // HashAlgorithmWrapper should no longer be usable.
            Assert.Throws<ObjectDisposedException>(() =>
                haw.ComputeHash(new byte[0]));

            Assert.Throws<ObjectDisposedException>(() =>
                haw.ComputeHash(new MemoryStream()));
        }
    }
}

#endif