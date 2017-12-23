using System;

namespace TypingsCreator.Core.Naming
{
    public class TypeScriptClassNameResolver : ITypeScriptClassNameResolver
    {
        public string GetClassName(Type classType)
        {
            return classType.Name;
        }
    }
}