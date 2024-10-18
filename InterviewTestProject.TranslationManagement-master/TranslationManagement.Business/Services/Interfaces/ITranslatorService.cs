using TranslationManagement.Business.Models;

namespace TranslationManagement.Business.Services.Interfaces;
public interface ITranslatorService
{
    IEnumerable<TranslatorDto> GetTranslators();
    TranslatorDto GetTranslatorById(int id);
    int CreateTranslator(TranslatorDto dto);
    void UpdateTranslator(TranslatorDto dto);
}
