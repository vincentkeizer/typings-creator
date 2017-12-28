using System;

namespace TypingsCreator.Core.Tests.Classes.DummyClasses
{
    public class DummyClassWithMethodsAndProperties
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public string StringValue { get; set; }

        public DummyClass2 DummyClass2 { get; set; }

        public int Add(int a, int b)
        {
            return a + b;
        }
    }

    public class DummyClass2
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }
    }
}
