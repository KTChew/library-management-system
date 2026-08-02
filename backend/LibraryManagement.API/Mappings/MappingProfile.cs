using AutoMapper;
using LibraryManagement.API.DTOs;
using LibraryManagement.API.Enums;
using LibraryManagement.API.Models;

namespace LibraryManagement.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Entity enum -> API string
        CreateMap<Book, BookDto>()
            .ForMember(
                destination => destination.Status,
                options => options.MapFrom(
                    source => source.Status.ToString()));

        // Status is assigned by the controller as a business rule.
        CreateMap<CreateBookDto, Book>()
            .ForMember(
                destination => destination.Status,
                options => options.Ignore());

        // API string -> existing entity enum
        CreateMap<UpdateBookDto, Book>()
           .ForMember(
               destination => destination.Status,
               options => options.MapFrom(
                   source => Enum.Parse<BookStatus>(
                       source.Status,
                       true)));
    }
}