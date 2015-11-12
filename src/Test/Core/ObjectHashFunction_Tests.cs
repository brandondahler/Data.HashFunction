using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace System.Data.HashFunction.Test.Core
{
    public class ObjectHashFunction_Tests
    {

        #region Constructor

        [Fact]
        public void ObjectHashFunction_Constructor_HashFunctionNull_BuiltInSerializer_Throws()
        {
            Assert.Equal(
                "hashFunction",
                Assert.Throws<ArgumentNullException>(() => 
                        new ObjectHashFunction(null, ObjectHashFunction.BuiltInSerializerOptions.BinaryFormatter))
                    .ParamName);
        }

        [Fact]
        public void ObjectHashFunction_Constructor_HashFunction_BuiltInSerializerInvalidEnum_Throws()
        {
            Assert.Equal(
                "builtInSerializerOption",
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                        new ObjectHashFunction(Mock.Of<IHashFunction>(), (ObjectHashFunction.BuiltInSerializerOptions) int.MaxValue))
                    .ParamName);
        }

        [Fact]
        public void ObjectHashFunction_Constructor_HashFunctionNull_CustomSerializer_Throws()
        {
            Assert.Equal(
                "hashFunction",
                Assert.Throws<ArgumentNullException>(() =>
                        new ObjectHashFunction(null, Mock.Of<Func<object, byte[]>>()))
                    .ParamName);
        }

        [Fact]
        public void ObjectHashFunction_Constructor_CustomSerializerNull_Throws()
        {
            Assert.Equal(
                "customSerializer",
                Assert.Throws<ArgumentNullException>(() =>
                        new ObjectHashFunction(Mock.Of<IHashFunction>(), null))
                    .ParamName);
        }

        #endregion

        #region HashSize

        [Fact]
        public void ObjectHashFunction_HashSize_ReturnsUnderlyingHashSize()
        {
            var objectHashFunction = new ObjectHashFunction(Mock.Of<IHashFunction>(hf => hf.HashSize == 1337), ObjectHashFunction.BuiltInSerializerOptions.BinaryFormatter);

            Assert.Equal(1337, objectHashFunction.HashSize);
        } 

        #endregion

        #region CalculateHash

        [Fact]
        public void ObjectHashFunction_CalculateHash_ObjectNull_Throws()
        {
            var objectHashFunction = new ObjectHashFunction(Mock.Of<IHashFunction>(), ObjectHashFunction.BuiltInSerializerOptions.BinaryFormatter);

            Assert.Equal(
                "object",
                Assert.Throws<ArgumentNullException>(() =>
                        objectHashFunction.CalculateHash(null))
                    .ParamName);
        }

        #region BuiltIn

        #region BinaryFormatter

        [Fact]
        public void ObjectHashFunction_CalculateHash_BuiltIn_BinaryFormatter_CallsHashFunction()
        {
            var hashFunction = Mock.Of<IHashFunction>();
            var objectHashFunction = new ObjectHashFunction(hashFunction, ObjectHashFunction.BuiltInSerializerOptions.BinaryFormatter);

            objectHashFunction.CalculateHash(5);

            Mock.Get(hashFunction)
                .Verify(hf => hf.ComputeHash(It.IsAny<byte[]>()), Times.Once());
        }

        #endregion

        #region BitConverter

        [Fact]
        public void ObjectHashFunction_CalculateHash_BuiltIn_BitConverter_CallsHashFunction()
        {
            var hashFunction = Mock.Of<IHashFunction>();
            var objectHashFunction = new ObjectHashFunction(hashFunction, ObjectHashFunction.BuiltInSerializerOptions.BitConverter);

            objectHashFunction.CalculateHash(5);

            Mock.Get(hashFunction)
                .Verify(hf => hf.ComputeHash(It.IsAny<byte[]>()), Times.Once());
        }

        [Fact]
        public void ObjectHashFunction_CalculateHash_BuiltIn_BitConverter_InvalidType_Throws()
        {
            var hashFunction = Mock.Of<IHashFunction>();
            var objectHashFunction = new ObjectHashFunction(hashFunction, ObjectHashFunction.BuiltInSerializerOptions.BitConverter);

            Assert.Throws<InvalidOperationException>(() => 
                objectHashFunction.CalculateHash(DateTime.Now));
        }

        [Fact]
        public void ObjectHashFunction_CalculateHash_BuiltIn_BitConverter_ValidTypes_Work()
        {
            var validTypes = new[] {
                typeof(bool),
                typeof(float), typeof(double),
                typeof(char), typeof(byte),
                typeof(short), typeof(ushort),
                typeof(int), typeof(uint),
                typeof(long), typeof(long),
            };


            var objectHashFunction = new ObjectHashFunction(Mock.Of<IHashFunction>(), ObjectHashFunction.BuiltInSerializerOptions.BitConverter);

            foreach (var validType in validTypes)
                objectHashFunction.CalculateHash(Activator.CreateInstance(validType));
        }

        #endregion

        #endregion

        #region CustomSerializer

        [Fact]
        public void ObjectHashFunction_CalculateHash_BuiltIn_CustomSerializer_CallsHashFunction()
        {
            var hashFunction = Mock.Of<IHashFunction>();
            var objectHashFunction = new ObjectHashFunction(hashFunction, (o) => new byte[1]);

            objectHashFunction.CalculateHash(5);

            Mock.Get(hashFunction)
                .Verify(hf => hf.ComputeHash(It.IsAny<byte[]>()), Times.Once());
        }

        #endregion

        #endregion


    }
}
