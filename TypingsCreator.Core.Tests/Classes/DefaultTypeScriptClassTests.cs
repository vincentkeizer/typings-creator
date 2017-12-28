using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypingsCreator.Core.Classes;
using TypingsCreator.Core.Models;
using TypingsCreator.Core.Tests.Classes.DummyClasses;

namespace TypingsCreator.Core.Tests.Classes
{
    [TestClass]
    public class DefaultTypeScriptClassTests
    {
        [TestMethod]
        public void DefaultTypeScriptClassTest_GeneratesDefinitionForProperties()
        {
            var typeScriptClass = new DefaultTypeScriptClass(typeof(DummyClassWithProperties));

            var definition = typeScriptClass.GenerateClassDefinition();

            Assert.AreEqual(@"interface DummyClassWithProperties {
     Id:any,
     Number:number,
     StringValue:string,
     DummyClass2:DummyClass2
}", definition);
        }

        [TestMethod]
        public void DefaultTypeScriptClassTest_GeneratesDefinitionForMethods()
        {
            var typeScriptClass = new DefaultTypeScriptClass(typeof(DummyClassWithMethods));

            var definition = typeScriptClass.GenerateClassDefinition();

            Assert.AreEqual(@"interface DummyClassWithMethods {
     Add(a:number, b:number):number
}", definition);
        }

        [TestMethod]
        public void DefaultTypeScriptClassTest_GeneratesDefinitionForMethodsAndProperties()
        {
            var typeScriptClass = new DefaultTypeScriptClass(typeof(DummyClassWithMethodsAndProperties));

            var definition = typeScriptClass.GenerateClassDefinition();
            
            Assert.AreEqual(@"interface DummyClassWithMethodsAndProperties {
     Id:any,
     Number:number,
     StringValue:string,
     DummyClass2:DummyClass2,
     Add(a:number, b:number):number
}", definition);
        }

        [TestMethod]
        public void DefaultTypeScriptClassTest_ModelListReturnsCorrectNumberOfModels()
        {
            var typeScriptClass = new DefaultTypeScriptClass(typeof(DummyClassWithMethodsAndProperties));
            var modelCollection = new TypeScriptModelList();
            typeScriptClass.AddModelsToCollection(modelCollection);

            Assert.AreEqual(1, modelCollection.GetModels().Count());
        }

        [TestMethod]
        public void DefaultTypeScriptClass_ForSameTypeAreEquaul()
        {
            var typeScriptClass = new DefaultTypeScriptClass(typeof(DummyClassWithMethodsAndProperties));
            var typeScriptClass2 = new DefaultTypeScriptClass(typeof(DummyClassWithMethodsAndProperties));

            Assert.IsTrue(typeScriptClass == typeScriptClass2);
            Assert.IsTrue(typeScriptClass.Equals(typeScriptClass2));
        }

        [TestMethod]
        public void DefaultTypeScriptClass_ForDifferentTypeAreNotEquaul()
        {
            var typeScriptClass = new DefaultTypeScriptClass(typeof(DummyClassWithMethodsAndProperties));
            var typeScriptClass2 = new DefaultTypeScriptClass(typeof(DummyClass2));

            Assert.IsFalse(typeScriptClass == typeScriptClass2);
            Assert.IsFalse(typeScriptClass.Equals(typeScriptClass2));
        }
    }
}
