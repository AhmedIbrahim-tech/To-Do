using AutoMapper;
using TodoAPI.Domain.Models;
using TodoAPI.DTOS;

namespace TodoAPI.Helper.AutoMapper
{
    public class TodoMapper : Profile
    {
        public TodoMapper()
        {
            CreateMap<TodoDTO, Todo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.title))
                .ForMember(dest => dest.Completed, opt => opt.MapFrom(src => src.completed))
                .ForMember(dest => dest.CustomUserId, opt => opt.MapFrom(src => src.userId))
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<Todo, TodoDTO>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.completed, opt => opt.MapFrom(src => src.Completed))
                .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.CustomUserId));
                

        }
    }
}
