using System.Reflection;

namespace TypingsCreator.Core.Naming
{
    public interface ITypeScriptMethodNameResolver
    {
        string GetMethodName(MethodInfo method);
    }
}