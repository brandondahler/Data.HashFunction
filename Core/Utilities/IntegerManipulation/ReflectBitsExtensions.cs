using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Utilities.IntegerManipulation
{
    internal static class ReflectBitsExtensions
    {
        public static byte ReflectBits(this byte value, int bitLength)
        {
            if (bitLength <= 0 || bitLength > 8)
                throw new ArgumentOutOfRangeException("bitLength", "bitLength must be in the range [1, 8].");


            byte reflectedValue = 0;

            for (int x = 0; x < bitLength; ++x)
            {
                reflectedValue <<= 1;
                
                reflectedValue |= (byte) (value & 1);

                value >>= 1;
            }

            return reflectedValue;
        }

        public static UInt16 ReflectBits(this UInt16 value, int bitLength)
        {
            if (bitLength <= 0 || bitLength > 16)
                throw new ArgumentOutOfRangeException("bitLength", "bitLength must be in the range [1, 16].");


            UInt16 reflectedValue = 0;

            for (int x = 0; x < bitLength; ++x)
            {
                reflectedValue <<= 1;

                reflectedValue |= (UInt16) (value & 1);

                value >>= 1;
            }

            return reflectedValue;
        }

        public static UInt32 ReflectBits(this UInt32 value, int bitLength)
        {
            if (bitLength <= 0 || bitLength > 32)
                throw new ArgumentOutOfRangeException("bitLength", "bitLength must be in the range [1, 32].");


            UInt32 reflectedValue = 0U;

            for (int x = 0; x < bitLength; ++x)
            {
                reflectedValue <<= 1;

                reflectedValue |= (value & 1);

                value >>= 1;
            }

            return reflectedValue;
        }

        public static UInt64 ReflectBits(this UInt64 value, int bitLength)
        {
            if (bitLength <= 0 || bitLength > 64)
                throw new ArgumentOutOfRangeException("bitLength", "bitLength must be in the range [1, 64].");

            UInt64 reflectedValue = 0UL;

            for (int x = 0; x < bitLength; ++x)
            {
                reflectedValue <<= 1;

                reflectedValue |= (value & 1);

                value >>= 1;
            }

            return reflectedValue;
        }
    }
}
