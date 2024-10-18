namespace TranslationManagement.Api.Models.Requests;

public record TranslationJobUpdateRequest
{
    public int Id { get; set; }
    public string Status { get; set; }
}
