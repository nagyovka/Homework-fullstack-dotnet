using TranslationManagement.Business.Models;

namespace TranslationManagement.Business.Services.Interfaces;
public interface ITranslationJobService
{
    IEnumerable<TranslationJobDto> GetTranslationJobs();
    int CreateTranslationJob(TranslationJobDto dto);
    void UpdateTranslationJob(TranslationJobDto dto);
}
