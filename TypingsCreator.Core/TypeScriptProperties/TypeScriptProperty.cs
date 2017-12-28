using System.Reflection;
using TypingsCreator.Core.Classes;
using TypingsCreator.Core.Models;
using TypingsCreator.Core.TypeConversion;
using TypingsCreator.Core.TypeScriptProperties.Naming;

namespace TypingsCreator.Core.TypeScriptProperties
{
    public class TypeScriptProperty : IModelProvider, ITypeScriptProperty
    {
        private readonly PropertyInfo _property;
        private readonly ITypeScriptPropertyNameResolver _typeScriptPropertyNameResolver;
        private readonly TypeScriptTypeHandler _typeScriptTypeHandler;
        private readonly TypeScriptModelCreator _typeScriptModelCreator;

        public TypeScriptProperty(PropertyInfo property, ITypeScriptPropertyNameResolver typeScriptPropertyNameResolver, ITypeScriptClassFactory typeScriptClassFactory)
        {
            _property = property;
            _typeScriptPropertyNameResolver = typeScriptPropertyNameResolver;
            _typeScriptTypeHandler = new TypeScriptTypeHandler();
            _typeScriptModelCreator = new TypeScriptModelCreator(typeScriptClassFactory);
        }

        public void AddModelsToCollection(TypeScriptModelList modelCollection)
        {
            var model =_typeScriptModelCreator.CreateModel(_property);
            modelCollection.Add(model);
        }

        public string GeneratePropertyDefinition()
        {
            return $"{_typeScriptPropertyNameResolver.GetPropertyName(_property)}:{_typeScriptTypeHandler.GetTypeScriptType(_property.PropertyType)}";
        }
    }
}