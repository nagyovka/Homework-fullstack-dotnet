using External.ThirdParty.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        var reader = new StreamReader(dto.File.OpenReadStream());
        string content;

        if (dto.File.FileName.EndsWith(".txt"))
        {
            content = reader.ReadToEnd();
        }
        else if (dto.File.FileName.EndsWith(".xml"))
        {
            var xdoc = XDocument.Parse(reader.ReadToEnd());
            content = xdoc.Root.Element("Content").Value;
        }
        else
        {
            throw new NotSupportedException("unsupported file");
        }

        dto.OriginalContent = content;// call parser
        dto.TranslatedContent = "";
        dto.Price = dto.OriginalContent.Length * Constants.TranslationJobConstants.PricePerCharacter;
        var id = _repository.Add(_mapper.DtoToEntity(dto));
        var notificationSvc = new UnreliableNotificationService();
        while (!notificationSvc.SendNotification("Job created: " + id).Result)
        {
        }

        _logger.LogInformation("New job notification sent");
        return id;
    }

    public IEnumerable<TranslationJobDto> GetTranslationJobs()
    {
        return _repository.GetAll().Select(_mapper.EntityToDto);
    }

    public int UpdateTranslationJob(TranslationJobDto dto)
    {
        throw new NotImplementedException();
    }
}
