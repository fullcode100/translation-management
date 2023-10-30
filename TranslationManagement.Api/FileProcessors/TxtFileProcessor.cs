namespace TranslationManagement.Api.FileProcessors
{
  public class TxtFileProcessor : IFileProcessor
  {
    public bool CanProcess(string filename)
    {
      return filename.ToLower().EndsWith(".txt");
    }

    public async Task<(string?, string?)> ProcessFile(StreamReader reader, string? customer)
    {
      return (await reader.ReadToEndAsync(), customer);
    }
  }
}
