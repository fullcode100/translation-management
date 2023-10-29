namespace TranslationManagement.Api.FileProcessors
{
  public class TxtFileProcessor : IFileProcessor
  {
    public bool CanProcess(string filename)
    {
      return filename.ToLower().EndsWith(".txt");
    }

    public (string?, string?) ProcessFile(StreamReader reader, string? customer)
    {
      return (reader.ReadToEnd(), customer);
    }
  }
}
