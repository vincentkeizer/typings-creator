using System;
using System.Text;
using TypingsCreator.Core.Methods;
using TypingsCreator.Core.Models;
using TypingsCreator.Core.Naming;

namespace TypingsCreator.Core.Classes
{
    public class DefaultTypeScriptClass : ITypeScriptClass
    {
        private readonly Type _classType;
        private readonly ITypeScriptClassNameResolver _classNameProvider;
        private TypeScriptMethodList _methodList;

        public DefaultTypeScriptClass()
        {
            _classType = this.GetType();
            _classNameProvider = new TypeScriptClassNameResolver();
            
            _methodList = new TypeScriptMethodList(_classType, new TypeScriptMethodNameResolver());
        }

        public string GetTypingsFileName()
        {
            var className = _classNameProvider.GetClassName(_classType);

            return $"{className}.d.ts"; 
        }

        public void AddModelsToCollection(TypeScriptModelList modelCollection)
        {
            _methodList.AddModelsToCollection(modelCollection);
        }

        public string GenerateClassDefinition()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"interface {GetClassName()} {{");
            _methodList.GenerateMethodDefinitions(stringBuilder);
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }

        public string GetClassName()
        {
            return _classNameProvider.GetClassName(_classType);
        }
    }
}
