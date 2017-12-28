# Typings creator

Class library for generating typings files from classes.

## NuGet

[TypingsCreator ](https://www.nuget.org/packages/TypingsCreator/)

```
Install-Package TypingsCreator
```

## Features

* Generates definition files for all models used in a class/type (properties, method arguments and method return types)
* Supports Array, IEnumerable, Collection types
* Default implementation for generating definition file for all public methods and properties

## Usage

### ITypeScriptClass

The ITypeScriptClass is used for generating definitions for a single Class/Type.
The DefaultTypeScriptClass is an implementation of ITypeScriptClass which provides an implementation for generating all public properties and methods of a single type.

### TypingsFileWriter

The TypingsFileWriter requires a list of ITypeScriptClass implementations and creates a definition file for every Class/Type.

### Example

In this example, the following c# classes are used:

```csharp
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
```

Creating definition files:

```csharp
var typeScriptClass = new DefaultTypeScriptClass(typeof(DummyClassWithMethodsAndProperties));

var writer = new TypingsFileWriter();
writer.WriteFiles(config.ProjectRootDir, config.RelativeOutputDir, new List<ITypeScriptClass> { typeScriptClass } );
```

Using the **DefaultTypeScriptClass** results in the following typescript files:

```csharp
interface DummyClassWithMethodsAndProperties {
     Id:any,
     Number:number,
     StringValue:string,
     DummyClass2:DummyClass2,
     Add(a:number, b:number):number
}
```
*DummyClassWithMethodsAndProperties.d.ts*

```csharp
interface DummyClass2 {
     Value1:string,
     Value2:string
}
```
*DummyClass2.d.ts*

