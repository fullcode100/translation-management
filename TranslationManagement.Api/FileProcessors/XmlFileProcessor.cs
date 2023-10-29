using System.Xml.Linq;

namespace TranslationManagement.Api.FileProcessors
{
  public class XmlFileProcessor : IFileProcessor
  {
    public string ProcessFile(StreamReader reader)
    {
      var xdoc = XDocument.Parse(reader.ReadToEnd());
      return xdoc.Root?.Element("Content")?.Value;
    }
  }
}
