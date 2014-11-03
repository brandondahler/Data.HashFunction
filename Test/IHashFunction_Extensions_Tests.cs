using Moq;
using System;
using System.Collections.Generic;
using System.Data.HashFunction.Test.Mocks;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test
{
    public class IHashFunction_Extensions_Tests
    {
        public class Sugar
        {
            [Fact]
            public void IHashFunction_Extensions_ComputeHash_bool()
            {
                var value = true;

                AssertSugar(
                    hf => hf.ComputeHash(value),
                    BitConverter.GetBytes(value));
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_byte()
            {
                var value = (byte) 0x39;

                AssertSugar(
                    hf => hf.ComputeHash(value),
                    new[] { value });
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_char()
            {
                var value = (char) 0x39;

                AssertSugar(
                    hf => hf.ComputeHash(value),
                    BitConverter.GetBytes(value));
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_double()
            {
                var value = 39.93820d;

                AssertSugar(
                    hf => hf.ComputeHash(value),
                    BitConverter.GetBytes(value));
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_float()
            {
                var value = 39.93820f;

                AssertSugar(
                    hf => hf.ComputeHash(value),
                    BitConverter.GetBytes(value));
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_int()
            {
                var value = 2830928;

                AssertSugar(
                    hf => hf.ComputeHash(value),
                    BitConverter.GetBytes(value));
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_long()
            {
                var value = 138812075890L;

                AssertSugar(
                    hf => hf.ComputeHash(value),
                    BitConverter.GetBytes(value));
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_sbyte()
            {
                var value = (sbyte) 0x39;

                AssertSugar(
                    hf => hf.ComputeHash(value),
                    new[] { (byte) value });
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_short()
            {
                var value = (short) 3209;

                AssertSugar(
                    hf => hf.ComputeHash(value),
                    BitConverter.GetBytes(value));
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_string()
            {
                var value = "foobar";

                AssertSugar(
                    hf => hf.ComputeHash(value),
                    Encoding.UTF8.GetBytes(value));
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_uint()
            {
                var value = 2830928U;

                AssertSugar(
                    hf => hf.ComputeHash(value),
                    BitConverter.GetBytes(value));
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_ulong()
            {
                var value = 138812075890UL;

                AssertSugar(
                    hf => hf.ComputeHash(value),
                    BitConverter.GetBytes(value));
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_ushort()
            {
                var value = (ushort) 36090;

                AssertSugar(
                    hf => hf.ComputeHash(value),
                    BitConverter.GetBytes(value));
            }


            [Fact]
            public void IHashFunction_Extensions_ComputeHash_TModel()
            {
                var value = new Dictionary<string, int>() { 
                    {"Test", 5 },
                    {"Foo", 20 },
                    {"Bar", 40 }
                };
            
                using (var memoryStream = new MemoryStream())
                {
                    var binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(memoryStream, value);

                    AssertSugar(
                        hf => hf.ComputeHash(value),
                        memoryStream.ToArray());
                }
            }


            private void AssertSugar(Action<IHashFunction> action, byte[] data)
            {
                var hashFunctionMock = new Mock<HashFunctionImpl>();

                hashFunctionMock.Setup(hf => hf.ComputeHash(It.Is<byte[]>(d => data.SequenceEqual(d))))
                    .Verifiable();

                action(hashFunctionMock.Object);

                Assert.DoesNotThrow(() => hashFunctionMock.Verify());
            }
        }

        public class SugarWithDesiredSize
        {
            [Fact]
            public void IHashFunction_Extensions_ComputeHash_WithDesiredBits_bool()
            {
                var value = true;

                AssertSugar(
                    (hf, desiredSize) => hf.ComputeHash(value, desiredSize),
                    BitConverter.GetBytes(value));
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_WithDesiredBits_byte()
            {
                var value = (byte)0x39;

                AssertSugar(
                    (hf, desiredSize) => hf.ComputeHash(value, desiredSize),
                    new[] { value });
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_WithDesiredBits_char()
            {
                var value = (char)0x39;

                AssertSugar(
                    (hf, desiredSize) => hf.ComputeHash(value, desiredSize),
                    BitConverter.GetBytes(value));
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_WithDesiredBits_double()
            {
                var value = 39.93820d;

                AssertSugar(
                    (hf, desiredSize) => hf.ComputeHash(value, desiredSize),
                    BitConverter.GetBytes(value));
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_WithDesiredBits_float()
            {
                var value = 39.93820f;

                AssertSugar(
                    (hf, desiredSize) => hf.ComputeHash(value, desiredSize),
                    BitConverter.GetBytes(value));
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_WithDesiredBits_int()
            {
                var value = 2830928;

                AssertSugar(
                    (hf, desiredSize) => hf.ComputeHash(value, desiredSize),
                    BitConverter.GetBytes(value));
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_WithDesiredBits_long()
            {
                var value = 138812075890L;

                AssertSugar(
                    (hf, desiredSize) => hf.ComputeHash(value, desiredSize),
                    BitConverter.GetBytes(value));
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_WithDesiredBits_sbyte()
            {
                var value = (sbyte) 0x39;

                AssertSugar(
                    (hf, desiredSize) => hf.ComputeHash(value, desiredSize),
                    new[] { (byte) value });
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_WithDesiredBits_short()
            {
                var value = (short)3209;

                AssertSugar(
                    (hf, desiredSize) => hf.ComputeHash(value, desiredSize),
                    BitConverter.GetBytes(value));
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_WithDesiredBits_string()
            {
                var value = "foobar";

                AssertSugar(
                    (hf, desiredSize) => hf.ComputeHash(value, desiredSize),
                    Encoding.UTF8.GetBytes(value));
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_WithDesiredBits_uint()
            {
                var value = 2830928U;

                AssertSugar(
                    (hf, desiredSize) => hf.ComputeHash(value, desiredSize),
                    BitConverter.GetBytes(value));
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_WithDesiredBits_ulong()
            {
                var value = 138812075890UL;

                AssertSugar(
                    (hf, desiredSize) => hf.ComputeHash(value, desiredSize),
                    BitConverter.GetBytes(value));
            }

            [Fact]
            public void IHashFunction_Extensions_ComputeHash_WithDesiredBits_ushort()
            {
                var value = (ushort)36090;

                AssertSugar(
                    (hf, desiredSize) => hf.ComputeHash(value, desiredSize),
                    BitConverter.GetBytes(value));
            }


            [Fact]
            public void IHashFunction_Extensions_ComputeHash_WithDesiredBits_TModel()
            {
                var value = new Dictionary<string, int>() { 
                {"Test", 5 },
                {"Foo", 20 },
                {"Bar", 40 }
            };

                using (var memoryStream = new MemoryStream())
                {
                    var binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(memoryStream, value);

                    AssertSugar(
                        (hf, desiredSize) => hf.ComputeHash(value, desiredSize),
                        memoryStream.ToArray());
                }
            }


            private void AssertSugar(Action<IHashFunction, int> action, byte[] data)
            {
                var hashFunctionMock = new Mock<HashFunctionImpl>();

                hashFunctionMock.SetupGet(hf => hf.HashSize)
                    .Returns(32);

                hashFunctionMock.Setup(hf => hf.ComputeHash(It.Is<byte[]>(d => data.SequenceEqual(d))))
                    .Returns(new byte[4])
                    .Verifiable();


                var hashFunction = hashFunctionMock.Object;

                action(hashFunction, hashFunction.HashSize);


                Assert.DoesNotThrow(() => hashFunctionMock.Verify());
            }

        }


        [Fact]
        public void IHashFunction_Extensions_ComputeHash_WithDesiredBits_byteArray()
        {
            var hashFunction = new JenkinsOneAtATime();

            var knownValues = new Dictionary<int, byte[]>() {
                {  1, new byte[] { 0x00 } },
                {  2, new byte[] { 0x02 } },
                {  4, new byte[] { 0x09 } },
                {  8, new byte[] { 0x1a } },
                { 16, new byte[] { 0xb5, 0x04 } },
                { 32, new byte[] { 0xe7, 0xfd, 0x52, 0xf9 } },
                { 48, new byte[] { 0xcb, 0x66, 0x52, 0xf9, 0x70, 0xac } },
                { 53, new byte[] { 0x3e, 0xf9, 0x52, 0xf9, 0x70, 0xac, 0x0c } },
                { 55, new byte[] { 0xd1, 0xfc, 0x52, 0xf9, 0x70, 0xac, 0x2c } },
                { 64, new byte[] { 0xe7, 0xfd, 0x52, 0xf9, 0x70, 0xac, 0x2c, 0x9b } },
            };


            foreach (var knownValue in knownValues)
            {
                Assert.Equal(
                    knownValue.Value, 
                    hashFunction.ComputeHash(
                        TestConstants.FooBar, 
                        knownValue.Key));
            }
        }

    }
}
