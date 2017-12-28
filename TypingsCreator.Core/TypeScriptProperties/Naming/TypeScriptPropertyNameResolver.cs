using System.Reflection;

namespace TypingsCreator.Core.TypeScriptProperties.Naming
{
    public class TypeScriptPropertyNameResolver : ITypeScriptPropertyNameResolver
    {
        public string GetPropertyName(PropertyInfo propertyInfo)
        {
            return propertyInfo.Name;
        }
    }
}