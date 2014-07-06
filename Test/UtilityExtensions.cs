using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.HashFunction.Test
{
    public static class UtilityExtensions
    {
        /// <summary>
        /// Converts a hex string to byte array.
        /// </summary>
        /// <param name="hexString">String containing a hexadecimal value, [0-9a-fA-F _-] allowed.</param>
        /// <returns>Byte array of the binary representation of the hexString.</returns>
        public static byte[] HexToBytes(this string hexString)
        {
            var chars = hexString
                .Replace(" ", "")
                .Replace("-", "")
                .Replace("_", "")
                .ToCharArray();

            if (chars.Length % 2 == 1)
                throw new ArgumentException("hexString's length must be divisible by 2 after removing spaces, underscores, and dashes");

            var bytes = new byte[chars.Length / 2];

            for (int x = 0; x < chars.Length; ++x)
            {
                if (x % 2 == 0)
                    bytes[x / 2] = 0;
                else
                    bytes[x / 2] <<= 4;


                if (chars[x] >= '0' && chars[x] <= '9')
                    bytes[x / 2] |= (byte)(chars[x] - '0');
                else if (chars[x] >= 'a' && chars[x] <= 'f')
                    bytes[x / 2] |= (byte)(chars[x] - 'a' + 10);
                else if (chars[x] >= 'A' && chars[x] <= 'F')
                    bytes[x / 2] |= (byte)(chars[x] - 'A' + 10);
                else
                    throw new ArgumentException("hexString contains an invalid character, only [0-9a-fA-F _-] expected");
            }

            return bytes;
        }

        /// <summary>
        /// Converts string to byte array.
        /// </summary>
        /// <param name="value">String to encode into bytes.</param>
        /// <returns>UTF-8 encoding of the string as a byte array.</returns>
        public static byte[] ToBytes(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }


        public static string ToHexString(this byte[] value, bool lowercase = true)
        {
            var sb = new StringBuilder(value.Length * 2);
            foreach (var b in value)
                sb.Append(b.ToString(lowercase ? "x2" : "X2"));

            return sb.ToString();
        }

    }
}
