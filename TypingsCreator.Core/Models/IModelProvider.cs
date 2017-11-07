namespace TypingsCreator.Core.Models
{
    public interface IModelProvider
    {
        void AddModelsToCollection(TypeScriptModelList modelCollection);
    }
}