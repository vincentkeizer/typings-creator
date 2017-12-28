using System;

namespace TypingsCreator.Core.Classes.Naming
{
    public interface ITypeScriptClassNameResolver
    {
        string GetClassName(Type classType);
    }
}