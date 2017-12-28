using System;

namespace TypingsCreator.Core.Tests.Classes.DummyClasses
{
    public class DummyClassWithProperties
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public string StringValue { get; set; }

        public DummyClass2 DummyClass2 { get; set; }
    }
}