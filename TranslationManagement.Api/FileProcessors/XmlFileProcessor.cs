using System.Xml.Linq;

namespace TranslationManagement.Api.FileProcessors
{
  public class XmlFileProcessor : IFileProcessor
  {
    public bool CanProcess(string filename)
    {
      return filename.ToLower().EndsWith(".xml");
    }

    public async Task<(string?, string?)> ProcessFile(StreamReader reader, string? customer)
    {
      var xdoc = XDocument.Parse(await reader.ReadToEndAsync());
      return (xdoc.Root?.Element("Content")?.Value, customer);
    }
  }
}
