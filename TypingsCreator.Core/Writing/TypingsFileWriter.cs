using System.Collections.Generic;
using System.IO;
using SignalRTypingsCreator.Core.Typings.Writing;
using TypingsCreator.Core.Classes;
using TypingsCreator.Core.Models;

namespace TypingsCreator.Core.Writing
{
    public class TypingsFileWriter
    {
        public IEnumerable<TypingsFile> WriteFiles(string projectRootDir, string relativeOutputDir, IEnumerable<ITypeScriptClass> typeScriptClasses)
        {
            var typingsFiles = new List<TypingsFile>();
            var typingsDir = GetTypingsDirectory(projectRootDir, relativeOutputDir);
            var modelCollection = new TypeScriptModelList();

            foreach (var typeScriptClass in typeScriptClasses)
            {
                var fullPath = typingsDir + typeScriptClass.GetTypingsFileName();
                AddFileToTypingsFileList(fullPath, typingsFiles);

                typeScriptClass.AddModelsToCollection(modelCollection);

                var fileContents = typeScriptClass.GenerateClassDefinition();
                WriteFile(fullPath, fileContents);


            }

            //            var typeScriptModelList = typeScriptClasses.GetTypeScriptModels();
            //            foreach (var typeScriptModel in typeScriptModelList.GetModels())
            foreach (var typeScriptModel in modelCollection.GetModels())
            {
                var fullPath = typingsDir + typeScriptModel.GetTypingsFileName();
                AddFileToTypingsFileList(fullPath, typingsFiles);

                var fileContents = typeScriptModel.GenerateModelDefinition();
                WriteFile(fullPath, fileContents);
            }

            return typingsFiles;
        }

        private string GetTypingsDirectory(string projectRootDir, string relativeOutputDir)
        {
            var fullDir = $"{projectRootDir}\\{relativeOutputDir}";
            EnsureDirectory(fullDir);
            return fullDir;
        }

        private void EnsureDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private void AddFileToTypingsFileList(string fullPath, List<TypingsFile> typingsFiles)
        {
            var typingsFile = new TypingsFile(fullPath);
            typingsFiles.Add(typingsFile);
        }

        private void WriteFile(string fullPath, string fileContents)
        {
            if (File.Exists(fullPath) && File.ReadAllText(fullPath) == fileContents)
            {
                return;
            }
                
            File.WriteAllText(fullPath, fileContents);
        }
    }
}