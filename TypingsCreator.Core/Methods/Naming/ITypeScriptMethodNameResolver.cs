using System.Reflection;

namespace TypingsCreator.Core.Methods.Naming
{
    public interface ITypeScriptMethodNameResolver
    {
        string GetMethodName(MethodInfo method);
    }
}