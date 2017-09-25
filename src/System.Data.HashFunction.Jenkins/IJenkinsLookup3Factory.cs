using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.HashFunction.Jenkins
{
    public interface IJenkinsLookup3Factory
    {
        IJenkinsLookup3 Create();

        IJenkinsLookup3 Create(IJenkinsLookup3Config config);
    }
}
