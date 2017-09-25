using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Jenkins
{
    public interface IJenkinsLookup2Factory
    {
        IJenkinsLookup2 Create();

        IJenkinsLookup2 Create(IJenkinsLookup2Config config);
    }
}
