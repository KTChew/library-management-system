using AutoMapper;
using LibraryManagement.API.DTOs;
using LibraryManagement.API.Models;

namespace LibraryManagement.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateBookDto, Book>();
        CreateMap<UpdateBookDto, Book>();
    }
}