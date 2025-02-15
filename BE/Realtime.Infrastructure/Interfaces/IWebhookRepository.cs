using Realtime.Domain.Entity;

namespace Realtime.Infrastructure.Interfaces
{
    public interface IWebhookRepository
    {
        // session event
        Task<WebhookEvent> AddWebhookEventAsync(WebhookEvent request, CancellationToken cancellationToken = default);
        // session Subscription
        Task<List<WebhookSubscription>> GetActiveSubscriptionsAsync(CancellationToken cancellationToken = default);
        Task<WebhookSubscription> AddWebhookSubscriptionAsync(WebhookSubscription request, CancellationToken cancellationToken = default);
        // session Log
        Task<WebhookLog> AddWebhookLogAsync(WebhookLog request, CancellationToken cancellationToken = default);
    }
}