//! Automatically generated from IHashFunctionTests_HashFunctionBase.tt
//! Direct modifications to this file will be lost.

using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.Test.Mocks;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test
{

    #pragma warning disable 0618 // Disable ObsoleteAttribute warnings

    #region IHashFunctionTests

    #region Data.HashFunction.BernsteinHash

    public class IHashFunctionAsyncTests_BernsteinHash
        : IHashFunctionAsyncTests<BernsteinHash>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0x00000000),
                    new KnownValue(32, TestConstants.FooBar, 0xf6055bf9),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x24bdc248),
                };
            }
        }

        protected override BernsteinHash CreateHashFunction(int hashSize)
        {
            return new BernsteinHash();
        }

        protected override Mock<BernsteinHash> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<BernsteinHash>();
        }
    }
    

    public class IHashFunctionAsyncTests_ModifiedBernsteinHash
        : IHashFunctionAsyncTests<ModifiedBernsteinHash>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0x00000000),
                    new KnownValue(32, TestConstants.FooBar, 0xf030b397),
                    new KnownValue(32, TestConstants.LoremIpsum, 0xfeceaf2a),
                };
            }
        }

        protected override ModifiedBernsteinHash CreateHashFunction(int hashSize)
        {
            return new ModifiedBernsteinHash();
        }

        protected override Mock<ModifiedBernsteinHash> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<ModifiedBernsteinHash>();
        }
    }
    

    #endregion

    #region Data.HashFunction.Blake2B

    public class IHashFunctionAsyncTests_Blake2B_DefaultConstructor
        : IHashFunctionAsyncTests<Blake2B>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(512, TestConstants.Empty, "786A02F742015903C6C6FD852552D272912F4740E15847618A86E217F71F5419D25E1031AFEE585313896444934EB04B903A685B1448B755D56F701AFE9BE2CE"),
                    new KnownValue(512, TestConstants.FooBar, "8DF31F60D6AEABD01B7DC83F277D0E24CBE104F7290FF89077A7EB58646068EDFE1A83022866C46F65FB91612E516E0ECFA5CB25FC16B37D2C8D73732FE74CB2"),
                    new KnownValue(512, TestConstants.LoremIpsum, "F90D2E28E3A7E834FE6C3D27886D0E3E070586D1B6E5128FC260A909AB0176A17FB7D4AAF8396A6329C39B786BAF49E8393FDD986A08F6E5BC263F0CA4BF625A"),
                };
            }
        }

        protected override Blake2B CreateHashFunction(int hashSize)
        {
            return new Blake2B();
        }

        protected override Mock<Blake2B> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<Blake2B>();
        }
    }
    

    public class IHashFunctionAsyncTests_Blake2B_HashSizeConstructor
        : IHashFunctionAsyncTests<Blake2B>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(8, TestConstants.Empty, 0x2E),
                    new KnownValue(16, TestConstants.Empty, 0xFEB1),
                    new KnownValue(32, TestConstants.Empty, 0x25CF7112),
                    new KnownValue(64, TestConstants.Empty, 0xB4B2797457A0A6E4L),
                    new KnownValue(128, TestConstants.Empty, "CAE66941D9EFBD404E4D88758EA67670"),
                    new KnownValue(256, TestConstants.Empty, "0E5751C026E543B2E8AB2EB06099DAA1D1E5DF47778F7787FAAB45CDF12FE3A8"),
                    new KnownValue(512, TestConstants.Empty, "786A02F742015903C6C6FD852552D272912F4740E15847618A86E217F71F5419D25E1031AFEE585313896444934EB04B903A685B1448B755D56F701AFE9BE2CE"),
                    new KnownValue(8, TestConstants.FooBar, 0x13),
                    new KnownValue(16, TestConstants.FooBar, 0xDF5F),
                    new KnownValue(32, TestConstants.FooBar, 0xD839266A),
                    new KnownValue(64, TestConstants.FooBar, 0xF9514A257F2F219DL),
                    new KnownValue(128, TestConstants.FooBar, "13B16EEC2597E4D5616A70B1ABD318B0"),
                    new KnownValue(256, TestConstants.FooBar, "93A0E84A8CDD4166267DBE1263E937F08087723AC24E7DCC35B3D5941775EF47"),
                    new KnownValue(512, TestConstants.FooBar, "8DF31F60D6AEABD01B7DC83F277D0E24CBE104F7290FF89077A7EB58646068EDFE1A83022866C46F65FB91612E516E0ECFA5CB25FC16B37D2C8D73732FE74CB2"),
                    new KnownValue(8, TestConstants.LoremIpsum, 0xC8),
                    new KnownValue(16, TestConstants.LoremIpsum, 0xA0A7),
                    new KnownValue(32, TestConstants.LoremIpsum, 0xE8C02DA7),
                    new KnownValue(64, TestConstants.LoremIpsum, 0x3BEDA4F6319326A0L),
                    new KnownValue(128, TestConstants.LoremIpsum, "FAF29FE101A0014E917BB8CA6F6F42B1"),
                    new KnownValue(256, TestConstants.LoremIpsum, "07DEFAFE398706F9FBD5FABBE63F38D2177D5FE10710B633A1B2A1EF585DBD41"),
                    new KnownValue(512, TestConstants.LoremIpsum, "F90D2E28E3A7E834FE6C3D27886D0E3E070586D1B6E5128FC260A909AB0176A17FB7D4AAF8396A6329C39B786BAF49E8393FDD986A08F6E5BC263F0CA4BF625A"),
                };
            }
        }

        protected override Blake2B CreateHashFunction(int hashSize)
        {
            return new Blake2B(hashSize);
        }

        protected override Mock<Blake2B> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<Blake2B>(hashSize);
        }
    }
    

    public class IHashFunctionAsyncTests_Blake2B_FoobarAsKeyWithHashSizeConstructor
        : IHashFunctionAsyncTests<Blake2B>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(8, TestConstants.Empty, 0x1D),
                    new KnownValue(16, TestConstants.Empty, 0x73DC),
                    new KnownValue(32, TestConstants.Empty, 0x9A87498C),
                    new KnownValue(64, TestConstants.Empty, 0x9BFBF5F924B0A10DL),
                    new KnownValue(128, TestConstants.Empty, "1071A42316E10C29AE052635FDBB14F2"),
                    new KnownValue(256, TestConstants.Empty, "1DADAC7EA5D7F4122A26ABFD4171283EAD456C812ABA8BF089CC4C314F1BF9F2"),
                    new KnownValue(512, TestConstants.Empty, "B757DDC6AE9629278C5EF7747EA2FBB3324B88398F45C057CE5E3B23732CF35C627C948E07EA70CED77E77528F30A6F178FF59777C05D8D12F341A10A5AB2430"),
                    new KnownValue(8, TestConstants.FooBar, 0x42),
                    new KnownValue(16, TestConstants.FooBar, 0x891E),
                    new KnownValue(32, TestConstants.FooBar, 0x3B616D45),
                    new KnownValue(64, TestConstants.FooBar, 0x6F9D9A4C9AFD3F5EL),
                    new KnownValue(128, TestConstants.FooBar, "3D1760C86A25EB9C924ECCBB13076ECE"),
                    new KnownValue(256, TestConstants.FooBar, "73A66C48D128055BB22BCFF0D6316D3D5E829A2A1D9CB261581A7672803A6454"),
                    new KnownValue(512, TestConstants.FooBar, "47DC3AC6E0B7BFD83C653A92C8CBD9FF16A758DE82690483D90A6A905C917DF39691D54F3AF919A717379A87DEDF716192160BCE2EB5C02C775819ABD4066A12"),
                    new KnownValue(8, TestConstants.LoremIpsum, 0xB8),
                    new KnownValue(16, TestConstants.LoremIpsum, 0x80B6),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x936D150E),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xB630C582812A78FDL),
                    new KnownValue(128, TestConstants.LoremIpsum, "0E50AA3684C8B28871073DC540B8A36B"),
                    new KnownValue(256, TestConstants.LoremIpsum, "C065D225BD499FE9E7D2BEFD7588679AE9697EB3F0E43B63A474145720F33F74"),
                    new KnownValue(512, TestConstants.LoremIpsum, "B38035457007274B9BE1AAE5628744763E5D52F48CEF583A0FF24327630CDCE2D951C421E7350585782A92D0EA5C3F8DA20ECB1A04E6C25C5E8395C76589BCF2"),
                };
            }
        }

        protected override Blake2B CreateHashFunction(int hashSize)
        {
            return new Blake2B(TestConstants.FooBar, null, null, hashSize);
        }

        protected override Mock<Blake2B> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<Blake2B>(TestConstants.FooBar, null, null, hashSize);
        }
    }
    

    #endregion

    #region Data.HashFunction.BuzHash

    public class IHashFunctionAsyncTests_DefaultBuzHash
        : IHashFunctionAsyncTests<DefaultBuzHash>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(8, TestConstants.Empty, 0xd3),
                    new KnownValue(8, TestConstants.FooBar, 0xb2),
                    new KnownValue(8, TestConstants.LoremIpsum, 0x83),
                    new KnownValue(16, TestConstants.Empty, 0x37d3),
                    new KnownValue(16, TestConstants.FooBar, 0xd088),
                    new KnownValue(16, TestConstants.LoremIpsum, 0x284a),
                    new KnownValue(32, TestConstants.Empty, 0xfd0337d3),
                    new KnownValue(32, TestConstants.FooBar, 0xe3d9d0b6),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x2f16d6a7),
                    new KnownValue(64, TestConstants.Empty, 0x3cd05367fd0337d3),
                    new KnownValue(64, TestConstants.FooBar, 0xe8ebaa27e3d9d09f),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xfad1d8bf5108c8dd),
                };
            }
        }

        protected override DefaultBuzHash CreateHashFunction(int hashSize)
        {
            return new DefaultBuzHash(hashSize);
        }

        protected override Mock<DefaultBuzHash> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<DefaultBuzHash>(hashSize);
        }
    }
    

    public class IHashFunctionAsyncTests_DefaultBuzHash_DefaultConstructor
        : IHashFunctionAsyncTests<DefaultBuzHash>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(64, TestConstants.Empty, 0x3cd05367fd0337d3),
                    new KnownValue(64, TestConstants.FooBar, 0xe8ebaa27e3d9d09f),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xfad1d8bf5108c8dd),
                };
            }
        }

        protected override DefaultBuzHash CreateHashFunction(int hashSize)
        {
            return new DefaultBuzHash();
        }

        protected override Mock<DefaultBuzHash> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<DefaultBuzHash>();
        }
    }
    

    public class IHashFunctionAsyncTests_DefaultBuzHash_RightShift
        : IHashFunctionAsyncTests<DefaultBuzHash>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(8, TestConstants.Empty, 0xd3),
                    new KnownValue(8, TestConstants.FooBar, 0x8b),
                    new KnownValue(8, TestConstants.LoremIpsum, 0x97),
                    new KnownValue(16, TestConstants.Empty, 0x37d3),
                    new KnownValue(16, TestConstants.FooBar, 0xe16b),
                    new KnownValue(16, TestConstants.LoremIpsum, 0x076f),
                    new KnownValue(32, TestConstants.Empty, 0xfd0337d3),
                    new KnownValue(32, TestConstants.FooBar, 0xd2d7f16b),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x36d306ab),
                    new KnownValue(64, TestConstants.Empty, 0x3cd05367fd0337d3),
                    new KnownValue(64, TestConstants.FooBar, 0x7f44cd6ecad7f16b),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xb826c048cb5399a3),
                };
            }
        }

        protected override DefaultBuzHash CreateHashFunction(int hashSize)
        {
            return new DefaultBuzHash(BuzHashBase.CircularShiftDirection.Right, hashSize);
        }

        protected override Mock<DefaultBuzHash> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<DefaultBuzHash>(BuzHashBase.CircularShiftDirection.Right, hashSize);
        }
    }
    

    public class IHashFunctionAsyncTests_DefaultBuzHash_RightShift_DefaultConstructor
        : IHashFunctionAsyncTests<DefaultBuzHash>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(64, TestConstants.Empty, 0x3cd05367fd0337d3),
                    new KnownValue(64, TestConstants.FooBar, 0x7f44cd6ecad7f16b),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xb826c048cb5399a3),
                };
            }
        }

        protected override DefaultBuzHash CreateHashFunction(int hashSize)
        {
            return new DefaultBuzHash(BuzHashBase.CircularShiftDirection.Right);
        }

        protected override Mock<DefaultBuzHash> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<DefaultBuzHash>(BuzHashBase.CircularShiftDirection.Right);
        }
    }
    

    public class IHashFunctionAsyncTests_BuzHashBaseImpl
        : IHashFunctionAsyncTests<BuzHashBaseImpl>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(8, TestConstants.Empty, 0x00),
                    new KnownValue(8, TestConstants.FooBar, 0x00),
                    new KnownValue(8, TestConstants.LoremIpsum, 0x00),
                    new KnownValue(16, TestConstants.Empty, 0x0000),
                    new KnownValue(16, TestConstants.FooBar, 0x0000),
                    new KnownValue(16, TestConstants.LoremIpsum, 0x0000),
                    new KnownValue(32, TestConstants.Empty, 0x00000000),
                    new KnownValue(32, TestConstants.FooBar, 0x00000000),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x00000000),
                    new KnownValue(64, TestConstants.Empty, 0x0000000000000000L),
                    new KnownValue(64, TestConstants.FooBar, 0x0000000000000000L),
                    new KnownValue(64, TestConstants.LoremIpsum, 0x0000000000000000L),
                };
            }
        }

        protected override BuzHashBaseImpl CreateHashFunction(int hashSize)
        {
            return new BuzHashBaseImpl(hashSize);
        }

        protected override Mock<BuzHashBaseImpl> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<BuzHashBaseImpl>(hashSize);
        }
    }
    

    public class IHashFunctionAsyncTests_BuzHashBaseImpl_DefaultConstructor
        : IHashFunctionAsyncTests<BuzHashBaseImpl>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(64, TestConstants.Empty, 0x0000000000000000L),
                    new KnownValue(64, TestConstants.FooBar, 0x0000000000000000L),
                    new KnownValue(64, TestConstants.LoremIpsum, 0x0000000000000000L),
                };
            }
        }

        protected override BuzHashBaseImpl CreateHashFunction(int hashSize)
        {
            return new BuzHashBaseImpl();
        }

        protected override Mock<BuzHashBaseImpl> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<BuzHashBaseImpl>();
        }
    }
    

    #endregion

    #region Data.HashFunction.CityHash

    public class IHashFunctionAsyncTests_CityHash
        : IHashFunctionAsyncTests<CityHash>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0xdc56d17a),
                    new KnownValue(32, TestConstants.FooBar, 0xe2f34cdf),
                    new KnownValue(32, TestConstants.LoremIpsum, 0xc2ebd64e),
                    new KnownValue(32, TestConstants.RandomShort, 0x1fcea779),
                    new KnownValue(32, TestConstants.RandomLong, 0x9dba44d0),
                    new KnownValue(64, TestConstants.Empty, 0x9ae16a3b2f90404f),
                    new KnownValue(64, TestConstants.FooBar, 0xc43fb29ab5effcfe),
                    new KnownValue(64, TestConstants.LoremIpsum, 0x764df1e17d92d1eb),
                    new KnownValue(64, TestConstants.RandomShort, 0x3ef8698eae651b16),
                    new KnownValue(64, TestConstants.RandomLong, 0x39e9fcdba69979b0),
                    new KnownValue(128, TestConstants.Empty, "2b9ac064fc9df03d291ee592c340b53c"),
                    new KnownValue(128, TestConstants.FooBar, "5064c017cf2c1672daa1f13a15b78c98"),
                    new KnownValue(128, TestConstants.LoremIpsum, "31dd5cb57a6c29dc0826565eeb0cf6a4"),
                    new KnownValue(128, TestConstants.RandomShort, "67cbf6f803487e7e09bffce371172c13"),
                    new KnownValue(128, TestConstants.RandomLong, "98999f077f446a5ee962148d86279ea0"),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(3), 0xd83c2fa0),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(3), 0xfa10ac780bf932dd),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(3), "8b5529b80301a1c414cc313959fd5255"),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(23), 0xa6480aae),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(23), 0x3a03aa21105c4286),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(23), "658f52c24d66d71d844823de90c3d9ac"),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(64), 0x8ace2a1a),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(64), 0x2167be8daa61f94d),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(64), "50e89788e0dbb5d6784fbcbdf57264d1"),
                };
            }
        }

        protected override CityHash CreateHashFunction(int hashSize)
        {
            return new CityHash(hashSize);
        }

        protected override Mock<CityHash> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CityHash>(hashSize);
        }
    }
    

    public class IHashFunctionAsyncTests_CityHash_DefaultConstructor
        : IHashFunctionAsyncTests<CityHash>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0xdc56d17a),
                    new KnownValue(32, TestConstants.FooBar, 0xe2f34cdf),
                    new KnownValue(32, TestConstants.LoremIpsum, 0xc2ebd64e),
                    new KnownValue(32, TestConstants.RandomShort, 0x1fcea779),
                    new KnownValue(32, TestConstants.RandomLong, 0x9dba44d0),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(3), 0xd83c2fa0),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(23), 0xa6480aae),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(64), 0x8ace2a1a),
                };
            }
        }

        protected override CityHash CreateHashFunction(int hashSize)
        {
            return new CityHash();
        }

        protected override Mock<CityHash> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<CityHash>();
        }
    }
    

    #endregion

    #region Data.HashFunction.Core

    public class IHashFunctionAsyncTests_HashFunctionImpl
        : IHashFunctionAsyncTests<HashFunctionImpl>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(0, TestConstants.Empty, new byte[0]),
                };
            }
        }

        protected override HashFunctionImpl CreateHashFunction(int hashSize)
        {
            return new HashFunctionImpl();
        }

        protected override Mock<HashFunctionImpl> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<HashFunctionImpl>();
        }
    }
    

    public class IHashFunctionTests_HashAlgorithmWrapper_SHA1
        : IHashFunctionTests<HashAlgorithmWrapper>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(160, TestConstants.Empty, "da39a3ee5e6b4b0d3255bfef95601890afd80709"),
                    new KnownValue(160, TestConstants.FooBar, "8843d7f92416211de9ebb963ff4ce28125932878"),
                    new KnownValue(160, TestConstants.LoremIpsum, "2dd4010f15f21c9e26e31a693ba31c6ab78a5a4c"),
                    new KnownValue(160, TestConstants.RandomShort, "d64df40c72068b01e7dfb5ceb2b519ad3b483eb0"),
                    new KnownValue(160, TestConstants.RandomLong, "e5901cb4679133729c5555210c3cfe3e5851a2aa"),
                };
            }
        }

        protected override HashAlgorithmWrapper CreateHashFunction(int hashSize)
        {
            return new HashAlgorithmWrapper(new SHA1Managed());
        }

        protected override Mock<HashAlgorithmWrapper> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<HashAlgorithmWrapper>(new SHA1Managed());
        }
    }
    

    public class IHashFunctionTests_HashAlgorithmWrapper_SHA256
        : IHashFunctionTests<HashAlgorithmWrapper>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(256, TestConstants.FooBar, "c3ab8ff13720e8ad9047dd39466b3c8974e592c2fa383d4a3960714caef0c4f2"),
                };
            }
        }

        protected override HashAlgorithmWrapper CreateHashFunction(int hashSize)
        {
            return new HashAlgorithmWrapper(new SHA256Managed());
        }

        protected override Mock<HashAlgorithmWrapper> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<HashAlgorithmWrapper>(new SHA256Managed());
        }
    }
    

    public class IHashFunctionTests_HashAlgorithmWrapper_SHA384
        : IHashFunctionTests<HashAlgorithmWrapper>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(384, TestConstants.FooBar, "3c9c30d9f665e74d515c842960d4a451c83a0125fd3de7392d7b37231af10c72ea58aedfcdf89a5765bf902af93ecf06"),
                };
            }
        }

        protected override HashAlgorithmWrapper CreateHashFunction(int hashSize)
        {
            return new HashAlgorithmWrapper(new SHA384Managed());
        }

        protected override Mock<HashAlgorithmWrapper> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<HashAlgorithmWrapper>(new SHA384Managed());
        }
    }
    

    public class IHashFunctionTests_HashAlgorithmWrapper_SHA512
        : IHashFunctionTests<HashAlgorithmWrapper>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(512, TestConstants.FooBar, "0a50261ebd1a390fed2bf326f2673c145582a6342d523204973d0219337f81616a8069b012587cf5635f6925f1b56c360230c19b273500ee013e030601bf2425"),
                };
            }
        }

        protected override HashAlgorithmWrapper CreateHashFunction(int hashSize)
        {
            return new HashAlgorithmWrapper(new SHA512Managed());
        }

        protected override Mock<HashAlgorithmWrapper> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<HashAlgorithmWrapper>(new SHA512Managed());
        }
    }
    

    public class IHashFunctionTests_HashAlgorithmWrapper_MD5
        : IHashFunctionTests<HashAlgorithmWrapper>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(128, TestConstants.FooBar, "3858f62230ac3c915f300c664312c63f"),
                };
            }
        }

        protected override HashAlgorithmWrapper CreateHashFunction(int hashSize)
        {
            return new HashAlgorithmWrapper(new MD5CryptoServiceProvider());
        }

        protected override Mock<HashAlgorithmWrapper> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<HashAlgorithmWrapper>(new MD5CryptoServiceProvider());
        }
    }
    

    public class IHashFunctionTests_HashAlgorithmWrapper_RIPEMD160
        : IHashFunctionTests<HashAlgorithmWrapper>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(160, TestConstants.FooBar, "a06e327ea7388c18e4740e350ed4e60f2e04fc41"),
                };
            }
        }

        protected override HashAlgorithmWrapper CreateHashFunction(int hashSize)
        {
            return new HashAlgorithmWrapper(new RIPEMD160Managed());
        }

        protected override Mock<HashAlgorithmWrapper> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<HashAlgorithmWrapper>(new RIPEMD160Managed());
        }
    }
    

    public class IHashFunctionTests_HashAlgorithmWrapper_SHA1Managed
        : IHashFunctionTests<HashAlgorithmWrapper<SHA1Managed>>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(160, TestConstants.Empty, "da39a3ee5e6b4b0d3255bfef95601890afd80709"),
                    new KnownValue(160, TestConstants.FooBar, "8843d7f92416211de9ebb963ff4ce28125932878"),
                    new KnownValue(160, TestConstants.LoremIpsum, "2dd4010f15f21c9e26e31a693ba31c6ab78a5a4c"),
                    new KnownValue(160, TestConstants.RandomShort, "d64df40c72068b01e7dfb5ceb2b519ad3b483eb0"),
                    new KnownValue(160, TestConstants.RandomLong, "e5901cb4679133729c5555210c3cfe3e5851a2aa"),
                };
            }
        }

        protected override HashAlgorithmWrapper<SHA1Managed> CreateHashFunction(int hashSize)
        {
            return new HashAlgorithmWrapper<SHA1Managed>();
        }

        protected override Mock<HashAlgorithmWrapper<SHA1Managed>> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<HashAlgorithmWrapper<SHA1Managed>>();
        }
    }
    

    public class IHashFunctionTests_HashAlgorithmWrapper_SHA256Managed
        : IHashFunctionTests<HashAlgorithmWrapper<SHA256Managed>>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(256, TestConstants.FooBar, "c3ab8ff13720e8ad9047dd39466b3c8974e592c2fa383d4a3960714caef0c4f2"),
                };
            }
        }

        protected override HashAlgorithmWrapper<SHA256Managed> CreateHashFunction(int hashSize)
        {
            return new HashAlgorithmWrapper<SHA256Managed>();
        }

        protected override Mock<HashAlgorithmWrapper<SHA256Managed>> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<HashAlgorithmWrapper<SHA256Managed>>();
        }
    }
    

    public class IHashFunctionTests_HashAlgorithmWrapper_SHA384Managed
        : IHashFunctionTests<HashAlgorithmWrapper<SHA384Managed>>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(384, TestConstants.FooBar, "3c9c30d9f665e74d515c842960d4a451c83a0125fd3de7392d7b37231af10c72ea58aedfcdf89a5765bf902af93ecf06"),
                };
            }
        }

        protected override HashAlgorithmWrapper<SHA384Managed> CreateHashFunction(int hashSize)
        {
            return new HashAlgorithmWrapper<SHA384Managed>();
        }

        protected override Mock<HashAlgorithmWrapper<SHA384Managed>> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<HashAlgorithmWrapper<SHA384Managed>>();
        }
    }
    

    public class IHashFunctionTests_HashAlgorithmWrapper_SHA512Managed
        : IHashFunctionTests<HashAlgorithmWrapper<SHA512Managed>>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(512, TestConstants.FooBar, "0a50261ebd1a390fed2bf326f2673c145582a6342d523204973d0219337f81616a8069b012587cf5635f6925f1b56c360230c19b273500ee013e030601bf2425"),
                };
            }
        }

        protected override HashAlgorithmWrapper<SHA512Managed> CreateHashFunction(int hashSize)
        {
            return new HashAlgorithmWrapper<SHA512Managed>();
        }

        protected override Mock<HashAlgorithmWrapper<SHA512Managed>> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<HashAlgorithmWrapper<SHA512Managed>>();
        }
    }
    

    public class IHashFunctionTests_HashAlgorithmWrapper_MD5CryptoServiceProvider
        : IHashFunctionTests<HashAlgorithmWrapper<MD5CryptoServiceProvider>>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(128, TestConstants.FooBar, "3858f62230ac3c915f300c664312c63f"),
                };
            }
        }

        protected override HashAlgorithmWrapper<MD5CryptoServiceProvider> CreateHashFunction(int hashSize)
        {
            return new HashAlgorithmWrapper<MD5CryptoServiceProvider>();
        }

        protected override Mock<HashAlgorithmWrapper<MD5CryptoServiceProvider>> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<HashAlgorithmWrapper<MD5CryptoServiceProvider>>();
        }
    }
    

    public class IHashFunctionTests_HashAlgorithmWrapper_RIPEMD160Managed
        : IHashFunctionTests<HashAlgorithmWrapper<RIPEMD160Managed>>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(160, TestConstants.FooBar, "a06e327ea7388c18e4740e350ed4e60f2e04fc41"),
                };
            }
        }

        protected override HashAlgorithmWrapper<RIPEMD160Managed> CreateHashFunction(int hashSize)
        {
            return new HashAlgorithmWrapper<RIPEMD160Managed>();
        }

        protected override Mock<HashAlgorithmWrapper<RIPEMD160Managed>> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<HashAlgorithmWrapper<RIPEMD160Managed>>();
        }
    }
    

    #endregion

    #region Data.HashFunction.ELF64

    public class IHashFunctionAsyncTests_ELF64
        : IHashFunctionAsyncTests<ELF64>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0x00000000),
                    new KnownValue(32, TestConstants.FooBar, 0x06d65882),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x09e0a53e),
                };
            }
        }

        protected override ELF64 CreateHashFunction(int hashSize)
        {
            return new ELF64();
        }

        protected override Mock<ELF64> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<ELF64>();
        }
    }
    

    #endregion

    #region Data.HashFunction.FNV

    public class IHashFunctionAsyncTests_FNV1
        : IHashFunctionAsyncTests<FNV1>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0x811c9dc5),
                    new KnownValue(32, TestConstants.FooBar, 0x31f0b262),
                    new KnownValue(32, TestConstants.LoremIpsum, 0xe1fb1efb),
                    new KnownValue(64, TestConstants.Empty, 0xcbf29ce484222325),
                    new KnownValue(64, TestConstants.FooBar, 0x340d8765a4dda9c2),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xe1d37010556d091b),
                    new KnownValue(128, TestConstants.Empty, "8dc595627521b8624201bb072e27626c"),
                    new KnownValue(128, TestConstants.FooBar, "aa93c2d25383c56dbf643c9ceabf9678"),
                    new KnownValue(128, TestConstants.LoremIpsum, "130c122234e097c352c819800a56ea75"),
                };
            }
        }

        protected override FNV1 CreateHashFunction(int hashSize)
        {
            return new FNV1(hashSize);
        }

        protected override Mock<FNV1> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<FNV1>(hashSize);
        }
    }
    

    public class IHashFunctionAsyncTests_FNV1_DefaultConstructor
        : IHashFunctionAsyncTests<FNV1>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(64, TestConstants.Empty, 0xcbf29ce484222325),
                    new KnownValue(64, TestConstants.FooBar, 0x340d8765a4dda9c2),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xe1d37010556d091b),
                };
            }
        }

        protected override FNV1 CreateHashFunction(int hashSize)
        {
            return new FNV1();
        }

        protected override Mock<FNV1> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<FNV1>();
        }
    }
    

    public class IHashFunctionAsyncTests_FNV1a
        : IHashFunctionAsyncTests<FNV1a>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0x811c9dc5),
                    new KnownValue(32, TestConstants.FooBar, 0xbf9cf968),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x15cc3fdb),
                    new KnownValue(64, TestConstants.Empty, 0xcbf29ce484222325),
                    new KnownValue(64, TestConstants.FooBar, 0x85944171f73967e8),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xd9daf73d6af17cfb),
                    new KnownValue(128, TestConstants.Empty, "8dc595627521b8624201bb072e27626c"),
                    new KnownValue(128, TestConstants.FooBar, "186f44ba97350d6fbf643c7962163e34"),
                    new KnownValue(128, TestConstants.LoremIpsum, "b3db4ee71f492ed1c2166a4bccdce8b6"),
                };
            }
        }

        protected override FNV1a CreateHashFunction(int hashSize)
        {
            return new FNV1a(hashSize);
        }

        protected override Mock<FNV1a> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<FNV1a>(hashSize);
        }
    }
    

    public class IHashFunctionAsyncTests_FNV1a_DefaultConstructor
        : IHashFunctionAsyncTests<FNV1a>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(64, TestConstants.Empty, 0xcbf29ce484222325),
                    new KnownValue(64, TestConstants.FooBar, 0x85944171f73967e8),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xd9daf73d6af17cfb),
                };
            }
        }

        protected override FNV1a CreateHashFunction(int hashSize)
        {
            return new FNV1a();
        }

        protected override Mock<FNV1a> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<FNV1a>();
        }
    }
    

    #endregion

    #region Data.HashFunction.Jenkins

    public class IHashFunctionAsyncTests_JenkinsOneAtATime
        : IHashFunctionAsyncTests<JenkinsOneAtATime>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0xf952fde7),
                };
            }
        }

        protected override JenkinsOneAtATime CreateHashFunction(int hashSize)
        {
            return new JenkinsOneAtATime();
        }

        protected override Mock<JenkinsOneAtATime> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<JenkinsOneAtATime>();
        }
    }
    

    public class IHashFunctionAsyncTests_JenkinsLookup2
        : IHashFunctionAsyncTests<JenkinsLookup2>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0xbd49d10d),
                    new KnownValue(32, TestConstants.FooBar, 0x9d3ffa02),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(15), 0x0c39787e),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(19), 0x2a06cf89),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(23), 0xa4fd862c),
                };
            }
        }

        protected override JenkinsLookup2 CreateHashFunction(int hashSize)
        {
            return new JenkinsLookup2();
        }

        protected override Mock<JenkinsLookup2> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<JenkinsLookup2>();
        }
    }
    

    public class IHashFunctionAsyncTests_JenkinsLookup2_WithInitVal
        : IHashFunctionAsyncTests<JenkinsLookup2>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0x6117ff85),
                };
            }
        }

        protected override JenkinsLookup2 CreateHashFunction(int hashSize)
        {
            return new JenkinsLookup2(0x7da236b9U);
        }

        protected override Mock<JenkinsLookup2> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<JenkinsLookup2>(0x7da236b9U);
        }
    }
    

    public class IHashFunctionAsyncTests_JenkinsLookup3
        : IHashFunctionAsyncTests<JenkinsLookup3>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0xaeb72b0c),
                    new KnownValue(64, TestConstants.FooBar, 0xacf2fd4caeb72b0c),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(12), 0x8e663aee),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(12), 0xb6d69c8c8e663aee),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(15), 0x9b484ed5),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(15), 0x14b71e4d9b484ed5),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(19), 0xad9cba6f),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(19), 0x2807920ead9cba6f),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(23), 0x57c46621),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(23), 0x01e860f357c46621),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(24), 0xf2b662ef),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(24), 0xcad6d781f2b662ef),
                };
            }
        }

        protected override JenkinsLookup3 CreateHashFunction(int hashSize)
        {
            return new JenkinsLookup3(hashSize);
        }

        protected override Mock<JenkinsLookup3> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<JenkinsLookup3>(hashSize);
        }
    }
    

    public class IHashFunctionAsyncTests_JenkinsLookup3_DefaultConstructor
        : IHashFunctionAsyncTests<JenkinsLookup3>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0xaeb72b0c),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(12), 0x8e663aee),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(15), 0x9b484ed5),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(19), 0xad9cba6f),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(23), 0x57c46621),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(24), 0xf2b662ef),
                };
            }
        }

        protected override JenkinsLookup3 CreateHashFunction(int hashSize)
        {
            return new JenkinsLookup3();
        }

        protected override Mock<JenkinsLookup3> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<JenkinsLookup3>();
        }
    }
    

    public class IHashFunctionAsyncTests_JenkinsLookup3_WithInitVals
        : IHashFunctionAsyncTests<JenkinsLookup3>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0x7306b2fc),
                    new KnownValue(64, TestConstants.FooBar, 0x28b9e020444d6de2),
                };
            }
        }

        protected override JenkinsLookup3 CreateHashFunction(int hashSize)
        {
            return new JenkinsLookup3(hashSize, 0x7da236b9U, 0x87930b75U);
        }

        protected override Mock<JenkinsLookup3> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<JenkinsLookup3>(hashSize, 0x7da236b9U, 0x87930b75U);
        }
    }
    

    public class IHashFunctionAsyncTests_JenkinsLookup3_WithInitVals_DefaultHashSize
        : IHashFunctionAsyncTests<JenkinsLookup3>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0x7306b2fc),
                };
            }
        }

        protected override JenkinsLookup3 CreateHashFunction(int hashSize)
        {
            return new JenkinsLookup3(0x7da236b9U, 0x87930b75U);
        }

        protected override Mock<JenkinsLookup3> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<JenkinsLookup3>(0x7da236b9U, 0x87930b75U);
        }
    }
    

    #endregion

    #region Data.HashFunction.MurmurHash

    public class IHashFunctionAsyncTests_MurmurHash1
        : IHashFunctionAsyncTests<MurmurHash1>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0x5c45f49b),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(7), 0xff3a37aa),
                };
            }
        }

        protected override MurmurHash1 CreateHashFunction(int hashSize)
        {
            return new MurmurHash1();
        }

        protected override Mock<MurmurHash1> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<MurmurHash1>();
        }
    }
    

    public class IHashFunctionAsyncTests_MurmurHash2
        : IHashFunctionAsyncTests<MurmurHash2>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0x6715a92e),
                    new KnownValue(64, TestConstants.FooBar, 0xd49f461720d7a196),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(15), 0x384025b5),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(11), 0xa4d1d1c83f3125d2),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(15), 0xfa38fed50a3dc771),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(23), 0x82d0eccc1172c984),
                };
            }
        }

        protected override MurmurHash2 CreateHashFunction(int hashSize)
        {
            return new MurmurHash2(hashSize);
        }

        protected override Mock<MurmurHash2> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<MurmurHash2>(hashSize);
        }
    }
    

    public class IHashFunctionAsyncTests_MurmurHash2_DefaultConstructor
        : IHashFunctionAsyncTests<MurmurHash2>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(64, TestConstants.FooBar, 0xd49f461720d7a196),
                    new KnownValue(64, TestConstants.LoremIpsum.Take(15), 0xfa38fed50a3dc771),
                };
            }
        }

        protected override MurmurHash2 CreateHashFunction(int hashSize)
        {
            return new MurmurHash2();
        }

        protected override Mock<MurmurHash2> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<MurmurHash2>();
        }
    }
    

    public class IHashFunctionAsyncTests_MurmurHash3
        : IHashFunctionAsyncTests<MurmurHash3>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0xa4c4d4bd),
                    new KnownValue(128, TestConstants.FooBar, "455ac81671aed2bdafd6f8bae055a274"),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(7), 0x5a1d7378),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(7), "1689190f13f3290b3c5ead34c751ea8a"),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(31), 0x8d50f530),
                    new KnownValue(128, TestConstants.LoremIpsum.Take(31), "5c769a439b78878e8640e16335e4313f"),
                };
            }
        }

        protected override MurmurHash3 CreateHashFunction(int hashSize)
        {
            return new MurmurHash3(hashSize);
        }

        protected override Mock<MurmurHash3> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<MurmurHash3>(hashSize);
        }
    }
    

    public class IHashFunctionAsyncTests_MurmurHash3_DefaultConstructor
        : IHashFunctionAsyncTests<MurmurHash3>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0xa4c4d4bd),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(7), 0x5a1d7378),
                    new KnownValue(32, TestConstants.LoremIpsum.Take(31), 0x8d50f530),
                };
            }
        }

        protected override MurmurHash3 CreateHashFunction(int hashSize)
        {
            return new MurmurHash3();
        }

        protected override Mock<MurmurHash3> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<MurmurHash3>();
        }
    }
    

    #endregion

    #region Data.HashFunction.Pearson

    public class IHashFunctionAsyncTests_WikipediaPearson
        : IHashFunctionAsyncTests<WikipediaPearson>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(8, TestConstants.Empty, 0x00),
                    new KnownValue(16, TestConstants.Empty, 0x0000),
                    new KnownValue(24, TestConstants.Empty, 0x000000),
                    new KnownValue(32, TestConstants.Empty, 0x00000000),
                    new KnownValue(40, TestConstants.Empty, 0x0000000000L),
                    new KnownValue(48, TestConstants.Empty, 0x000000000000L),
                    new KnownValue(56, TestConstants.Empty, 0x00000000000000L),
                    new KnownValue(64, TestConstants.Empty, 0x0000000000000000L),
                    new KnownValue(128, TestConstants.Empty, "00000000000000000000000000000000"),
                    new KnownValue(256, TestConstants.Empty, "0000000000000000000000000000000000000000000000000000000000000000"),
                    new KnownValue(512, TestConstants.Empty, "00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"),
                    new KnownValue(1024, TestConstants.Empty, "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"),
                    new KnownValue(2040, TestConstants.Empty, "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"),
                    new KnownValue(8, TestConstants.FooBar, 0xac),
                    new KnownValue(16, TestConstants.FooBar, 0xcfac),
                    new KnownValue(24, TestConstants.FooBar, 0xf2cfac),
                    new KnownValue(32, TestConstants.FooBar, 0x36f2cfac),
                    new KnownValue(40, TestConstants.FooBar, 0x0136f2cfac),
                    new KnownValue(48, TestConstants.FooBar, 0xc20136f2cfac),
                    new KnownValue(56, TestConstants.FooBar, 0xa3c20136f2cfac),
                    new KnownValue(64, TestConstants.FooBar, 0xdda3c20136f2cfac),
                    new KnownValue(128, TestConstants.FooBar, "accff23601c2a3dd42eee467c3c0953f"),
                    new KnownValue(256, TestConstants.FooBar, "accff23601c2a3dd42eee467c3c0953f4cd1359c80c9ad8c5232e29b6d5df7a4"),
                    new KnownValue(512, TestConstants.FooBar, "accff23601c2a3dd42eee467c3c0953f4cd1359c80c9ad8c5232e29b6d5df7a4b4e9695a04db341c64d53723b1dcb631e5de72b2626c3d4ad3c1df1d3c89f0f5"),
                    new KnownValue(1024, TestConstants.FooBar, "accff23601c2a3dd42eee467c3c0953f4cd1359c80c9ad8c5232e29b6d5df7a4b4e9695a04db341c64d53723b1dcb631e5de72b2626c3d4ad3c1df1d3c89f0f5dae0befd498428c465d6a6186a1f51d492a2edf44ea12d2774f95e1798b8588f0bf1e1a082a7ca85ae55593a078b60d9914b6f4353e87341766824b097b92996"),
                    new KnownValue(2040, TestConstants.FooBar, "accff23601c2a3dd42eee467c3c0953f4cd1359c80c9ad8c5232e29b6d5df7a4b4e9695a04db341c64d53723b1dcb631e5de72b2626c3d4ad3c1df1d3c89f0f5dae0befd498428c465d6a6186a1f51d492a2edf44ea12d2774f95e1798b8588f0bf1e1a082a7ca85ae55593a078b60d9914b6f4353e87341766824b097b92996149dbf20d8af8eec56619afb19e3005f471e7c25c80a939f3b1bba5cbcb77eaa7a8a83d0e7992112eab5bdc605775433882bd77fcbabc7a52c0d70eb3938661a098d7b5081fc4445b371c5ef9e0ce602782a1506cefa0fa9164d7dcd86030e9075f62611ccd22ef8081379a863ff30bb6e104f2f229487f357406b463e485b"),
                    new KnownValue(8, TestConstants.LoremIpsum, 0x92),
                    new KnownValue(16, TestConstants.LoremIpsum, 0x9f92),
                    new KnownValue(24, TestConstants.LoremIpsum, 0xba9f92),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x73ba9f92),
                    new KnownValue(40, TestConstants.LoremIpsum, 0x1073ba9f92),
                    new KnownValue(48, TestConstants.LoremIpsum, 0xc91073ba9f92),
                    new KnownValue(56, TestConstants.LoremIpsum, 0x58c91073ba9f92),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xdf58c91073ba9f92),
                    new KnownValue(128, TestConstants.LoremIpsum, "929fba7310c958df517cf4dbfd760a8c"),
                    new KnownValue(256, TestConstants.LoremIpsum, "929fba7310c958df517cf4dbfd760a8c9c871fe9aef875ed848ebbdd2bf505e3"),
                    new KnownValue(512, TestConstants.LoremIpsum, "929fba7310c958df517cf4dbfd760a8c9c871fe9aef875ed848ebbdd2bf505e343b9ea988152abace8a54d1b784828674199a2b1e5324779868012694c2fda44"),
                    new KnownValue(1024, TestConstants.LoremIpsum, "929fba7310c958df517cf4dbfd760a8c9c871fe9aef875ed848ebbdd2bf505e343b9ea988152abace8a54d1b784828674199a2b1e5324779868012694c2fda4416de2ad7a76c4f19043e62087ec00230ccc53be4661c9eb0944bca3a886af26bb5fed0e714bd978f3cd489a9d1574a45c1d2bf7deb27c7617b9359ef9d9631a8"),
                    new KnownValue(2040, TestConstants.LoremIpsum, "929fba7310c958df517cf4dbfd760a8c9c871fe9aef875ed848ebbdd2bf505e343b9ea988152abace8a54d1b784828674199a2b1e5324779868012694c2fda4416de2ad7a76c4f19043e62087ec00230ccc53be4661c9eb0944bca3a886af26bb5fed0e714bd978f3cd489a9d1574a45c1d2bf7deb27c7617b9359ef9d9631a877182d83cb03ff251ef6132cf7503dd811266d538535d60639c8f37fe164faa69024c6ada10e2334cd0c4936fc420fe00154918bf0c24007f1b3b6705c56156fd355eed900fb097460711de29a461ac45e9b22d5e68d0bafa3c3955d68823fec205b21b8b2dca0cf5f65aa29637a2e0dce8a3772bea4b46e4ebc17b7f93833"),
                };
            }
        }

        protected override WikipediaPearson CreateHashFunction(int hashSize)
        {
            return new WikipediaPearson(hashSize);
        }

        protected override Mock<WikipediaPearson> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<WikipediaPearson>(hashSize);
        }
    }
    

    public class IHashFunctionAsyncTests_WikipediaPearson_DefaultConstructor
        : IHashFunctionAsyncTests<WikipediaPearson>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(8, TestConstants.Empty, 0x00),
                    new KnownValue(8, TestConstants.FooBar, 0xac),
                    new KnownValue(8, TestConstants.LoremIpsum, 0x92),
                };
            }
        }

        protected override WikipediaPearson CreateHashFunction(int hashSize)
        {
            return new WikipediaPearson();
        }

        protected override Mock<WikipediaPearson> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<WikipediaPearson>();
        }
    }
    

    #endregion

    #region Data.HashFunction.SpookyHash

    public class IHashFunctionAsyncTests_SpookyHashV1
        : IHashFunctionAsyncTests<SpookyHashV1>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0xd019a52d),
                    new KnownValue(64, TestConstants.FooBar, 0x52919208d019a52d),
                    new KnownValue(128, TestConstants.FooBar, "2da519d0089291529c22f24a80017a5e"),
                    new KnownValue(32, TestConstants.LoremIpsum, 0xcc79cd7e),
                    new KnownValue(64, TestConstants.LoremIpsum, 0x1c7efd4ccc79cd7e),
                    new KnownValue(128, TestConstants.LoremIpsum, "7ecd79cc4cfd7e1c5c15710c2d261311"),
                };
            }
        }

        protected override SpookyHashV1 CreateHashFunction(int hashSize)
        {
            return new SpookyHashV1(hashSize);
        }

        protected override Mock<SpookyHashV1> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<SpookyHashV1>(hashSize);
        }
    }
    

    public class IHashFunctionAsyncTests_SpookyHashV1_DefaultConstructor
        : IHashFunctionAsyncTests<SpookyHashV1>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(128, TestConstants.FooBar, "2da519d0089291529c22f24a80017a5e"),
                    new KnownValue(128, TestConstants.LoremIpsum, "7ecd79cc4cfd7e1c5c15710c2d261311"),
                };
            }
        }

        protected override SpookyHashV1 CreateHashFunction(int hashSize)
        {
            return new SpookyHashV1();
        }

        protected override Mock<SpookyHashV1> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<SpookyHashV1>();
        }
    }
    

    public class IHashFunctionAsyncTests_SpookyHashV1_WithInitVals
        : IHashFunctionAsyncTests<SpookyHashV1>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0xddf2894b),
                    new KnownValue(64, TestConstants.FooBar, 0x35d04f6cddf2894b),
                    new KnownValue(128, TestConstants.FooBar, "2ffa3a68544614fc258f142b35dfb07a"),
                };
            }
        }

        protected override SpookyHashV1 CreateHashFunction(int hashSize)
        {
            return new SpookyHashV1(hashSize, 0x7da236b987930b75U, 0x2eb994a3851d2f54U);
        }

        protected override Mock<SpookyHashV1> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<SpookyHashV1>(hashSize, 0x7da236b987930b75U, 0x2eb994a3851d2f54U);
        }
    }
    

    public class IHashFunctionAsyncTests_SpookyHashV1_WithInitVals_DefaultHashSize
        : IHashFunctionAsyncTests<SpookyHashV1>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(128, TestConstants.FooBar, "2ffa3a68544614fc258f142b35dfb07a"),
                };
            }
        }

        protected override SpookyHashV1 CreateHashFunction(int hashSize)
        {
            return new SpookyHashV1(0x7da236b987930b75U, 0x2eb994a3851d2f54U);
        }

        protected override Mock<SpookyHashV1> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<SpookyHashV1>(0x7da236b987930b75U, 0x2eb994a3851d2f54U);
        }
    }
    

    public class IHashFunctionAsyncTests_SpookyHashV2
        : IHashFunctionAsyncTests<SpookyHashV2>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0xcef04070),
                    new KnownValue(64, TestConstants.FooBar, 0x1f9b9f79cef04070),
                    new KnownValue(128, TestConstants.FooBar, "7040f0ce799f9b1f22020c7b2be86797"),
                    new KnownValue(32, TestConstants.LoremIpsum, 0xded54c84),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xbb6b988bded54c84),
                    new KnownValue(128, TestConstants.LoremIpsum, "844cd5de8b986bbb1062913785ea1fa2"),
                };
            }
        }

        protected override SpookyHashV2 CreateHashFunction(int hashSize)
        {
            return new SpookyHashV2(hashSize);
        }

        protected override Mock<SpookyHashV2> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<SpookyHashV2>(hashSize);
        }
    }
    

    public class IHashFunctionAsyncTests_SpookyHashV2_DefaultConstructor
        : IHashFunctionAsyncTests<SpookyHashV2>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(128, TestConstants.FooBar, "7040f0ce799f9b1f22020c7b2be86797"),
                    new KnownValue(128, TestConstants.LoremIpsum, "844cd5de8b986bbb1062913785ea1fa2"),
                };
            }
        }

        protected override SpookyHashV2 CreateHashFunction(int hashSize)
        {
            return new SpookyHashV2();
        }

        protected override Mock<SpookyHashV2> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<SpookyHashV2>();
        }
    }
    

    public class IHashFunctionAsyncTests_SpookyHashV2_WithInitVals
        : IHashFunctionAsyncTests<SpookyHashV2>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.FooBar, 0x7acaff2f),
                    new KnownValue(64, TestConstants.FooBar, 0x080cdf197acaff2f),
                    new KnownValue(128, TestConstants.FooBar, "275e9e8cb0cc53c1d604509a253730a9"),
                };
            }
        }

        protected override SpookyHashV2 CreateHashFunction(int hashSize)
        {
            return new SpookyHashV2(hashSize, 0x7da236b987930b75U, 0x2eb994a3851d2f54U);
        }

        protected override Mock<SpookyHashV2> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<SpookyHashV2>(hashSize, 0x7da236b987930b75U, 0x2eb994a3851d2f54U);
        }
    }
    

    public class IHashFunctionAsyncTests_SpookyHashV2_WithInitVals_DefaultHashSize
        : IHashFunctionAsyncTests<SpookyHashV2>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(128, TestConstants.FooBar, "275e9e8cb0cc53c1d604509a253730a9"),
                };
            }
        }

        protected override SpookyHashV2 CreateHashFunction(int hashSize)
        {
            return new SpookyHashV2(0x7da236b987930b75U, 0x2eb994a3851d2f54U);
        }

        protected override Mock<SpookyHashV2> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<SpookyHashV2>(0x7da236b987930b75U, 0x2eb994a3851d2f54U);
        }
    }
    

    #endregion

    #region Data.HashFunction.xxHash

    public class IHashFunctionAsyncTests_xxHash
        : IHashFunctionAsyncTests<xxHash>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0x02cc5d05),
                    new KnownValue(32, TestConstants.FooBar, 0xeda34aaf),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x92ea46ac),
                    new KnownValue(64, TestConstants.Empty, 0xef46db3751d8e999),
                    new KnownValue(64, TestConstants.FooBar, 0xa2aa05ed9085aaf9),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xaf35642971419cbe),
                };
            }
        }

        protected override xxHash CreateHashFunction(int hashSize)
        {
            return new xxHash(hashSize);
        }

        protected override Mock<xxHash> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<xxHash>(hashSize);
        }
    }
    

    public class IHashFunctionAsyncTests_xxHash_DefaultConstructor
        : IHashFunctionAsyncTests<xxHash>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0x02cc5d05),
                    new KnownValue(32, TestConstants.FooBar, 0xeda34aaf),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x92ea46ac),
                };
            }
        }

        protected override xxHash CreateHashFunction(int hashSize)
        {
            return new xxHash();
        }

        protected override Mock<xxHash> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<xxHash>();
        }
    }
    

    public class IHashFunctionAsyncTests_xxHash_WithInitVal
        : IHashFunctionAsyncTests<xxHash>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0xff52b36b),
                    new KnownValue(32, TestConstants.FooBar, 0x294f6b05),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x01f950ab),
                    new KnownValue(64, TestConstants.Empty, 0x985e09f666271418),
                    new KnownValue(64, TestConstants.FooBar, 0x947ebc3ef380d35d),
                    new KnownValue(64, TestConstants.LoremIpsum, 0xf6b6e74f681d3e5b),
                };
            }
        }

        protected override xxHash CreateHashFunction(int hashSize)
        {
            return new xxHash(hashSize, 0x78fef705b7c769faU);
        }

        protected override Mock<xxHash> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<xxHash>(hashSize, 0x78fef705b7c769faU);
        }
    }
    

    public class IHashFunctionAsyncTests_xxHash_WithInitVal_DefaultHashSize
        : IHashFunctionAsyncTests<xxHash>
    {
        protected override IEnumerable<KnownValue> KnownValues
        {
            get
            {
                return new KnownValue[] {
                    new KnownValue(32, TestConstants.Empty, 0xff52b36b),
                    new KnownValue(32, TestConstants.FooBar, 0x294f6b05),
                    new KnownValue(32, TestConstants.LoremIpsum, 0x01f950ab),
                };
            }
        }

        protected override xxHash CreateHashFunction(int hashSize)
        {
            return new xxHash(0x78fef705b7c769faU);
        }

        protected override Mock<xxHash> CreateHashFunctionMock(int hashSize)
        {
            return new Mock<xxHash>(0x78fef705b7c769faU);
        }
    }
    

    #endregion


    #endregion

    #region IHashFunction_SpeedTest

    public class IHashFunction_SpeedTests_HashFunctionBase
        : IHashFunction_SpeedTest
    {
        protected override IReadOnlyDictionary<string, IHashFunction> TestHashFunctions { get { return _TestHashFunctions; } }

        private readonly IReadOnlyDictionary<string, IHashFunction> _TestHashFunctions = new Dictionary<string, IHashFunction>() {
                { 
                    @"BernsteinHash()", 
                    new BernsteinHash() 
                },
                { 
                    @"ModifiedBernsteinHash()", 
                    new ModifiedBernsteinHash() 
                },
                { 
                    @"Blake2B()", 
                    new Blake2B() 
                },
                { 
                    @"Blake2B(8)", 
                    new Blake2B(8) 
                },
                { 
                    @"Blake2B(16)", 
                    new Blake2B(16) 
                },
                { 
                    @"Blake2B(32)", 
                    new Blake2B(32) 
                },
                { 
                    @"Blake2B(64)", 
                    new Blake2B(64) 
                },
                { 
                    @"Blake2B(128)", 
                    new Blake2B(128) 
                },
                { 
                    @"Blake2B(256)", 
                    new Blake2B(256) 
                },
                { 
                    @"Blake2B(512)", 
                    new Blake2B(512) 
                },
                { 
                    @"Blake2B(TestConstants.FooBar, null, null, 8)", 
                    new Blake2B(TestConstants.FooBar, null, null, 8) 
                },
                { 
                    @"Blake2B(TestConstants.FooBar, null, null, 16)", 
                    new Blake2B(TestConstants.FooBar, null, null, 16) 
                },
                { 
                    @"Blake2B(TestConstants.FooBar, null, null, 32)", 
                    new Blake2B(TestConstants.FooBar, null, null, 32) 
                },
                { 
                    @"Blake2B(TestConstants.FooBar, null, null, 64)", 
                    new Blake2B(TestConstants.FooBar, null, null, 64) 
                },
                { 
                    @"Blake2B(TestConstants.FooBar, null, null, 128)", 
                    new Blake2B(TestConstants.FooBar, null, null, 128) 
                },
                { 
                    @"Blake2B(TestConstants.FooBar, null, null, 256)", 
                    new Blake2B(TestConstants.FooBar, null, null, 256) 
                },
                { 
                    @"Blake2B(TestConstants.FooBar, null, null, 512)", 
                    new Blake2B(TestConstants.FooBar, null, null, 512) 
                },
                { 
                    @"DefaultBuzHash(8)", 
                    new DefaultBuzHash(8) 
                },
                { 
                    @"DefaultBuzHash(16)", 
                    new DefaultBuzHash(16) 
                },
                { 
                    @"DefaultBuzHash(32)", 
                    new DefaultBuzHash(32) 
                },
                { 
                    @"DefaultBuzHash(64)", 
                    new DefaultBuzHash(64) 
                },
                { 
                    @"DefaultBuzHash()", 
                    new DefaultBuzHash() 
                },
                { 
                    @"DefaultBuzHash(BuzHashBase.CircularShiftDirection.Right, 8)", 
                    new DefaultBuzHash(BuzHashBase.CircularShiftDirection.Right, 8) 
                },
                { 
                    @"DefaultBuzHash(BuzHashBase.CircularShiftDirection.Right, 16)", 
                    new DefaultBuzHash(BuzHashBase.CircularShiftDirection.Right, 16) 
                },
                { 
                    @"DefaultBuzHash(BuzHashBase.CircularShiftDirection.Right, 32)", 
                    new DefaultBuzHash(BuzHashBase.CircularShiftDirection.Right, 32) 
                },
                { 
                    @"DefaultBuzHash(BuzHashBase.CircularShiftDirection.Right, 64)", 
                    new DefaultBuzHash(BuzHashBase.CircularShiftDirection.Right, 64) 
                },
                { 
                    @"DefaultBuzHash(BuzHashBase.CircularShiftDirection.Right)", 
                    new DefaultBuzHash(BuzHashBase.CircularShiftDirection.Right) 
                },
                { 
                    @"BuzHashBaseImpl(8)", 
                    new BuzHashBaseImpl(8) 
                },
                { 
                    @"BuzHashBaseImpl(16)", 
                    new BuzHashBaseImpl(16) 
                },
                { 
                    @"BuzHashBaseImpl(32)", 
                    new BuzHashBaseImpl(32) 
                },
                { 
                    @"BuzHashBaseImpl(64)", 
                    new BuzHashBaseImpl(64) 
                },
                { 
                    @"BuzHashBaseImpl()", 
                    new BuzHashBaseImpl() 
                },
                { 
                    @"CityHash(32)", 
                    new CityHash(32) 
                },
                { 
                    @"CityHash(64)", 
                    new CityHash(64) 
                },
                { 
                    @"CityHash(128)", 
                    new CityHash(128) 
                },
                { 
                    @"CityHash()", 
                    new CityHash() 
                },
                { 
                    @"HashFunctionImpl()", 
                    new HashFunctionImpl() 
                },
                { 
                    @"HashAlgorithmWrapper(new SHA1Managed())", 
                    new HashAlgorithmWrapper(new SHA1Managed()) 
                },
                { 
                    @"HashAlgorithmWrapper(new SHA256Managed())", 
                    new HashAlgorithmWrapper(new SHA256Managed()) 
                },
                { 
                    @"HashAlgorithmWrapper(new SHA384Managed())", 
                    new HashAlgorithmWrapper(new SHA384Managed()) 
                },
                { 
                    @"HashAlgorithmWrapper(new SHA512Managed())", 
                    new HashAlgorithmWrapper(new SHA512Managed()) 
                },
                { 
                    @"HashAlgorithmWrapper(new MD5CryptoServiceProvider())", 
                    new HashAlgorithmWrapper(new MD5CryptoServiceProvider()) 
                },
                { 
                    @"HashAlgorithmWrapper(new RIPEMD160Managed())", 
                    new HashAlgorithmWrapper(new RIPEMD160Managed()) 
                },
                { 
                    @"HashAlgorithmWrapper<SHA1Managed>()", 
                    new HashAlgorithmWrapper<SHA1Managed>() 
                },
                { 
                    @"HashAlgorithmWrapper<SHA256Managed>()", 
                    new HashAlgorithmWrapper<SHA256Managed>() 
                },
                { 
                    @"HashAlgorithmWrapper<SHA384Managed>()", 
                    new HashAlgorithmWrapper<SHA384Managed>() 
                },
                { 
                    @"HashAlgorithmWrapper<SHA512Managed>()", 
                    new HashAlgorithmWrapper<SHA512Managed>() 
                },
                { 
                    @"HashAlgorithmWrapper<MD5CryptoServiceProvider>()", 
                    new HashAlgorithmWrapper<MD5CryptoServiceProvider>() 
                },
                { 
                    @"HashAlgorithmWrapper<RIPEMD160Managed>()", 
                    new HashAlgorithmWrapper<RIPEMD160Managed>() 
                },
                { 
                    @"ELF64()", 
                    new ELF64() 
                },
                { 
                    @"FNV1(32)", 
                    new FNV1(32) 
                },
                { 
                    @"FNV1(64)", 
                    new FNV1(64) 
                },
                { 
                    @"FNV1(128)", 
                    new FNV1(128) 
                },
                { 
                    @"FNV1()", 
                    new FNV1() 
                },
                { 
                    @"FNV1a(32)", 
                    new FNV1a(32) 
                },
                { 
                    @"FNV1a(64)", 
                    new FNV1a(64) 
                },
                { 
                    @"FNV1a(128)", 
                    new FNV1a(128) 
                },
                { 
                    @"FNV1a()", 
                    new FNV1a() 
                },
                { 
                    @"JenkinsOneAtATime()", 
                    new JenkinsOneAtATime() 
                },
                { 
                    @"JenkinsLookup2()", 
                    new JenkinsLookup2() 
                },
                { 
                    @"JenkinsLookup2(0x7da236b9U)", 
                    new JenkinsLookup2(0x7da236b9U) 
                },
                { 
                    @"JenkinsLookup3(32)", 
                    new JenkinsLookup3(32) 
                },
                { 
                    @"JenkinsLookup3(64)", 
                    new JenkinsLookup3(64) 
                },
                { 
                    @"JenkinsLookup3()", 
                    new JenkinsLookup3() 
                },
                { 
                    @"JenkinsLookup3(32, 0x7da236b9U, 0x87930b75U)", 
                    new JenkinsLookup3(32, 0x7da236b9U, 0x87930b75U) 
                },
                { 
                    @"JenkinsLookup3(64, 0x7da236b9U, 0x87930b75U)", 
                    new JenkinsLookup3(64, 0x7da236b9U, 0x87930b75U) 
                },
                { 
                    @"JenkinsLookup3(0x7da236b9U, 0x87930b75U)", 
                    new JenkinsLookup3(0x7da236b9U, 0x87930b75U) 
                },
                { 
                    @"MurmurHash1()", 
                    new MurmurHash1() 
                },
                { 
                    @"MurmurHash2(32)", 
                    new MurmurHash2(32) 
                },
                { 
                    @"MurmurHash2(64)", 
                    new MurmurHash2(64) 
                },
                { 
                    @"MurmurHash2()", 
                    new MurmurHash2() 
                },
                { 
                    @"MurmurHash3(32)", 
                    new MurmurHash3(32) 
                },
                { 
                    @"MurmurHash3(128)", 
                    new MurmurHash3(128) 
                },
                { 
                    @"MurmurHash3()", 
                    new MurmurHash3() 
                },
                { 
                    @"WikipediaPearson(8)", 
                    new WikipediaPearson(8) 
                },
                { 
                    @"WikipediaPearson(16)", 
                    new WikipediaPearson(16) 
                },
                { 
                    @"WikipediaPearson(24)", 
                    new WikipediaPearson(24) 
                },
                { 
                    @"WikipediaPearson(32)", 
                    new WikipediaPearson(32) 
                },
                { 
                    @"WikipediaPearson(40)", 
                    new WikipediaPearson(40) 
                },
                { 
                    @"WikipediaPearson(48)", 
                    new WikipediaPearson(48) 
                },
                { 
                    @"WikipediaPearson(56)", 
                    new WikipediaPearson(56) 
                },
                { 
                    @"WikipediaPearson(64)", 
                    new WikipediaPearson(64) 
                },
                { 
                    @"WikipediaPearson(128)", 
                    new WikipediaPearson(128) 
                },
                { 
                    @"WikipediaPearson(256)", 
                    new WikipediaPearson(256) 
                },
                { 
                    @"WikipediaPearson(512)", 
                    new WikipediaPearson(512) 
                },
                { 
                    @"WikipediaPearson(1024)", 
                    new WikipediaPearson(1024) 
                },
                { 
                    @"WikipediaPearson(2040)", 
                    new WikipediaPearson(2040) 
                },
                { 
                    @"WikipediaPearson()", 
                    new WikipediaPearson() 
                },
                { 
                    @"SpookyHashV1(32)", 
                    new SpookyHashV1(32) 
                },
                { 
                    @"SpookyHashV1(64)", 
                    new SpookyHashV1(64) 
                },
                { 
                    @"SpookyHashV1(128)", 
                    new SpookyHashV1(128) 
                },
                { 
                    @"SpookyHashV1()", 
                    new SpookyHashV1() 
                },
                { 
                    @"SpookyHashV1(32, 0x7da236b987930b75U, 0x2eb994a3851d2f54U)", 
                    new SpookyHashV1(32, 0x7da236b987930b75U, 0x2eb994a3851d2f54U) 
                },
                { 
                    @"SpookyHashV1(64, 0x7da236b987930b75U, 0x2eb994a3851d2f54U)", 
                    new SpookyHashV1(64, 0x7da236b987930b75U, 0x2eb994a3851d2f54U) 
                },
                { 
                    @"SpookyHashV1(128, 0x7da236b987930b75U, 0x2eb994a3851d2f54U)", 
                    new SpookyHashV1(128, 0x7da236b987930b75U, 0x2eb994a3851d2f54U) 
                },
                { 
                    @"SpookyHashV1(0x7da236b987930b75U, 0x2eb994a3851d2f54U)", 
                    new SpookyHashV1(0x7da236b987930b75U, 0x2eb994a3851d2f54U) 
                },
                { 
                    @"SpookyHashV2(32)", 
                    new SpookyHashV2(32) 
                },
                { 
                    @"SpookyHashV2(64)", 
                    new SpookyHashV2(64) 
                },
                { 
                    @"SpookyHashV2(128)", 
                    new SpookyHashV2(128) 
                },
                { 
                    @"SpookyHashV2()", 
                    new SpookyHashV2() 
                },
                { 
                    @"SpookyHashV2(32, 0x7da236b987930b75U, 0x2eb994a3851d2f54U)", 
                    new SpookyHashV2(32, 0x7da236b987930b75U, 0x2eb994a3851d2f54U) 
                },
                { 
                    @"SpookyHashV2(64, 0x7da236b987930b75U, 0x2eb994a3851d2f54U)", 
                    new SpookyHashV2(64, 0x7da236b987930b75U, 0x2eb994a3851d2f54U) 
                },
                { 
                    @"SpookyHashV2(128, 0x7da236b987930b75U, 0x2eb994a3851d2f54U)", 
                    new SpookyHashV2(128, 0x7da236b987930b75U, 0x2eb994a3851d2f54U) 
                },
                { 
                    @"SpookyHashV2(0x7da236b987930b75U, 0x2eb994a3851d2f54U)", 
                    new SpookyHashV2(0x7da236b987930b75U, 0x2eb994a3851d2f54U) 
                },
                { 
                    @"xxHash(32)", 
                    new xxHash(32) 
                },
                { 
                    @"xxHash(64)", 
                    new xxHash(64) 
                },
                { 
                    @"xxHash()", 
                    new xxHash() 
                },
                { 
                    @"xxHash(32, 0x78fef705b7c769faU)", 
                    new xxHash(32, 0x78fef705b7c769faU) 
                },
                { 
                    @"xxHash(64, 0x78fef705b7c769faU)", 
                    new xxHash(64, 0x78fef705b7c769faU) 
                },
                { 
                    @"xxHash(0x78fef705b7c769faU)", 
                    new xxHash(0x78fef705b7c769faU) 
                },
        };
    }

    #endregion

    #pragma warning restore 0618 // Restore ObsoleteAttribute warnings

}

