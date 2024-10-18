using TranslationManagement.Database.Entities;
using TranslationManagement.Database.Repositories.Interfaces;

namespace TranslationManagement.Database.Repositories.Implementation;
public class TranslatorRepository : ITranslatorRepository
{
    private readonly AppDbContext _context;

    public TranslatorRepository(AppDbContext context)
    {
        _context = context;
    }

    public int Add(TranslatorEntity entity)
    {
        _context.Translators.Add(entity);
        _context.SaveChanges();
        return entity.Id;
    }

    public void Delete(int id)
    {
        var translator = _context.Translators.Find(id);
        if (translator != null)
        {
            _context.Translators.Remove(translator);
            _context.SaveChanges();
        }
    }

    public IEnumerable<TranslatorEntity> GetAll()
    {
        return _context.Translators.ToList();
    }

    public TranslatorEntity GetById(int id)
    {
        return _context.Translators.Find(id) ?? throw new ArgumentNullException();
    }

    public void Update(TranslatorEntity entity)
    {
        _context.Translators.Update(entity);
        _context.SaveChanges();
    }
}
