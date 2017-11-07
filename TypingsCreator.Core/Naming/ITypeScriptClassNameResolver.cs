using System;

namespace TypingsCreator.Core.Naming
{
    public interface ITypeScriptClassNameResolver
    {
        string GetClassName(Type hubType);
    }
}