using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TypingsCreator.Core.Models;
using TypingsCreator.Core.Naming;

namespace TypingsCreator.Core.Methods
{
    public class TypeScriptMethodList : IModelProvider
    {
        private readonly Type _type;
        private readonly ITypeScriptMethodNameResolver _typeScriptMethodNameResolver;
        private IList<TypeScriptMethod> _methods;

        public TypeScriptMethodList(Type type, ITypeScriptMethodNameResolver typeScriptMethodNameResolver)
        {
            _type = type;
            _typeScriptMethodNameResolver = typeScriptMethodNameResolver;
            FindMethods();
        }

        public void GenerateMethodDefinitions(StringBuilder stringBuilder)
        {
            var totalNumberOfMethods = _methods.Count;
            var i = 1;
            foreach (var method in _methods)
            {
                var lineEnd = "";
                if (i < totalNumberOfMethods)
                {
                    lineEnd = ",";
                }
                i++;

                stringBuilder.AppendLine($"     {method.GenerateMethodDefinition()}{lineEnd}");
            }
        }

        public void AddModelsToCollection(TypeScriptModelList modelCollection)
        {
            foreach (var method in _methods)
            {
                method.AddModelsToCollection(modelCollection);
            }
        }

        private void FindMethods()
        {
            _methods = new List<TypeScriptMethod>();
            if (_type == null)
            {
                return;
            }

            foreach (var method in _type.GetTypeInfo().DeclaredMethods)
            {
                if (method.DeclaringType == _type)
                {
                    var typeScriptMethod = new TypeScriptMethod(method, _typeScriptMethodNameResolver);
                    _methods.Add(typeScriptMethod);
                }
            }
        }
    }
}