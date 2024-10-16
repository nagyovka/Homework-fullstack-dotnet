using FluentValidation;
using TranslationManagement.Api.Models.Requests;
using TranslationManagement.Business.Enums;
using TranslationManagement.Database.Repositories.Interfaces;

namespace TranslationManagement.Api.Validators;

public class TranslationJobUpdateRequestValidator : AbstractValidator<TranslationJobUpdateRequest>
{
    private const string InvalidStatusChangeMessage = "Invalid status change";
    private const string InvalidJobIdMessage = "Job doesn't exist";
    private const string JobAlreadyCompletedMessage = "Job is already completed";
    private readonly ITranslationJobRepository _repository;
    public TranslationJobUpdateRequestValidator(ITranslationJobRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Status).Must(StatusIsValid).WithMessage(InvalidStatusChangeMessage);
        RuleFor(x => x.Id).Must(TranslationJobExists).WithMessage(InvalidJobIdMessage);
        RuleFor(x => x.Id).Must(TranslationJobStatusIsComplete).WithMessage(JobAlreadyCompletedMessage);
    }

    private bool TranslationJobExists(int id)
    {
        return _repository.GetById(id) != null;
    }

    private bool TranslationJobStatusIsComplete(int id)
    {
        var job = _repository.GetById(id);
        return job != null && job.Status == JobStatus.Complete.ToString();
    }

    private bool StatusIsValid(string status)
    {
        return status == JobStatus.Complete.ToString() || status == JobStatus.InProgress.ToString();
    }
}
