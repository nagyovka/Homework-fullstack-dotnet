using Microsoft.AspNetCore.Http;

namespace TranslationManagement.Business.Models;
public class TranslationJobDto
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public string? Status { get; set; }
    public string? OriginalContent { get; set; }
    public string? TranslatedContent { get; set; }
    public double Price { get; set; }
    public IFormFile? File { get; set; }
}
