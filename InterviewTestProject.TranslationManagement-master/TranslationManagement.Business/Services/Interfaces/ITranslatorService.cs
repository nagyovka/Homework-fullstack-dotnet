using TranslationManagement.Business.Models;

namespace TranslationManagement.Business.Services.Interfaces;
public interface ITranslatorService
{
    IEnumerable<TranslatorDto> GetTranslators();
    int CreateTranslator(TranslatorDto dto);
    int UpdateTranslator(TranslatorDto dto);
}
