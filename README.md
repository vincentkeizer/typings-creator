# Typings creator

Class library for generating typings files from classes.

## NuGet

[TypingsCreator ](https://www.nuget.org/packages/TypingsCreator/)

```
Install-Package TypingsCreator
```

## Features

* Generates definition files for all models used in the class (arguments and return types)
* Supports Array and IEnumerable types

## Usage

The TypingsFileWriter requires a list of ITypeScriptClass implementations and creates a definition file for every class.

### Example



```csharp
var writer = new TypingsFileWriter();
writer.WriteFiles(config.ProjectRootDir, config.RelativeOutputDir, typeScriptClasses);
```
