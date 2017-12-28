using System;
using System.Text;
using TypingsCreator.Core.Classes.Naming;
using TypingsCreator.Core.Methods;
using TypingsCreator.Core.Methods.Naming;
using TypingsCreator.Core.Models;
using TypingsCreator.Core.TypeScriptProperties;
using TypingsCreator.Core.TypeScriptProperties.Naming;

namespace TypingsCreator.Core.Classes
{
    public class DefaultTypeScriptClass : ITypeScriptClass
    {
        private readonly Type _classType;
        private readonly ITypeScriptClassNameResolver _classNameProvider;
        private readonly TypeScriptMethodList _methodList;
        private readonly TypeScriptPropertyList _typeScriptPropertyList;

        public DefaultTypeScriptClass(Type type)
        {
            _classType = type;
            _classNameProvider = new TypeScriptClassNameResolver();

            var defaultTypeScriptClassFactory = new DefaultTypeScriptClassFactory();
            _typeScriptPropertyList = new TypeScriptPropertyList(_classType, new TypeScriptPropertyNameResolver(), defaultTypeScriptClassFactory);
            _methodList = new TypeScriptMethodList(_classType, new TypeScriptMethodNameResolver(), defaultTypeScriptClassFactory);
        }

        public string GetTypingsFileName()
        {
            var className = _classNameProvider.GetClassName(_classType);

            return $"{className}.d.ts"; 
        }

        public void AddModelsToCollection(TypeScriptModelList modelCollection)
        {
            _typeScriptPropertyList.AddModelsToCollection(modelCollection);
            _methodList.AddModelsToCollection(modelCollection);
        }

        public string GenerateClassDefinition()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"interface {GetClassName()} {{");
            _typeScriptPropertyList.GeneratePropertyDefinitions(stringBuilder);
            if (_typeScriptPropertyList.HasProperties() && _methodList.HasMethods())
            {
                stringBuilder.AppendLine(",");
            }
            _methodList.GenerateMethodDefinitions(stringBuilder);
            stringBuilder.AppendLine();
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

        public string GetClassName()
        {
            return _classNameProvider.GetClassName(_classType);
        }

        public override int GetHashCode()
        {
            return (_classType != null ? _classType.GetHashCode() : 0);
        }

        public bool Equals(ITypeScriptClass other)
        {
            if (other is DefaultTypeScriptClass)
            {
                return Equals((DefaultTypeScriptClass)other);
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DefaultTypeScriptClass) obj);
        }

        protected bool Equals(DefaultTypeScriptClass other)
        {
            return Equals(_classType, other._classType);
        }

        public static bool operator ==(DefaultTypeScriptClass obj1, DefaultTypeScriptClass obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator !=(DefaultTypeScriptClass obj1, DefaultTypeScriptClass obj2)
        {
            return !obj1.Equals(obj2);
        }
    }
}
