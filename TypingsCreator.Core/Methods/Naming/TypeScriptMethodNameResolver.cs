using System.Reflection;

namespace TypingsCreator.Core.Methods.Naming
{
    public class TypeScriptMethodNameResolver : ITypeScriptMethodNameResolver
    {
        public string GetMethodName(MethodInfo method)
        {
            return method.Name;
        }
    }
}