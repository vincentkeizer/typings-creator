using System;

namespace TypingsCreator.Core.Classes
{
    public interface ITypeScriptClassFactory
    {
        ITypeScriptClass Create(Type type);
    }
}