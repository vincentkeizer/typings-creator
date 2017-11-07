using TypingsCreator.Core.Files;
using TypingsCreator.Core.Models;

namespace TypingsCreator.Core.Classes
{
    public interface ITypeScriptClass : ITypeScriptFile, IModelProvider
    {
        string GenerateClassDefinition();
    }
}