namespace TranslationManagement.Api.FileProcessors
{
  public class TxtFileProcessor : IFileProcessor
  {
    public string ProcessFile(StreamReader reader)
    {
      return reader.ReadToEnd();
    }
  }
}
