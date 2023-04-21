using AutoMapper;
using Modsen.Domain.Dto;
using Modsen.Domain.Models;

namespace Modsen.Mapper.Extensions;

public static class MapToDtoExtension
{
    public static BookDto MapToDto(this BookModel? bookModel, IMapper mapper)
    {
        return mapper.Map<BookDto>(bookModel);
    }
}