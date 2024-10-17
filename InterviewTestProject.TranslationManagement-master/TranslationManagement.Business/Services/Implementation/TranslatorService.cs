using TranslationManagement.Business.Mappers;
using TranslationManagement.Business.Models;
using TranslationManagement.Business.Services.Interfaces;
using TranslationManagement.Database.Repositories.Interfaces;

namespace TranslationManagement.Business.Services.Implementation;
public class TranslatorService : ITranslatorService
{
    private readonly ITranslatorRepository _repository;
    private readonly TranslatorMapper _mapper;

    public TranslatorService(ITranslatorRepository repository, TranslatorMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public int CreateTranslator(TranslatorDto dto)
    {
        return _repository.Add(_mapper.DtoToEntity(dto));
    }

    public IEnumerable<TranslatorDto> GetTranslators()
    {
        return _repository.GetAll().Select(_mapper.EntityToDto);
    }

    public void UpdateTranslator(TranslatorDto dto)
    {
        _repository.Update(_mapper.DtoToEntity(dto));
    }
}
