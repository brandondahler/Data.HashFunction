using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Utilities.IntegerManipulation
{
    internal static class ToBytesExtensions
    {
        public static byte[] ToBytes(this byte value, int bitLength)
        {
            if (bitLength <= 0 || bitLength > 8)
                throw new ArgumentOutOfRangeException("bitLength", "bitLength but be in the range [1, 8].");


            value &= (byte) (byte.MaxValue >> (8 - bitLength));


            return new byte[] { 
                value 
            };
        }

        public static byte[] ToBytes(this UInt16 value, int bitLength)
        {
            if (bitLength <= 0 || bitLength > 16)
                throw new ArgumentOutOfRangeException("bitLength", "bitLength but be in the range [1, 16].");


            value &= (UInt16) (UInt16.MaxValue >> (16 - bitLength));


            var valueBytes = new byte[(bitLength + 7) / 8];

            for (int x = 0; x < valueBytes.Length; ++x)
            {
                valueBytes[x] = (byte)value;
                value >>= 8;
            }

            return valueBytes;
        }

        public static byte[] ToBytes(this UInt32 value, int bitLength)
        {
            if (bitLength <= 0 || bitLength > 32)
                throw new ArgumentOutOfRangeException("bitLength", "bitLength but be in the range [1, 32].");


            value &= (UInt32.MaxValue >> (32 - bitLength));


            var valueBytes = new byte[(bitLength + 7) / 8];

            for (int x = 0; x < valueBytes.Length; ++x)
            {
                valueBytes[x] = (byte) value;
                value >>= 8;
            }

            return valueBytes;
        }

        public static byte[] ToBytes(this UInt64 value, int bitLength)
        {
            if (bitLength <= 0 || bitLength > 64)
                throw new ArgumentOutOfRangeException("bitLength", "bitLength but be in the range [1, 64].");


            value &= (UInt64.MaxValue >> (64 - bitLength));


            var valueBytes = new byte[(bitLength + 7) / 8];

            for (int x = 0; x < valueBytes.Length; ++x)
            {
                valueBytes[x] = (byte)value;
                value >>= 8;
            }

            return valueBytes;
        }
    }
}
