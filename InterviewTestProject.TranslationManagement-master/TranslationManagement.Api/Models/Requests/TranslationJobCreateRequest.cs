using Microsoft.AspNetCore.Http;

namespace TranslationManagement.Api.Models.Requests;

public record TranslationJobCreateRequest
{
    public string CustomerName { get; set; }
    public IFormFile File { get; set; }
}
