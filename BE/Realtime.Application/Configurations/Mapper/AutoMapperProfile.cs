using AutoMapper;
using Realtime.Application.Commands.TodoCommand;
using Realtime.Application.Commands.Webhook;
using Realtime.Domain.Entity;

namespace Realtime.Application.Configurations.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Todo, CreateTodoCommand>().ReverseMap().ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<WebhookEvent, CreateWebhookEventCommand>().ReverseMap().ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<WebhookSubscription, CreateSubscriptionCommand>().ReverseMap().ForAllMembers(x => x.Condition((src, dest, srcMember) => srcMember != null));
        }

    }
}