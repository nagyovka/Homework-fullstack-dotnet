using System.Reflection.Metadata.Ecma335;

namespace TranslationManagement.Api.Models.Requests;

public record TranslationJobCreateRequest
{
    public string CustomerName { get; set; }
    public string OriginalContent { get; set; }
    public string TranslatedContent { get; set; }
    //public double Price { get; init; } = OriginalContent.Length * 0.01; // bude v DTO

}
