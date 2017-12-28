using System;
using System.Reflection;
using System.Text;
using TypingsCreator.Core.Classes;
using TypingsCreator.Core.Methods.Naming;
using TypingsCreator.Core.Models;
using TypingsCreator.Core.TypeConversion;

namespace TypingsCreator.Core.Methods
{
    public class TypeScriptMethod : IModelProvider, ITypeScriptMethod
    {
        private readonly MethodInfo _method;
        private readonly TypeScriptTypeHandler _typeScriptTypeHandler;
        private readonly TypeScriptModelCreator _typeScriptModelCreator;
        private readonly ITypeScriptMethodNameResolver _typeScriptMethodNameResolver;

        public TypeScriptMethod(MethodInfo method, ITypeScriptMethodNameResolver typeScriptMethodNameResolver, ITypeScriptClassFactory typeScriptClassFactory)
        {
            _method = method;
            _typeScriptModelCreator = new TypeScriptModelCreator(typeScriptClassFactory);
            _typeScriptTypeHandler = new TypeScriptTypeHandler();
            _typeScriptMethodNameResolver = typeScriptMethodNameResolver;
        }

        public string GenerateMethodDefinition()
        {
            StringBuilder stringBuilder = new StringBuilder();

            var name = GetName();
            stringBuilder.Append(name).Append("(");

            AddParameters(stringBuilder);
            
            stringBuilder.Append("):");
            stringBuilder.Append(_typeScriptTypeHandler.GetTypeScriptType(_method.ReturnType));
            return stringBuilder.ToString();
        }

        public void AddModelsToCollection(TypeScriptModelList modelCollection)
        {
            var model = _typeScriptModelCreator.CreateModels(_method);
            modelCollection.Add(model);
        }

        private string GetName()
        {
            var methodName = _typeScriptMethodNameResolver.GetMethodName(_method);

            return methodName;
        }

        private void AddParameters(StringBuilder stringBuilder)
        {
            var parameters = _method.GetParameters();
            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                AddParameter(stringBuilder, parameter);
                if (i < parameters.Length - 1)
                {
                    stringBuilder.Append(", ");
                }
            }
        }

        private void AddParameter(StringBuilder stringBuilder, ParameterInfo parameter)
        {
            stringBuilder.Append(parameter.Name);
            stringBuilder.Append(":");
            stringBuilder.Append(_typeScriptTypeHandler.GetTypeScriptType(parameter.ParameterType));
        }
    }
}