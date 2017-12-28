using System.Collections.Generic;
using TypingsCreator.Core.Classes;

namespace TypingsCreator.Core.Models
{
    public class TypeScriptModelList
    {
        private readonly IList<ITypeScriptClass> _models;

        public TypeScriptModelList()
        {
            _models = new List<ITypeScriptClass>();    
        }

        public void Add(TypeScriptModelList typeScriptModelList)
        {
            var models = typeScriptModelList.GetModels();
            foreach (var model in models)
            {
                Add(model);
            }
         }

        public void Add(ITypeScriptClass model)
        {
            if (model == null) { return; }

            if (!_models.Contains(model))
            {
                _models.Add(model);

                model.AddModelsToCollection(this);
            }
        }

        public IEnumerable<ITypeScriptClass> GetModels()
        {
            return _models;
        }
    }
}