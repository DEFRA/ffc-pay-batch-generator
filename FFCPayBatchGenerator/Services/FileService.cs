using System;
using System.IO;
using System.Reflection;

namespace FFCPayBatchGenerator.Services;
public class FileService : IFileService
{
    string workingFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Files");

    public void Generate(string fileName, string content)
    {
        CreateDirectory();
        File.WriteAllText(Path.Combine(workingFolder, fileName), content);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Batch file successfully generated");
        Console.ResetColor();
    }

    private void CreateDirectory()
    {
        Directory.CreateDirectory(workingFolder);
    }
}
