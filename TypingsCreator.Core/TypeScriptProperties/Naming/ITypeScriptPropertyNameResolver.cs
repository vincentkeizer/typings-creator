using System.Reflection;

namespace TypingsCreator.Core.TypeScriptProperties.Naming
{
    public interface ITypeScriptPropertyNameResolver
    {
        string GetPropertyName(PropertyInfo propertyInfo);
    }
}