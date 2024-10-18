using FluentValidation;
using TranslationManagement.Api.Models.Requests;
using TranslationManagement.Business.Enums;
using TranslationManagement.Database.Repositories.Interfaces;

namespace TranslationManagement.Api.Validators;

public class TranslatorUpdateRequestValidator : AbstractValidator<TranslatorUpdateRequest>
{
    private const string InvalidStatusChangeMessage = "Invalid status change";
    private const string InvalidTranslatorIdMessage = "Translator doesn't exist";

    private readonly ITranslatorRepository _repository;

    public TranslatorUpdateRequestValidator()
    {
    }

    public TranslatorUpdateRequestValidator(ITranslatorRepository repository)
    {
        _repository = repository;
        
        RuleFor(x => x.Status).Must(StatusIsValid).WithMessage(InvalidStatusChangeMessage);
        RuleFor(x => x.Id).Must(TranslatorExists).WithMessage(InvalidTranslatorIdMessage);
    }

    private bool StatusIsValid(string status)
    {
        return status == TranslatorStatus.Applicant.ToString() 
            || status == TranslatorStatus.Certified.ToString()
            || status == TranslatorStatus.Deleted.ToString();
    }

    private bool TranslatorExists(int id)
    {
        return _repository.GetById(id) != null;
    }
}
