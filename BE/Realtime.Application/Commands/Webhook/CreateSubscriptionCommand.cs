using AutoMapper;
using MediatR;
using Realtime.Domain.Entity;
using Realtime.Infrastructure.Interfaces;

namespace Realtime.Application.Commands.Webhook
{
    public class CreateSubscriptionCommand : IRequest<WebhookSubscription>
    {
        public required string SubscriberName { get; set; }
        public required string Endpoint { get; set; }
        public required string SecretKey { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, WebhookSubscription>
    {
        private readonly IWebhookRepository _webHookRepository;
        private readonly IMapper _mapper;

        public CreateSubscriptionCommandHandler(IWebhookRepository webHookRepository, IMapper mapper)
        {
            _webHookRepository = webHookRepository;
            _mapper = mapper;
        }

        public async Task<WebhookSubscription> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<WebhookSubscription>(request);
            return await _webHookRepository.AddWebhookSubscriptionAsync(result, cancellationToken);
        }
    }
}