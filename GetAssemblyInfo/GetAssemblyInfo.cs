using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace AssemblyInformation
{
    public class GetAssemblyInfo
    {
        private Assembly assembly;
        public GetAssemblyInfo()
        {

        }

        private AssemblyNamespace GetAssemblyNamespace(AssemblyResult assemblyResult, Type type)
        {
            foreach (AssemblyNamespace assemblyNamespace in assemblyResult.AssemblyNamespaces)
            {
                if (type.Namespace == null)
                {
                    return null;
                }
                else if (assemblyNamespace.Namespace.Equals(type.Namespace))
                    {
                         return assemblyNamespace;
                    }
            }
            AssemblyNamespace tempNamespace = new AssemblyNamespace();
            assemblyResult.AssemblyNamespaces.Add(tempNamespace);
            tempNamespace.Namespace = type.Namespace;
            return tempNamespace;
        }

        private AssemblyType GetAssemblyType(AssemblyNamespace assemblyNamespace, Type type)
        {
            AssemblyType assemblyType = new AssemblyType();
            assemblyNamespace.AssTypes.Add(assemblyType);
            assemblyType.AssType = type.Name;
            return assemblyType;
        }

        private void FillInformationInAssemblyType(AssemblyType assemblyType, Type type)
        {
            foreach (MethodInfo methodInfo in type.GetMethods())
            {
                string methodName = methodInfo.Name;
                methodName += "(";
                bool haveParameters = false;
                foreach (ParameterInfo parameter in methodInfo.GetParameters())
                {
                    haveParameters = true;
                    methodName += parameter.ParameterType.Name + " " + parameter.Name + ", ";
                }
                if (haveParameters)
                    methodName = methodName.Substring(0, methodName.Length - 2);
                methodName += ")";
                AssemblyMethod assemblyMethod = new AssemblyMethod();
                assemblyMethod.Name = methodName;
                assemblyType.AssMethods.Add(assemblyMethod);
            }
            assemblyType.AssMethods.Sort(delegate (AssemblyMethod x, AssemblyMethod y)
            {
                return x.Name.CompareTo(y.Name);
            });
            foreach (FieldInfo fieldInfo in type.GetFields())
            {
                AssemblyField assemblyField = new AssemblyField();
                assemblyField.Name = fieldInfo.FieldType.Name + " " + fieldInfo.Name;
                assemblyType.AssFields.Add(assemblyField);
            }
            assemblyType.AssFields.Sort(delegate (AssemblyField x, AssemblyField y)
            {
                return x.Name.CompareTo(y.Name);
            });
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                AssemblyProperty assemblyProperty = new AssemblyProperty();
                assemblyProperty.Name = propertyInfo.PropertyType.Name + " " + propertyInfo.Name;
                assemblyType.AssProperties.Add(assemblyProperty);
            }
            assemblyType.AssProperties.Sort(delegate (AssemblyProperty x, AssemblyProperty y)
            {
                return x.Name.CompareTo(y.Name);
            });
        }

        private void SortAssemblyResult(AssemblyResult assemblyResult)
        {
            assemblyResult.AssemblyNamespaces.Sort(delegate (AssemblyNamespace x, AssemblyNamespace y)
            {
                return x.Namespace.CompareTo(y.Namespace);
            });
            foreach (AssemblyNamespace assemblyNamespace in assemblyResult.AssemblyNamespaces)
            {
                assemblyNamespace.AssTypes.Sort(delegate (AssemblyType x, AssemblyType y)
                {
                    return x.AssType.CompareTo(y.AssType);
                });
            }
        }

        public AssemblyResult Run(string filename)
        {
            assembly = Assembly.LoadFrom(filename);
            AssemblyResult assemblyResult = new AssemblyResult();
            assemblyResult.AssemblyName = assembly.FullName;

            Type[] types;
            types = assembly.GetTypes();
            foreach (Type type in types)
            {
                AssemblyNamespace assemblyNamespace = GetAssemblyNamespace(assemblyResult, type);
                if (assemblyNamespace != null)
                {
                    AssemblyType assemblyType = GetAssemblyType(assemblyNamespace, type);
                    FillInformationInAssemblyType(assemblyType, type);
                }
            }
            SortAssemblyResult(assemblyResult);
            return assemblyResult;
        }
    }
}
