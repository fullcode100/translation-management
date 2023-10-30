namespace TranslationManagement.Api.FileProcessors
{
  public class FileProcessorManager
  {
    public static IFileProcessor[] FileProcessors = new IFileProcessor[] {
      new TxtFileProcessor(),
      new XmlFileProcessor(),
      // Add your custom file processor object here to extend
    };

    public static async Task<(string?, string?)> Process(string filename, StreamReader reader, string? customer)
    {
      foreach (var processor in FileProcessors)
      {
        if (processor.CanProcess(filename))
        {
          return await processor.ProcessFile(reader, customer);
        }
      }

      throw new NotSupportedException("This filetype is not supported");
    }
  }
}
