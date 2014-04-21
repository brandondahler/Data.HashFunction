using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.Coverage
{
    public abstract class IHashFunctionTests<HashFunctionT>
        where HashFunctionT : IHashFunction, new()
    {
        [Fact]
        public void SpookyHashV1_ComputeHash_InvalidHashSize_Throws()
        {
            var sh = new HashFunctionT();
            sh.HashSize = -1;

            Assert.Throws<ArgumentOutOfRangeException>(() => sh.ComputeHash(new byte[0]));
        }
    }

    #region Concrete Test Classes

    #region Data.HashFunction.SpookyHash

    #pragma warning disable 0618 // Ignore Obsolete warnings for SpookyHashV1

    public class IHashFunctionTests_SpookyHashV1Tests
        : IHashFunctionTests<IHashFunctionTests_SpookyHashV1Tests.Test_SpookyHashV1>
    {
        public class Test_SpookyHashV1
            : SpookyHashV1
        {
            public override IEnumerable<int> ValidHashSizes { get { return base.ValidHashSizes.Union(new[] { -1 }); } }
        }
    }
    
    #pragma warning restore 0618 // End ignoring Obsolete warnings


    public class IHashFunctionTests_SpookyHashV2Tests
        : IHashFunctionTests<IHashFunctionTests_SpookyHashV2Tests.Test_SpookyHashV2>
    {
        public class Test_SpookyHashV2
            : SpookyHashV2
        {
            public override IEnumerable<int> ValidHashSizes { get { return base.ValidHashSizes.Union(new[] { -1 }); } }
        }
    }    

    #endregion


    #region Data.HashFunction.Jenkins

    public class IHashFunctionTests_JenkinsLookup3Tests
        : IHashFunctionTests<IHashFunctionTests_JenkinsLookup3Tests.Test_JenkinsLookup3>
    {
        public class Test_JenkinsLookup3
            : JenkinsLookup3
        {
            public override IEnumerable<int> ValidHashSizes { get { return base.ValidHashSizes.Union(new[] { -1 }); } }
        }
    }    

    #endregion


    #region Data.HashFunctions.MurmurHash

    public class IHashFunctionTests_MurmurHash2Tests
        : IHashFunctionTests<IHashFunctionTests_MurmurHash2Tests.Test_MurmurHash2>
    {
        public class Test_MurmurHash2
            : MurmurHash2
        {
            public override IEnumerable<int> ValidHashSizes { get { return base.ValidHashSizes.Union(new[] { -1 }); } }
        }
    }

    public class IHashFunctionTests_MurmurHash3Tests
        : IHashFunctionTests<IHashFunctionTests_MurmurHash3Tests.Test_MurmurHash3>
    {
        public class Test_MurmurHash3
            : MurmurHash3
        {
            public override IEnumerable<int> ValidHashSizes { get { return base.ValidHashSizes.Union(new[] { -1 }); } }
        }
    }

    #endregion


    #region Data.HashFunction.CityHash

    public class IHashFunctionTests_CityHashTests
        : IHashFunctionTests<IHashFunctionTests_CityHashTests.Test_CityHash>
    {
        public class Test_CityHash
            : CityHash
        {
            public override IEnumerable<int> ValidHashSizes { get { return base.ValidHashSizes.Union(new[] { -1 }); } }
        }
    }

    #endregion

    #endregion

}
