using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssemblyInformation;
using ExtensionClasses;
using FluentAssertions;

namespace AssemblyBrowser.Tests
{
    [TestClass]
    public class AssemblyBrowserTest
    {
        AssemblyResult assemblyResult;
        string filename = "ExtensionClasses.dll";

        [TestInitialize]
        public void TestInitialize()
        {
            GetAssemblyInfo getAssemblyInfo = new GetAssemblyInfo();
            assemblyResult = getAssemblyInfo.Run(filename);
        }

        [TestMethod]
        public void TestCountOfExtensionMethodInExtensionClasses()
        {
            int count = 0;
            foreach (AssemblyMethod assemblyMethod in assemblyResult.AssemblyNamespaces[0].AssTypes[0].AssMethods)
            {
                if (assemblyMethod.Name.StartsWith("$Extended$"))
                    count++;
            }
            count.Should().Be(1);
        }

        [TestMethod]
        public void TestCountOfNamespaces()
        {
            assemblyResult.AssemblyNamespaces.Count.Should().Be(1);
        }

        [TestMethod]
        public void TestCountOfTypes()
        {
            assemblyResult.AssemblyNamespaces[0].AssTypes.Count.Should().Be(2);
        }

        [TestMethod] 
        public void TestSignatureOfMethod_AddChar()
        {
            AssemblyMethod assemblyMethod;
            foreach (AssemblyType tempAssemblyType in assemblyResult.AssemblyNamespaces[0].AssTypes)
                if (tempAssemblyType.AssType == "TempExtension")
                {
                    foreach (AssemblyMethod tempAssemblyMethod in tempAssemblyType.AssMethods)
                    {
                        if (tempAssemblyMethod.Name.Contains("AddChar"))
                        {
                            assemblyMethod = tempAssemblyMethod;
                            assemblyMethod.Name.Should().Be("AddChar(Temp temp, Char c)");
                            break;
                        }
                    }
                    break;
                }
        }

        [TestMethod]
        public void TestCountOfProperties()
        {
            AssemblyType assemblytype;
            foreach (AssemblyType tempAssemblyType in assemblyResult.AssemblyNamespaces[0].AssTypes)
                if (tempAssemblyType.AssType == "Temp")
                {
                    assemblytype = tempAssemblyType;
                    assemblytype.AssProperties.Count.Should().Be(1);
                    break;
                }
        }

        [TestMethod]
        public void TestTypeOfProperty()
        {
            AssemblyType assemblytype;
            foreach (AssemblyType tempAssemblyType in assemblyResult.AssemblyNamespaces[0].AssTypes)
                if (tempAssemblyType.AssType == "Temp")
                {
                    assemblytype = tempAssemblyType;
                    assemblytype.AssProperties[0].Name.Split(' ')[0].ToLower().Should().Be("string");
                    break;
                }
        }

        [TestMethod]
        public void TestCountOfFields()
        {
            AssemblyType assemblytype;
            foreach (AssemblyType tempAssemblyType in assemblyResult.AssemblyNamespaces[0].AssTypes)
                if (tempAssemblyType.AssType == "Temp")
                {
                    assemblytype = tempAssemblyType;
                    assemblytype.AssFields.Count.Should().Be(1);
                    break;
                }
        }

        [TestMethod]
        public void TestTypeOfField()
        {
            AssemblyType assemblytype;
            foreach (AssemblyType tempAssemblyType in assemblyResult.AssemblyNamespaces[0].AssTypes)
                if (tempAssemblyType.AssType == "Temp")
                {
                    assemblytype = tempAssemblyType;
                    assemblytype.AssFields[0].Name.Split(' ')[0].ToLower().Should().Be("string");
                    break;
                }
        }
    }
}
