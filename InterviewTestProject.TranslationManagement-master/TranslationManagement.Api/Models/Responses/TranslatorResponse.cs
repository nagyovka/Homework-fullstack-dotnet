namespace TranslationManagement.Api.Models.Responses;

public record TranslatorResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string HourlyRate { get; set; }
    public string Status { get; set; }
    public string CreditCardNumber { get; set; }
}
