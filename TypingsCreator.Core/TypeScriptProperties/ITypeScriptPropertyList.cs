using System.Text;

namespace TypingsCreator.Core.TypeScriptProperties
{
    public interface ITypeScriptPropertyList
    {
        void GeneratePropertyDefinitions(StringBuilder stringBuilder);
        bool HasProperties();
    }
}