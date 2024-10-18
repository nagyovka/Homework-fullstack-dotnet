using Microsoft.AspNetCore.Http;
using System.Xml.Linq;

namespace TranslationManagement.Business
{
    public static class ContentParser
    {
        public static string ParseFile(IFormFile file)
        {
            var reader = new StreamReader(file.OpenReadStream());
            var fileExtension = Path.GetExtension(file.FileName);

            switch (fileExtension)
            {
                case "txt":
                    return reader.ReadToEnd();
                case "xml":
                    var xdoc = XDocument.Parse(reader.ReadToEnd());
                    return xdoc?.Root?.Element("Content")?.Value ?? throw new ArgumentException("XML document must contain Content element.");
                default:
                    throw new NotSupportedException("unsupported file");
            }
        }
    }
}
