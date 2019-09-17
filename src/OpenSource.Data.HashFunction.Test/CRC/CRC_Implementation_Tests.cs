using Moq;
using System;
using System.Collections.Generic;
using OpenSource.Data.HashFunction.CRC;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OpenSource.Data.HashFunction.Test.CRC
{
    public class CRC_Implementation_Tests
    {
        
        #region Constructor
        
        [Fact]
        public void CRC_Implementation_Constructor_ValidInputs_Work()
        {
            var crcConfigMock = new Mock<ICRCConfig>();
            {
                crcConfigMock.SetupGet(cc => cc.HashSizeInBits)
                    .Returns(64);
                
                crcConfigMock.Setup(cc => cc.Clone())
                    .Returns(() => crcConfigMock.Object);
            }


            GC.KeepAlive(
                new CRC_Implementation(crcConfigMock.Object));
        }


        #region Config

        [Fact]
        public void CRC_Implementation_Constructor_Config_IsNull_Throws()
        {
            Assert.Equal(
                "config",
                Assert.Throws<ArgumentNullException>(
                        () => new CRC_Implementation(null))
                    .ParamName);
        }

        [Fact]
        public void CRC_Implementation_Constructor_Config_IsCloned()
        {
            var crcConfigMock = new Mock<ICRCConfig>();
            {
                crcConfigMock.Setup(cc => cc.Clone())
                    .Returns(
                        new CRCConfig() {
                            HashSizeInBits = 64
                        });
            }

            GC.KeepAlive(
                new CRC_Implementation(crcConfigMock.Object));


            crcConfigMock.Verify(cc => cc.Clone(), Times.Once);

            crcConfigMock.VerifyGet(cc => cc.HashSizeInBits, Times.Never);
            crcConfigMock.VerifyGet(cc => cc.InitialValue, Times.Never);
            crcConfigMock.VerifyGet(cc => cc.Polynomial, Times.Never);
            crcConfigMock.VerifyGet(cc => cc.ReflectIn, Times.Never);
            crcConfigMock.VerifyGet(cc => cc.ReflectOut, Times.Never);
            crcConfigMock.VerifyGet(cc => cc.XOrOut, Times.Never);
        }
        
        #region HashSizeInBits

        [Fact]
        public void CRC_Implementation_Constructor_Config_HashSizeInBits_IsInvalid_Throws()
        {
            var invalidLengths = new[] { -1, 0, 65, 128 };

            foreach (var length in invalidLengths)
            {
                var crcConfigMock = new Mock<ICRCConfig>();
                {
                    crcConfigMock.SetupGet(cc => cc.HashSizeInBits)
                        .Returns(length);

                    crcConfigMock.Setup(cc => cc.Clone())
                        .Returns(() => crcConfigMock.Object);
                }


                Assert.Equal(
                    "config.HashSizeInBits",
                    Assert.Throws<ArgumentOutOfRangeException>(
                            () => new CRC_Implementation(crcConfigMock.Object))
                        .ParamName);
            }
        }

        [Fact]
        public void CRC_Implementation_Constructor_Config_HashSizeInBits_IsValid_Works()
        {
            var validLengths = Enumerable.Range(1, 64);

            foreach (var length in validLengths)
            {
                var crcConfigMock = new Mock<ICRCConfig>();
                {
                    crcConfigMock.SetupGet(cc => cc.HashSizeInBits)
                        .Returns(length);
                    
                    crcConfigMock.Setup(cc => cc.Clone())
                        .Returns(() => crcConfigMock.Object);
                }


                GC.KeepAlive(
                    new CRC_Implementation(crcConfigMock.Object));
            }
        }

        #endregion
        
        #endregion

        #endregion

        #region Config

        [Fact]
        public void CRC_Implementation_Config_IsCloneOfClone()
        {
            var crcConfig3 = Mock.Of<ICRCConfig>();
            var crcConfig2 = Mock.Of<ICRCConfig>(cc => cc.HashSizeInBits == 32 && cc.Clone() == crcConfig3);
            var crcConfig = Mock.Of<ICRCConfig>(cc => cc.Clone() == crcConfig2);


            var crcHash = new CRC_Implementation(crcConfig);

            Assert.Equal(crcConfig3, crcHash.Config);
        }

        #endregion

        #region HashSizeInBits

        [Fact]
        public void CRC_Implementation_HashSizeInBits_MatchesConfig()
        {
            var validHashSizes = Enumerable.Range(1, 64);

            foreach (var hashSize in validHashSizes)
            {
                var crcConfigMock = new Mock<ICRCConfig>();
                {
                    crcConfigMock.SetupGet(cc => cc.HashSizeInBits)
                        .Returns(hashSize);

                    crcConfigMock.Setup(cc => cc.Clone())
                        .Returns(() => crcConfigMock.Object);
                }


                var crc = new CRC_Implementation(crcConfigMock.Object);

                Assert.Equal(hashSize, crc.HashSizeInBits);
            }
        }

        #endregion
        

        public class IStreamableHashFunction_Tests_CRC3_ROHC
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(3, "123456789", 0x6),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC3_ROHC);
        }

        public class IStreamableHashFunction_Tests_CRC4_ITU
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(4, "123456789", 0x7),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC4_ITU);
        }

        public class IStreamableHashFunction_Tests_CRC5_EPC
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues =>
                new KnownValue[] {
                    new KnownValue(5, "123456789", 0x00),
                };


            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC5_EPC);
        }

        public class IStreamableHashFunction_Tests_CRC5_ITU
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues =>
                new KnownValue[] {
                    new KnownValue(5, "123456789", 0x07),
                };


            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC5_ITU);
        }

        public class IStreamableHashFunction_Tests_CRC5_USB
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(5, "123456789", 0x19),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC5_USB);
        }

        public class IStreamableHashFunction_Tests_CRC6_CDMA2000A
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(6, "123456789", 0x0d),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC6_CDMA2000A);
        }

        public class IStreamableHashFunction_Tests_CRC6_CDMA2000B
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(6, "123456789", 0x3b),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC6_CDMA2000B);
        }

        public class IStreamableHashFunction_Tests_CRC6_DARC
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(6, "123456789", 0x26),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC6_DARC);
        }

        public class IStreamableHashFunction_Tests_CRC6_ITU
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(6, "123456789", 0x06),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC6_ITU);
        }

        public class IStreamableHashFunction_Tests_CRC7
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(7, "123456789", 0x75),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC7);
        }

        public class IStreamableHashFunction_Tests_CRC7_ROHC
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(7, "123456789", 0x53),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC7_ROHC);
        }

        public class IStreamableHashFunction_Tests_CRC8
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, "123456789", 0xf4),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC8);
        }

        public class IStreamableHashFunction_Tests_CRC8_CDMA2000
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, "123456789", 0xda),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC8_CDMA2000);
        }

        public class IStreamableHashFunction_Tests_CRC8_DARC
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, "123456789", 0x15),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC8_DARC);
        }

        public class IStreamableHashFunction_Tests_CRC8_DVBS2
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, "123456789", 0xbc),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC8_DVBS2);
        }

        public class IStreamableHashFunction_Tests_CRC8_EBU
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, "123456789", 0x97),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC8_EBU);
        }

        public class IStreamableHashFunction_Tests_CRC8_ICODE
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, "123456789", 0x7e),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC8_ICODE);
        }

        public class IStreamableHashFunction_Tests_CRC8_ITU
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, "123456789", 0xa1),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC8_ITU);
        }

        public class IStreamableHashFunction_Tests_CRC8_MAXIM
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, "123456789", 0xa1),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC8_MAXIM);
        }

        public class IStreamableHashFunction_Tests_CRC8_ROHC
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, "123456789", 0xd0),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC8_ROHC);
        }

        public class IStreamableHashFunction_Tests_CRC8_WCDMA
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(8, "123456789", 0x25),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC8_WCDMA);
        }

        public class IStreamableHashFunction_Tests_CRC10
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(10, "123456789", 0x199),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC10);
        }

        public class IStreamableHashFunction_Tests_CRC10_CDMA2000
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(10, "123456789", 0x233),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC10_CDMA2000);
        }

        public class IStreamableHashFunction_Tests_CRC11
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(11, "123456789", 0x5a3),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC11);
        }

        public class IStreamableHashFunction_Tests_CRC12_3GPP
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(12, "123456789", 0xdaf),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC12_3GPP);
        }

        public class IStreamableHashFunction_Tests_CRC12_CDMA2000
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(12, "123456789", 0xd4d),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC12_CDMA2000);
        }

        public class IStreamableHashFunction_Tests_CRC12_DECT
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(12, "123456789", 0xf5b),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC12_DECT);
        }

        public class IStreamableHashFunction_Tests_CRC13_BBC
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(13, "123456789", 0x04fa),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC13_BBC);
        }

        public class IStreamableHashFunction_Tests_CRC14_DARC
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(14, "123456789", 0x082d),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC14_DARC);
        }

        public class IStreamableHashFunction_Tests_CRC15
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(15, "123456789", 0x059e),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC15);
        }

        public class IStreamableHashFunction_Tests_CRC15_MPT1327
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(15, "123456789", 0x2566),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC15_MPT1327);
        }

        public class IStreamableHashFunction_Tests_ARC
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0xbb3d),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.ARC);
        }

        public class IStreamableHashFunction_Tests_CRC16_AUGCCITT
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0xe5cc),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_AUGCCITT);
        }

        public class IStreamableHashFunction_Tests_CRC16_BUYPASS
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0xfee8),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_BUYPASS);
        }

        public class IStreamableHashFunction_Tests_CRC16_CCITTFALSE
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x29b1),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_CCITTFALSE);
        }

        public class IStreamableHashFunction_Tests_CRC16_CDMA2000
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x4c06),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_CDMA2000);
        }

        public class IStreamableHashFunction_Tests_CRC16_DDS110
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x9ecf),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_DDS110);
        }

        public class IStreamableHashFunction_Tests_CRC16_DECTR
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x007e),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_DECTR);
        }

        public class IStreamableHashFunction_Tests_CRC16_DECTX
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x007f),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_DECTX);
        }

        public class IStreamableHashFunction_Tests_CRC16_DNP
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0xea82),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_DNP);
        }

        public class IStreamableHashFunction_Tests_CRC16_EN13757
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0xc2b7),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_EN13757);
        }

        public class IStreamableHashFunction_Tests_CRC16_GENIBUS
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0xd64e),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_GENIBUS);
        }

        public class IStreamableHashFunction_Tests_CRC16_MAXIM
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x44c2),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_MAXIM);
        }

        public class IStreamableHashFunction_Tests_CRC16_MCRF4XX
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x6f91),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_MCRF4XX);
        }

        public class IStreamableHashFunction_Tests_CRC16_RIELLO
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x63d0),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_RIELLO);
        }

        public class IStreamableHashFunction_Tests_CRC16_T10DIF
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0xd0db),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_T10DIF);
        }

        public class IStreamableHashFunction_Tests_CRC16_TELEDISK
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x0fb3),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_TELEDISK);
        }

        public class IStreamableHashFunction_Tests_CRC16_TMS37157
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x26b1),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_TMS37157);
        }

        public class IStreamableHashFunction_Tests_CRC16_USB
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0xb4c8),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC16_USB);
        }

        public class IStreamableHashFunction_Tests_CRCA
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0xbf05),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRCA);
        }

        public class IStreamableHashFunction_Tests_KERMIT
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x2189),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.KERMIT);
        }

        public class IStreamableHashFunction_Tests_MODBUS
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x4b37),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.MODBUS);
        }

        public class IStreamableHashFunction_Tests_X25
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x906e),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.X25);
        }

        public class IStreamableHashFunction_Tests_XMODEM
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(16, "123456789", 0x31c3),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.XMODEM);
        }

        public class IStreamableHashFunction_Tests_CRC24
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(24, "123456789", 0x21cf02),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC24);
        }

        public class IStreamableHashFunction_Tests_CRC24_FLEXRAYA
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(24, "123456789", 0x7979bd),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC24_FLEXRAYA);
        }

        public class IStreamableHashFunction_Tests_CRC24_FLEXRAYB
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(24, "123456789", 0x1f23b8),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC24_FLEXRAYB);
        }

        public class IStreamableHashFunction_Tests_CRC31_PHILIPS
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(31, "123456789", 0x0ce9e46c),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC31_PHILIPS);
        }

        public class IStreamableHashFunction_Tests_CRC32
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, "123456789", 0xcbf43926),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC32);
        }

        public class IStreamableHashFunction_Tests_CRC32_BZIP2
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, "123456789", 0xfc891918),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC32_BZIP2);
        }

        public class IStreamableHashFunction_Tests_CRC32C
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, "123456789", 0xe3069283),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC32C);
        }

        public class IStreamableHashFunction_Tests_CRC32D
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, "123456789", 0x87315576),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC32D);
        }

        public class IStreamableHashFunction_Tests_CRC32_MPEG2
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, "123456789", 0x0376e6e7),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC32_MPEG2);
        }

        public class IStreamableHashFunction_Tests_CRC32_POSIX
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, "123456789", 0x765e7680),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC32_POSIX);
        }

        public class IStreamableHashFunction_Tests_CRC32Q
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, "123456789", 0x3010bf7f),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC32Q);
        }

        public class IStreamableHashFunction_Tests_JAMCRC
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, "123456789", 0x340bc6d9),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.JAMCRC);
        }

        public class IStreamableHashFunction_Tests_XFER
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(32, "123456789", 0xbd0be338),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.XFER);
        }

        public class IStreamableHashFunction_Tests_CRC40_GSM
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(40, "123456789", 0xd4164fc646),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC40_GSM);
        }

        public class IStreamableHashFunction_Tests_CRC64
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(64, "123456789", 0x6c40df5f0b497347),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC64);
        }

        public class IStreamableHashFunction_Tests_CRC64_WE
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(64, "123456789", 0x62ec59e3f1a4f00a),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC64_WE);
        }

        public class IStreamableHashFunction_Tests_CRC64_XZ
            : IStreamableHashFunction_TestBase<ICRC>
        {
            protected override IEnumerable<KnownValue> KnownValues { get; } =
                new KnownValue[] {
                    new KnownValue(64, "123456789", 0x995dc9bbdf1939fa),
                };

            protected override ICRC CreateHashFunction(int hashSize) =>
                new CRC_Implementation(CRCConfig.CRC64_XZ);
        }

    }
}
