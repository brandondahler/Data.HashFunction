Data.HashFunction ![License](https://img.shields.io/github/license/brandondahler/Data.HashFunction.svg)
=================

Data.HashFunction is a C# library to create a common interface to [non-cryptographic hash functions](http://en.wikipedia.org/wiki/List_of_hash_functions#Non-cryptographic_hash_functions) and provide implementations of public hash functions.  It is licensed under the permissive and OSI approved [MIT](http://opensource.org/licenses/MIT) license.


All functionality of the library is tested using [xUnit](https://github.com/xunit/xunit).  A primary requirement for each release is 100% code coverage by these tests.

All code within the libarary is commented using Visual Studio-compatible XML comments.

Status
------


### Master

[![Build Status](https://img.shields.io/appveyor/ci/brandondahler/data-hashfunction/master.svg)](https://ci.appveyor.com/project/brandondahler/data-hashfunction)
[![Test Status](https://img.shields.io/appveyor/tests/brandondahler/data-hashfunction/master.svg)](https://ci.appveyor.com/project/brandondahler/data-hashfunction/build/tests)


### NuGet

| Name                            | Normal                                                                                                                                                                 |
|---------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Data.HashFunction.Interfaces    | [![Version Status](https://img.shields.io/nuget/v/System.Data.HashFunction.Interfaces.svg)](https://www.nuget.org/packages/System.Data.HashFunction.Interfaces/)       | 
| Data.HashFunction.Core          | [![Version Status](https://img.shields.io/nuget/v/System.Data.HashFunction.Core.svg)](https://www.nuget.org/packages/System.Data.HashFunction.Core/)                   |
| Data.HashFunction.BernsteinHash | [![Version Status](https://img.shields.io/nuget/v/System.Data.HashFunction.BernsteinHash.svg)](https://www.nuget.org/packages/System.Data.HashFunction.BernsteinHash/) |
| Data.HashFunction.Blake2        | [![Version Status](https://img.shields.io/nuget/v/System.Data.HashFunction.Blake2.svg)](https://www.nuget.org/packages/System.Data.HashFunction.Blake2/)               |
| Data.HashFunction.Buzhash       | [![Version Status](https://img.shields.io/nuget/v/System.Data.HashFunction.Buzhash.svg)](https://www.nuget.org/packages/System.Data.HashFunction.Buzhash/)             |
| Data.HashFunction.CityHash      | [![Version Status](https://img.shields.io/nuget/v/System.Data.HashFunction.CityHash.svg)](https://www.nuget.org/packages/System.Data.HashFunction.CityHash/)           |
| Data.HashFunction.CRC           | [![Version Status](https://img.shields.io/nuget/v/System.Data.HashFunction.CRC.svg)](https://www.nuget.org/packages/System.Data.HashFunction.CRC/)                     |
| Data.HashFunction.ELF64         | [![Version Status](https://img.shields.io/nuget/v/System.Data.HashFunction.ELF64.svg)](https://www.nuget.org/packages/System.Data.HashFunction.ELF64/)                 |
| Data.HashFunction.FNV           | [![Version Status](https://img.shields.io/nuget/v/System.Data.HashFunction.FNV.svg)](https://www.nuget.org/packages/System.Data.HashFunction.FNV/)                     |
| Data.HashFunction.HashAlgorithm | [![Version Status](https://img.shields.io/nuget/v/System.Data.HashFunction.HashAlgorithm.svg)](https://www.nuget.org/packages/System.Data.HashFunction.HashAlgorithm/) |
| Data.HashFunction.Jenkins       | [![Version Status](https://img.shields.io/nuget/v/System.Data.HashFunction.Jenkins.svg)](https://www.nuget.org/packages/System.Data.HashFunction.Jenkins/)             |
| Data.HashFunction.MurmurHash    | [![Version Status](https://img.shields.io/nuget/v/System.Data.HashFunction.MurmurHash.svg)](https://www.nuget.org/packages/System.Data.HashFunction.MurmurHash/)       |
| Data.HashFunction.Pearson       | [![Version Status](https://img.shields.io/nuget/v/System.Data.HashFunction.Pearson.svg)](https://www.nuget.org/packages/System.Data.HashFunction.Pearson/)             |
| Data.HashFunction.SpookyHash    | [![Version Status](https://img.shields.io/nuget/v/System.Data.HashFunction.SpookyHash.svg)](https://www.nuget.org/packages/System.Data.HashFunction.SpookyHash/)       |
| Data.HashFunction.xxHash        | [![Version Status](https://img.shields.io/nuget/v/System.Data.HashFunction.xxHash.svg)](https://www.nuget.org/packages/System.Data.HashFunction.xxHash/)               |

Implementations
---------------

All implementation packages depend on the Data.HashFunction.Interfaces and Data.HashFunction.Core NuGet packages.

The following hash functions have been implemented from the most reliable reference that could be found.

* [Bernstein Hash](http://www.eternallyconfuzzled.com/tuts/algorithms/jsw_tut_hashing.aspx#djb)
  * BernsteinHash - Original
  * ModifiedBernsteinHash - Minor update that is said to result in better distribution
* [Blake2](https://blake2.net/)
  * Blake2b 
* [BuzHash](http://www.serve.net/buz/hash.adt/java.002.html)
  * BuzHashBase - Abstract implementation, there is no authoritative implementation
  * DefaultBuzHash - Concrete implementation, uses 256 random 64-bit integers
* [CityHash](https://code.google.com/p/cityhash/)
* [CRC](http://en.wikipedia.org/wiki/Cyclic_redundancy_check)
  * CRC - Generalized implementation to allow any CRC parameters between 1 and 64 bits.
  * CRCStandards - 71 implementations on top of CRC that use the parameters defined by their respective standard.  Standards and their parameters provided by [CRC RevEng's catalogue](http://reveng.sourceforge.net/crc-catalogue/).
* [ELF64](http://downloads.openwatcom.org/ftp/devel/docs/elf-64-gen.pdf)
* [FNV](http://www.isthe.com/chongo/tech/comp/fnv/index.html)
  * FNV1Base - Abstract base of the FNV-1 algorithms
  * FNV1 - Original
  * FNV1a - Minor variation of FNV-1
* [Hash Algorithm Wrapper](http://msdn.microsoft.com/en-us/library/system.security.cryptography.hashalgorithm%28v=vs.110%29.aspx)
  * HashAlgorithmWrapper - Wraps existing instance of a .Net HashAlgorithm
  * HashAlgorithmWrapper<HashAlgorithmT> - Wraps a managed instance of a .Net HashAlgorithm
* [Jenkins](http://en.wikipedia.org/wiki/Jenkins_hash_function)
  * JenkinsOneAtATime - Original
  * JenkinsLookup2 - Improvement upon One-at-a-Time hash function
  * JenkinsLookup3 - Further improvement upon Jenkins' Lookup2 hash function
* [Murmur Hash](https://code.google.com/p/smhasher/wiki/MurmurHash)
  * MurmurHash1 - Original
  * MurmurHash2 - Improvement upon MurmurHash1
  * MurmurHash3 - Further improvement upon MurmurHash2, addresses minor flaws
* [Pearson hashing](http://en.wikipedia.org/wiki/Pearson_hashing)
  * PearsonBase - Abstract implementation, there is no authoritative implementation
  * WikipediaPearson - Concrete implementation, uses values from Wikipedia article
* [SpookyHash](http://burtleburtle.net/bob/hash/spooky.html)
  * SpookyHashV1 - Original
  * SpookyHashV2 - Improvement upon SpookyHashV1, fixes bug in original specification
* [xxHash](https://code.google.com/p/xxhash/)
  * xxHash - Original and 64-bit version.


Each family of hash functions is contained within its own project and NuGet package.


Usage
-----

The usage for all hash functions has been standardized and is accessible via the System.Data.HashFunction.IHashFunction and System.Data.HashFunction.IHashFunctionAsync interfaces.  The core package, Data.HashFunction.Core, only contains abstract hash function implementations and base functionality for the library.  In order to use a specific hashing algorithms, you will need to reference its implementation packages.

IHashFunction implementations should be immutable and stateles.  All IHashFunction methods and members should be thread safe.

``` C#
using System;
using System.Data.HashFunction;
using System.Data.HashFunction.Jenkins;

public class Program
{
    public static readonly IJenkinsOneAtATime _jenkinsOneAtATime = JenkinsOneAtATimeFactory.Instance.Create();
    public static void Main()
    {
        var hashValue = _jenkinsOneAtATime.ComputeHash("foobar");

        Console.WriteLine(hashValue.AsHexString());
    }
}
```



Release Notes
-------------
See [Release Notes](https://github.com/brandondahler/Data.HashFunction/wiki/Release-Notes) wiki page.


Contributing
------------

Feel free to propose changes, notify of issues, or contribute code using GitHub!  Submit issues and/or pull requests as necessary. 

There are no special requirements for change proposal or issue notifications.  


Code contributions should follow existing code's methodologies and style, along with XML comments for all public and protected namespaces, classes, and functions added.


License
-------

Data.HashFunction is released under the terms of the MIT license. See [LICENSE](https://github.com/brandondahler/Data.HashFunction/blob/master/LICENSE) for more information or see http://opensource.org/licenses/MIT.
