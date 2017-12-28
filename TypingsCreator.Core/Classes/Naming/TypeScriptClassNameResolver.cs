using System;

namespace TypingsCreator.Core.Classes.Naming
{
    public class TypeScriptClassNameResolver : ITypeScriptClassNameResolver
    {
        public string GetClassName(Type classType)
        {
            return classType.Name;
        }
    }
}