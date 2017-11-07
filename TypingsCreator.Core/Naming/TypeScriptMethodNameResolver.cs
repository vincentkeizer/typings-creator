using System.Reflection;

namespace TypingsCreator.Core.Naming
{
    public class TypeScriptMethodNameResolver : ITypeScriptMethodNameResolver
    {
        public string GetMethodName(MethodInfo method)
        {
            return method.Name;
        }
    }
}