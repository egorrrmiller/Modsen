using AutoMapper;
using Modsen.Domain.Dto;
using Modsen.Domain.Models;

namespace Modsen.Mapper;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<BookModel, BookDto>().ReverseMap();
    }
}