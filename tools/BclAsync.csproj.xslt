<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" 
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
  xmlns="http://schemas.microsoft.com/developer/msbuild/2003"
  xmlns:ms="http://schemas.microsoft.com/developer/msbuild/2003"
  exclude-result-prefixes="#default ms"
>
  <xsl:template match="@*|node()">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()" />
    </xsl:copy>
  </xsl:template>

  <xsl:template match="/ms:Project/ms:ItemGroup[1]">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()" />
    </xsl:copy>
      
    <ItemGroup>
      <Reference Include="System.Net" />
      
      <Reference Include="Microsoft.Threading.Tasks, Version=1.0.12.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL">
        <HintPath>..\packages\Microsoft.Bcl.Async.1.0.16\lib\net40\Microsoft.Threading.Tasks.dll</HintPath>
        <Private>True</Private>
      </Reference>
      <Reference Include="Microsoft.Threading.Tasks.Extensions, Version=1.0.12.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL">
        <HintPath>..\packages\Microsoft.Bcl.Async.1.0.16\lib\net40\Microsoft.Threading.Tasks.Extensions.dll</HintPath>
        <Private>True</Private>
      </Reference>
      <Reference Include="Microsoft.Threading.Tasks.Extensions.Desktop, Version=1.0.16.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL">
        <HintPath>..\packages\Microsoft.Bcl.Async.1.0.16\lib\net40\Microsoft.Threading.Tasks.Extensions.Desktop.dll</HintPath>
        <Private>True</Private>
      </Reference>
      <Reference Include="System.IO, Version=2.6.7.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL">
        <HintPath>..\packages\Microsoft.Bcl.1.1.7\lib\net40\System.IO.dll</HintPath>
        <Private>True</Private>
      </Reference>
      <Reference Include="System.Runtime, Version=2.6.7.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL">
        <HintPath>..\packages\Microsoft.Bcl.1.1.7\lib\net40\System.Runtime.dll</HintPath>
        <Private>True</Private>
      </Reference>
      <Reference Include="System.Threading.Tasks, Version=2.6.7.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL">
        <HintPath>..\packages\Microsoft.Bcl.1.1.7\lib\net40\System.Threading.Tasks.dll</HintPath>
        <Private>True</Private>
      </Reference>
    </ItemGroup>
  </xsl:template>

  <xsl:template match="/ms:Project">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()" />
      <Import Project="..\packages\Microsoft.Bcl.Build.1.0.4\tools\Microsoft.Bcl.Build.targets" />
    </xsl:copy>
  </xsl:template>

  <xsl:template match="/ms:Project/ms:ItemGroup/ms:ProjectReference[@Include='..\Core\Data.HashFunction.Core.csproj']">
    <xsl:copy>
      <xsl:attribute name="Include">..\Core\Data.HashFunction.Core.Net40Async.csproj</xsl:attribute>
      <xsl:apply-templates />
    </xsl:copy>
  </xsl:template>
</xsl:stylesheet>