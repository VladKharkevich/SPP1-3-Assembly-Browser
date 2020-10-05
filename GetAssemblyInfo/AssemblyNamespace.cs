using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyInformation
{
    public class AssemblyNamespace
    {
        public string Namespace { get; set; }
        public List<AssemblyType> AssTypes { get;  set; }

        internal AssemblyNamespace()
        {
            Namespace = "";
            AssTypes = new List<AssemblyType>(); 
        }
    }
}
