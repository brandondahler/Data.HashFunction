/// <summary>
/// BLAKE2
/// 
/// BLAKE2 comes in two flavors:
///		- BLAKE2b (or just BLAKE2) is optimized for 64-bit platforms—including NEON-enabled ARMs—and
///		  produces digests of any size between 1 and 64 bytes
///		- BLAKE2s is optimized for 8- to 32-bit platforms and produces digests of any size between 1
///		  and 32 bytes
///		  
///	(quoted from https://blake2.net/)
///	
/// This is only the BLAKE2b implementation. It is a modified version of the "official" C# port
/// (https://blake2.net/blake2_code_20140114.zip), adapted to the System.Data.HashFunction 
/// interfaces. Licenses for the original implementation below:
/// 
/// BLAKE2 reference source code package - C# implementation
///
/// Written in 2012 by Christian Winnerlein  <codesinchaos@gmail.com>
///
/// To the extent possible under law, the author(s) have dedicated all copyright
/// and related and neighboring rights to this software to the public domain
/// worldwide. This software is distributed without any warranty.
///
/// You should have received a copy of the CC0 Public Domain Dedication along with
/// this software. If not, see <http:///creativecommons.org/publicdomain/zero/1.0/>.
/// </summary>
using System;
using System.Linq;
using Xunit;

namespace System.Data.HashFunction.Test.Blake2BTests
{
	class SequentialTests
	{
		byte[] input = Enumerable.Range(0, 256).Select(i => (byte)i).ToArray();

		[Fact]
		public void CheckTestVectors()
		{
			for (int len = 0; len < TestVectors.UnkeyedBlake2B.Length; len++)
			{
				var input = Enumerable.Range(0, len).Select(i => (byte)i).ToArray();
				var hash = new System.Data.HashFunction.Blake2B().ComputeHash(input);
				string actual = BitConverter.ToString(hash).Replace("-", "");
				string expected = TestVectors.UnkeyedBlake2B[len];
				Assert.Equal(expected, actual);
			}
		}

		[Fact]
		public void CheckKeyedTestVectors()
		{
			var key = Enumerable.Range(0, 64).Select(i => (byte)i).ToArray();
			for (int len = 0; len < TestVectors.KeyedBlake2B.Length; len++)
			{
				var input = Enumerable.Range(0, len).Select(i => (byte)i).ToArray();
				var hash = new System.Data.HashFunction.Blake2B(key).ComputeHash(input);
				string actual = BitConverter.ToString(hash).Replace("-", "");
				string expected = TestVectors.KeyedBlake2B[len];
				Assert.Equal(expected, actual);
			}
		}
	}
}
