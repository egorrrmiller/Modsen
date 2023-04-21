using AutoMapper;
using Modsen.Domain.Dto;
using Modsen.Domain.Models;

namespace Modsen.Mapper.Extensions;

public static class MapToModelExtension
{
    public static BookModel? MapToModel(this BookDto bookDto, IMapper mapper)
    {
        return mapper.Map<BookModel>(bookDto);
    }
}