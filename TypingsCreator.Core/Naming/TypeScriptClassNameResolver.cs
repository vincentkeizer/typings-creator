using System;

namespace TypingsCreator.Core.Naming
{
    public class TypeScriptClassNameResolver : ITypeScriptClassNameResolver
    {
        public string GetClassName(Type hubType)
        {
            return hubType.Name;
        }
    }
}