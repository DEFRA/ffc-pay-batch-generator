using System;
using System.IO;
using System.Reflection;

namespace FFCPayBatchGenerator.Services
{
    public class FileService : IFileService
    {
        readonly string workingFolder = Path.Combine(AppContext.BaseDirectory, "Files");

        public string Generate(string filename, string content)
        {
            CreateDirectory();
            var filepath = Path.Combine(workingFolder, filename);
            File.WriteAllText(filepath, content);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("File successfully generated");
            Console.ResetColor();

            return filepath;
        }

        private void CreateDirectory()
        {
            Directory.CreateDirectory(workingFolder);
        }
    }
}
