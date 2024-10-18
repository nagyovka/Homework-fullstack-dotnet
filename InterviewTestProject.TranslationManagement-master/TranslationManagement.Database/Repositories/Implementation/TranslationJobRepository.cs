using TranslationManagement.Database.Entities;
using TranslationManagement.Database.Repositories.Interfaces;

namespace TranslationManagement.Database.Repositories.Implementation;
public class TranslationJobRepository : ITranslationJobRepository
{
    private readonly AppDbContext _context;

    public TranslationJobRepository(AppDbContext context)
    {
        _context = context;
    }

    public int Add(TranslationJobEntity entity)
    {
        _context.TranslationJobs.Add(entity);
        _context.SaveChanges();
        return entity.Id;
    }

    public void Delete(int id)
    {
        var translationJob = _context.TranslationJobs.Find(id);
        if (translationJob != null) 
        {
            _context.TranslationJobs.Remove(translationJob);
            _context.SaveChanges();
        }
    }

    public IEnumerable<TranslationJobEntity> GetAll()
    {
        return _context.TranslationJobs.ToList();
    }

    public TranslationJobEntity GetById(int id)
    {
        return _context.TranslationJobs.Find(id) ?? throw new ArgumentNullException(); // TODO
    }

    public void Update(TranslationJobEntity entity)
    {
        _context.TranslationJobs.Update(entity);
        _context.SaveChanges();
    }
}
