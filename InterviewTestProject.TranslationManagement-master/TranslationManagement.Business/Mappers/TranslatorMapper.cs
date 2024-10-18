using Riok.Mapperly.Abstractions;
using TranslationManagement.Business.Models;
using TranslationManagement.Database.Entities;

namespace TranslationManagement.Business.Mappers;
[Mapper]
public partial class TranslatorMapper
{
    public partial TranslatorDto EntityToDto(TranslatorEntity entity);
    public partial TranslatorEntity DtoToEntity(TranslatorDto dto);
}
