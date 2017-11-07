using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TypingsCreator.Core.Models;

namespace TypingsCreator.Core.TypeScriptProperties
{
    public class TypeScriptPropertyList : IModelProvider
    {
        private readonly Type _modelType;
        private IList<TypeScriptProperty> _properties;

        public TypeScriptPropertyList(Type modelType)
        {
            _modelType = modelType;
            FindProperties();
        }

        public void GeneratePropertyDefinitions(StringBuilder stringBuilder)
        {
            var i = 1;
            foreach (var property in _properties)
            {
                var lineEnd = "";
                if (i < _properties.Count)
                {
                    lineEnd = ",";
                }
                i++;

                stringBuilder.AppendLine($"     {property.GeneratePropertyDefinition()}{lineEnd}");
            }
        }

        public void AddModelsToCollection(TypeScriptModelList modelCollection)
        {
            foreach (var property in _properties)
            {
                property.AddModelsToCollection(modelCollection);
            }
        }

        private void FindProperties()
        {
            _properties = new List<TypeScriptProperty>();
            var properties = _modelType.GetTypeInfo().DeclaredProperties;
            foreach (var property in properties)
            {
                var typeScriptProperty = new TypeScriptProperty(property);
                _properties.Add(typeScriptProperty);
            }
        }
    }
}