namespace TranslationManagement.Api.FileProcessors
{
  public interface IFileProcessor
  {
    public bool CanProcess(string filename);
    public (string?, string?) ProcessFile(StreamReader reader, string? customer);
  }
}
