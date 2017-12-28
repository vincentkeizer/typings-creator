using System;
using System.Reflection;
using TypingsCreator.Core.Classes;
using TypingsCreator.Core.TypeConversion;

namespace TypingsCreator.Core.Models
{
    public class TypeScriptModelCreator
    {
        private readonly ITypeScriptClassFactory _typeScriptClassFactory;
        private readonly TypeScriptTypeHandler _typeScriptTypeHandler;

        public TypeScriptModelCreator(ITypeScriptClassFactory typeScriptClassFactory)
        {
            _typeScriptClassFactory = typeScriptClassFactory;
            _typeScriptTypeHandler = new TypeScriptTypeHandler();
        }

        public TypeScriptModelList CreateModels(MethodInfo methodInfo)
        {
            var modelList = new TypeScriptModelList();

            var returnTypeModel = CreateTypeScriptModel(methodInfo.ReturnType);
            modelList.Add(returnTypeModel);
            
            var parameters = methodInfo.GetParameters();
            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                var parameterModel = CreateTypeScriptModel(parameter.ParameterType);
                modelList.Add(parameterModel);
            }

            return modelList;
        }

        public ITypeScriptClass CreateModel(PropertyInfo property)
        {
            return CreateTypeScriptModel(property.PropertyType);
        }

        private ITypeScriptClass CreateTypeScriptModel(Type type)
        {
            if (_typeScriptTypeHandler.IsUnknownType(type))
            {
                if (_typeScriptTypeHandler.IsCollection(type))
                {
                    var collectionType = _typeScriptTypeHandler.GetTypeFromCollection(type);
                    return _typeScriptClassFactory.Create(collectionType);
                }
                return _typeScriptClassFactory.Create(type);
            }
            
            return null;
        }
    }
}