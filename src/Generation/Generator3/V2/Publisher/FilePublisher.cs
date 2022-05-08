using System;
using System.Collections.Generic;
using System.IO;

namespace Generator3.V2.Publisher
{
    public class FilePublisher
    {
        private string TargetFolder { get; set; }
        
        public FilePublisher(string targetFolder)
        {
            TargetFolder = targetFolder;
        }

        public void Publish(Controller.CodeUnit codeUnit)
        {
            var allFolders = new List<string>
            {
                TargetFolder, 
                codeUnit.Project, 
                GetFileName(codeUnit)
            };

            var path = Path.Combine(allFolders.ToArray());
            var directoryName = Path.GetDirectoryName(path);

            if (directoryName is null)
                throw new Exception("Can not get directory for path: " + path);

            Directory.CreateDirectory(directoryName);
            File.WriteAllText(path, codeUnit.Source);
        }
        
        private static string GetFileName(Controller.CodeUnit codeUnit) => codeUnit.Name + ".Generated.cs";
    }
}
