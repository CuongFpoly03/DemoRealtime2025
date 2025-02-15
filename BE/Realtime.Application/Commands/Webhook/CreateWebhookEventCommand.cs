using System.Text;
using AutoMapper;
using MediatR;
using Realtime.Domain.Entity;
using Realtime.Infrastructure.Interfaces;

namespace Realtime.Application.Commands.Webhook
{
    public class CreateWebhookEventCommand : IRequest<WebhookEvent>
    {
        public string? EventType { get; set; }
        public required string Payload { get; set; }
    }

    public class CreateWebhookEventCommandHandler : IRequestHandler<CreateWebhookEventCommand, WebhookEvent>
    {
        private readonly IWebhookRepository _webHookRepository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        public CreateWebhookEventCommandHandler(IWebhookRepository webHookRepository, IMapper mapper, HttpClient httpClient)
        {
            _webHookRepository = webHookRepository;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        public async Task<WebhookEvent> Handle(CreateWebhookEventCommand request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<WebhookEvent>(request);
            await _webHookRepository.AddWebhookEventAsync(result, cancellationToken);

            // lay danh sach web subscription chua active
            var subscriptions = await _webHookRepository.GetActiveSubscriptionsAsync(cancellationToken);
            foreach (var subscription in subscriptions)
            {
                // gui webhook
                var response = await SendWebhookAsync(subscription.Endpoint, request.Payload);

                // luu log 
                await _webHookRepository.AddWebhookLogAsync(new WebhookLog()
                {
                    WebhookEventId = result.Id,
                    Endpoint = subscription.Endpoint,
                    AttemptCount = 1,
                    Success = response.IsSuccessStatusCode,
                    Response = await response.Content.ReadAsStringAsync()
                });
            }
            return result;
        }
        private async Task<HttpResponseMessage> SendWebhookAsync(string url, string payload)
        {
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync(url, content);
        }
    }
}