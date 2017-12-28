using System.Text;

namespace TypingsCreator.Core.Methods
{
    public interface ITypeScriptMethodList
    {
        void GenerateMethodDefinitions(StringBuilder stringBuilder);
        bool HasMethods();
    }
}