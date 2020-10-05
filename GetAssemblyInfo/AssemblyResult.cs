using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyInformation
{
    public class AssemblyResult
    {
        public AssemblyResult()
        {
            AssemblyName = "";
            AssemblyNamespaces = new List<AssemblyNamespace>();
        }
        public string AssemblyName { get; set; }

        public List<AssemblyNamespace> AssemblyNamespaces { get;  set; }
    }
}
