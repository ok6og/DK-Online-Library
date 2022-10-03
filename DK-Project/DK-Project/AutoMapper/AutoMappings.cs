using AutoMapper;
using DK_Project.Models.Models;
using DK_Project.Models.Requests;
using DK_Project.Models.Responses;

namespace DK_Project.AutoMapper
{
    internal class AutoMappings : Profile
    {
        public AutoMappings()
        {
            CreateMap<AddUpdateAuthorRequest, Author>();
            //CreateMap<Author, AddAuthorResponse>().ReverseMap();
            CreateMap<AddUpdateBookRequest, Book>();
            
        }
    }
}
