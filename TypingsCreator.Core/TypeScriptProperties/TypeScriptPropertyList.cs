using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TypingsCreator.Core.Classes;
using TypingsCreator.Core.Models;
using TypingsCreator.Core.TypeScriptProperties.Naming;

namespace TypingsCreator.Core.TypeScriptProperties
{
    public class TypeScriptPropertyList : IModelProvider, ITypeScriptPropertyList
    {
        private readonly Type _modelType;
        private readonly ITypeScriptPropertyNameResolver _typeScriptPropertyNameResolver;
        private readonly ITypeScriptClassFactory _typeScriptClassFactory;
        private readonly IList<TypeScriptProperty> _properties;

        public TypeScriptPropertyList(Type modelType, ITypeScriptPropertyNameResolver typeScriptPropertyNameResolver, ITypeScriptClassFactory typeScriptClassFactory)
        {
            _modelType = modelType;
            _typeScriptPropertyNameResolver = typeScriptPropertyNameResolver;
            _typeScriptClassFactory = typeScriptClassFactory;
            _properties = FindProperties();
        }

        public void GeneratePropertyDefinitions(StringBuilder stringBuilder)
        {
            var totalNumberOfProperties = _properties.Count;
            var i = 1;
            foreach (var property in _properties)
            {
                var propertyDefinition = $"     {property.GeneratePropertyDefinition()}";
                if (i < totalNumberOfProperties)
                {
                    stringBuilder.Append(propertyDefinition);
                    stringBuilder.AppendLine(",");
                }
                else
                {
                    stringBuilder.Append(propertyDefinition);
                }
                i++;
            }
        }

        public bool HasProperties()
        {
            return _properties.Count > 0;
        }

        public void AddModelsToCollection(TypeScriptModelList modelCollection)
        {
            foreach (var property in _properties)
            {
                property.AddModelsToCollection(modelCollection);
            }
        }

        private IList<TypeScriptProperty> FindProperties()
        {
            var properties = new List<TypeScriptProperty>();
            var declaredProperties = _modelType.GetTypeInfo().DeclaredProperties;
            foreach (var property in declaredProperties)
            {
                var typeScriptProperty = new TypeScriptProperty(property, _typeScriptPropertyNameResolver, _typeScriptClassFactory);
                properties.Add(typeScriptProperty);
            }

            return properties;
        }
    }
}