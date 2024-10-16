using Riok.Mapperly.Abstractions;
using TranslationManagement.Business.Models;
using TranslationManagement.Database.Entities;

namespace TranslationManagement.Business.Mappers;

[Mapper]
public partial class TranslationJobMapper
{
    public partial TranslationJobDto EntityToDto(TranslationJobEntity entity);
    public partial TranslationJobEntity DtoToEntity(TranslationJobDto dto);
}
