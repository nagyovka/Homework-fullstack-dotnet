using TranslationManagement.Database.Entities;

namespace TranslationManagement.Database.Repositories.Interfaces;
public interface ITranslationJobRepository
{
    IEnumerable<TranslationJobEntity> GetAll();
    TranslationJobEntity GetById(int id);
    int Add(TranslationJobEntity entity);
    void Update(TranslationJobEntity entity);
    void Delete(int id);
}
