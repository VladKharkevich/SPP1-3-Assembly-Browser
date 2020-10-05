using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyInformation;

namespace Assembly_Browser.Models
{
    public class MainModel
    {
        private AssemblyInformation.GetAssemblyInfo fGetAssemblyInfo;

        public MainModel()
        {
           fGetAssemblyInfo = new AssemblyInformation.GetAssemblyInfo();
        }

        public AssemblyResult GetResult(string filename)
        {
            return fGetAssemblyInfo.Run(filename);
        }
    }
}
