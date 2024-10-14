using TranslationManagement.Database.Entities;

namespace TranslationManagement.Database.Repositories.Interfaces;
public interface ITranslatorRepository
{
    IEnumerable<TranslatorEntity> GetAll();
    TranslatorEntity GetById(int id);
    int Add(TranslatorEntity entity);
    void Update(TranslatorEntity entity);
    void Delete(int id);
}
