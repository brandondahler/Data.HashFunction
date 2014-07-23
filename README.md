Data.HashFunction 
=================

Data.HashFunction is a C# library to create a common interface to [non-cryptographic hash functions](http://en.wikipedia.org/wiki/List_of_hash_functions#Non-cryptographic_hash_functions) and provide implementations of public hash functions.  It is licensed under the permissive and OSI approved [MIT](http://opensource.org/licenses/MIT) license.


All functionality of the library is tested using [xUnit](https://github.com/xunit/xunit).  A primary requirement for each release is 100% code coverage by these tests.

All code within the libarary is commented using Sandcastle-compatible XML comments.  Sandcastle documentation will be avaliable in the near future.


Implementations
---------------

All implementation packages depend on the Data.HashFunctionBase NuGet package.

The following hash functions have been implemented from the most reliable reference that could be found.

* [Bernstein Hash](http://www.eternallyconfuzzled.com/tuts/algorithms/jsw_tut_hashing.aspx#djb)
  * BernsteinHash - Original
  * ModifiedBernsteinHash - Minor update that is said to result in better distribution
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


Each family of hash functions is contained within its own project and NuGet package.


Usage
-----

The usage for all hash functions has been standardized and is accessible via the System.Data.HashFunction.IHashFunction interface.  The core package, Data.HashFunction, only contains wrappers for the .Net BCL's Cryptographic HashAlgorithm functions.  In order to use a different function, you will need to reference one of the implementation packages.


The System.Data.HashFunction namespace?
-------------------------------------------

The hope is that eventually this library will be integrated into the .Net BCL.  With that in mind, the choice was made so that if/when it gets merged, assuming the API remains compatible, the migration to the BCL version would require no code changes.


Contributing
------------

Feel free to propose changes, notify of issues, or contribute code using GitHub!  Submit issues and/or pull requests as necessary. 

There are no special requirements for change proposal or issue notifications.  


Code contributions should follow existing code's methodologies and style, along with XML comments for all public and protected namespaces, classes, and functions added.

See [Readme_PFX.txt](https://github.com/brandondahler/Data.HashFunction/blob/master/Readme_PFX.txt) for information about building the locally signed code.


License
-------

Data.HashFunction is released under the terms of the MIT license. See [LICENSE](https://github.com/brandondahler/Data.HashFunction/blob/master/LICENSE) for more information or see http://opensource.org/licenses/MIT.
