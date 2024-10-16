namespace TranslationManagement.Api.Models.Requests;

public record TranslatorUpdateRequest
{
    public int Id { get; set; }
    public string Status { get; set; }
}
