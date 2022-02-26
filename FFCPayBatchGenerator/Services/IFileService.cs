namespace FFCPayBatchGenerator.Services;
public interface IFileService
{
    string Generate(string fileName, string content);
}
