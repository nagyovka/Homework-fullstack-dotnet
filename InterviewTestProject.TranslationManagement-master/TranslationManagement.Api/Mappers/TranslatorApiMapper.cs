using Riok.Mapperly.Abstractions;
using TranslationManagement.Api.Models.Requests;
using TranslationManagement.Api.Models.Responses;
using TranslationManagement.Business.Models;

namespace TranslationManagement.Api.Mappers;

[Mapper]
public partial class TranslatorApiMapper
{
    [MapperRequiredMapping(RequiredMappingStrategy.Source)]
    public partial TranslatorResponse DtoToResponse(TranslatorDto dto);
    public partial TranslatorDto RequestToDto(TranslatorCreateRequest request);
    public partial TranslatorDto RequestToDto(TranslatorUpdateRequest request);
}
