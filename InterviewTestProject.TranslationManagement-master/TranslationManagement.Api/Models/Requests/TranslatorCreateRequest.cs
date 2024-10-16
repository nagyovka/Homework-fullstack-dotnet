namespace TranslationManagement.Api.Models.Requests;

public record TranslatorCreateRequest
{
    public string Name { get; set; }
    public string HourlyRate { get; set; }
    public string Status { get; set; }
    public string CreditCardNumber { get; set; }
}
