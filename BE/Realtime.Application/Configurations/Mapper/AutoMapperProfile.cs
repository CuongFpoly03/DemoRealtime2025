using AutoMapper;
using Realtime.Application.Commands.TodoCommand;
using Realtime.Domain.Entity;

namespace Realtime.Application.Configurations.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Todo, CreateTodoCommand>().ReverseMap().ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));
        }

    }
}