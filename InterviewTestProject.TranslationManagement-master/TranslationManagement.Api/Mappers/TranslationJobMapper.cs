using Riok.Mapperly.Abstractions;
using TranslationManagement.Api.Models.Requests;
using TranslationManagement.Api.Models.Responses;
using TranslationManagement.Business.Models;

namespace TranslationManagement.Api.Mappers;

[Mapper]
public partial class TranslationJobApiMapper
{
    [MapperRequiredMapping(RequiredMappingStrategy.Source)]
    [MapperIgnoreSource(nameof(TranslationJobDto.File))]
    public partial TranslationJobResponse DtoToResponse(TranslationJobDto dto);

    [MapperIgnoreTarget(nameof(TranslationJobDto.Id))]
    [MapperIgnoreTarget(nameof(TranslationJobDto.TranslatedContent))]
    [MapperIgnoreTarget(nameof(TranslationJobDto.OriginalContent))]
    [MapperIgnoreSource(nameof(TranslationJobCreateRequest.File))]
    public partial TranslationJobDto RequestToDto(TranslationJobCreateRequest request);
    public partial TranslationJobDto RequestToDto(TranslationJobUpdateRequest request);
}
