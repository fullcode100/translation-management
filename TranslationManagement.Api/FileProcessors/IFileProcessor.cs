namespace TranslationManagement.Api.FileProcessors
{
  public interface IFileProcessor
  {
    string ProcessFile(StreamReader reader);
  }
}
