using System.Reflection;
using TypingsCreator.Core.Models;
using TypingsCreator.Core.TypeConversion;

namespace TypingsCreator.Core.TypeScriptProperties
{
    public class TypeScriptProperty : IModelProvider, ITypeScriptProperty
    {
        private readonly PropertyInfo _property;
        private readonly TypeScriptTypeHandler _typeScriptTypeHandler;
        private readonly TypeScriptModelCreator _typeScriptModelCreator;

        public TypeScriptProperty(PropertyInfo property)
        {
            _property = property;
            _typeScriptTypeHandler = new TypeScriptTypeHandler();
            _typeScriptModelCreator = new TypeScriptModelCreator();
        }

        public void AddModelsToCollection(TypeScriptModelList modelCollection)
        {
            var model =_typeScriptModelCreator.CreateModel(_property);
            modelCollection.Add(model);
        }

        public string GeneratePropertyDefinition()
        {
            return $"     {_property.Name}:{_typeScriptTypeHandler.GetTypeScriptType(_property.PropertyType)}";
        }
    }
}