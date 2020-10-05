using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyInformation
{
    public class AssemblyType
    {
        public string AssType { get; internal set; }
        public List<AssemblyMethod> AssMethods { get; set; }
        public List<AssemblyField> AssFields { get; set; }

        public List<AssemblyDataFromClass> AssData { get;  }

        public List<AssemblyProperty> AssProperties { get; set; }

        internal AssemblyType()
        {
            AssType = "";
            AssMethods = new List<AssemblyMethod>();
            AssFields = new List<AssemblyField>();
            AssProperties = new List<AssemblyProperty>();
            AssData = new List<AssemblyDataFromClass>()
            {
                new AssemblyDataFromClass(){ Data = "Methods", Items = AssMethods},
                new AssemblyDataFromClass(){ Data = "Fields", Items = AssFields},
                new AssemblyDataFromClass(){ Data = "Properties", Items = AssProperties}
            };
        }
    }
}
