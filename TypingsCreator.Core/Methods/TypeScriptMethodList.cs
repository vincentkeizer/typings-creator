using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TypingsCreator.Core.Classes;
using TypingsCreator.Core.Methods.Naming;
using TypingsCreator.Core.Models;

namespace TypingsCreator.Core.Methods
{
    public class TypeScriptMethodList : IModelProvider, ITypeScriptMethodList
    {
        private readonly Type _type;
        private readonly ITypeScriptMethodNameResolver _typeScriptMethodNameResolver;
        private readonly ITypeScriptClassFactory _typeScriptClassFactory;
        private readonly IList<TypeScriptMethod> _methods;

        public TypeScriptMethodList(Type type, ITypeScriptMethodNameResolver typeScriptMethodNameResolver, ITypeScriptClassFactory typeScriptClassFactory)
        {
            _type = type;
            _typeScriptMethodNameResolver = typeScriptMethodNameResolver;
            _typeScriptClassFactory = typeScriptClassFactory;
            _methods = FindMethods();
        }

        public void GenerateMethodDefinitions(StringBuilder stringBuilder)
        {
            var totalNumberOfMethods = _methods.Count;
            var i = 1;
            foreach (var method in _methods)
            {
                var methodDefinition = $"     {method.GenerateMethodDefinition()}";
                if (i < totalNumberOfMethods)
                {
                    stringBuilder.Append(methodDefinition);
                    stringBuilder.AppendLine(",");
                }
                else
                {
                    stringBuilder.Append(methodDefinition);
                }
                i++;
            }
        }

        public bool HasMethods()
        {
            return _methods.Count > 0;
        }

        public void AddModelsToCollection(TypeScriptModelList modelCollection)
        {
            foreach (var method in _methods)
            {
                method.AddModelsToCollection(modelCollection);
            }
        }
        
        private IList<TypeScriptMethod> FindMethods()
        {
            var methods = new List<TypeScriptMethod>();
            if (_type == null)
            {
                return new List<TypeScriptMethod>();
            }

            foreach (var method in _type.GetTypeInfo().DeclaredMethods)
            {
                if (method.DeclaringType == _type && !method.IsSpecialName)
                {
                    var typeScriptMethod = new TypeScriptMethod(method, _typeScriptMethodNameResolver, _typeScriptClassFactory);
                    methods.Add(typeScriptMethod);
                }
            }

            return methods;
        }
    }
}