using System;

namespace TypingsCreator.Core.Classes
{
    public class DefaultTypeScriptClassFactory : ITypeScriptClassFactory
    {
        public ITypeScriptClass Create(Type type)
        {
            return new DefaultTypeScriptClass(type);
        }
    }
}