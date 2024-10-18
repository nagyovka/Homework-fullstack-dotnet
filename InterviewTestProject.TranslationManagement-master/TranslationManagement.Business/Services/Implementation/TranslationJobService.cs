using External.ThirdParty.Services;
using Microsoft.Extensions.Logging;
using TranslationManagement.Business.Enums;
using TranslationManagement.Business.Mappers;
using TranslationManagement.Business.Models;
using TranslationManagement.Business.Services.Interfaces;
using TranslationManagement.Database.Repositories.Interfaces;

namespace TranslationManagement.Business.Services.Implementation;
public class TranslationJobService : ITranslationJobService
{
    private readonly ITranslationJobRepository _repository;
    private readonly TranslationJobMapper _mapper;
    private readonly ILogger<TranslationJobService> _logger;

    public TranslationJobService(ITranslationJobRepository repository, TranslationJobMapper mapper, ILogger<TranslationJobService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public int CreateTranslationJob(TranslationJobDto dto)
    {
        dto.Status = JobStatus.New.ToString();
        dto.OriginalContent = ContentParser.ParseFile(dto.File);// call parser
        dto.TranslatedContent = "";
        dto.Price = dto.OriginalContent.Length * Constants.TranslationJobConstants.PricePerCharacter;
        var id = _repository.Add(_mapper.DtoToEntity(dto));

        SendNotificationAsync(id).ConfigureAwait(false);
        _logger.LogInformation("New job notification sent");
        return id;
    }

    public IEnumerable<TranslationJobDto> GetTranslationJobs()
    {
        return _repository.GetAll().Select(_mapper.EntityToDto);
    }

    public void UpdateTranslationJob(TranslationJobDto dto)
    {
        _repository.Update(_mapper.DtoToEntity(dto));
    }

    private async Task SendNotificationAsync(int jobId)
    {
        var notificationSvc = new UnreliableNotificationService();
        while (!await notificationSvc.SendNotification("Job created: " + jobId))
        {
            await Task.Delay(100);
        }
    }
}
