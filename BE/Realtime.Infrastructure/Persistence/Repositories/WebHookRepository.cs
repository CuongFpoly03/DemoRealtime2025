using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Realtime.Domain.Entity;
using Realtime.Infrastructure.Interfaces;

namespace Realtime.Infrastructure
{
    public class WebHookRepository : IWebhookRepository
    {
        private readonly IRealtimeDbContext _RealTimeDbContext;
        private readonly ILogger<WebHookRepository> _logger;
        public WebHookRepository(IRealtimeDbContext RealTimeDbContext, ILogger<WebHookRepository> logger)
        {
            _RealTimeDbContext = RealTimeDbContext;
            _logger = logger;
        }
        #region  session Event
        public async Task<WebhookEvent> AddWebhookEventAsync(WebhookEvent request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            //defined
            request.CreatedAt = DateTime.UtcNow;
            request.Status = 1;
            await _RealTimeDbContext.WebhookEvents.AddAsync(request, cancellationToken);
            await _RealTimeDbContext.SaveChangesAsync(cancellationToken);
            return request;

        }
        #endregion
        #region session Subscription
        public async Task<List<WebhookSubscription>> GetActiveSubscriptionsAsync(CancellationToken cancellationToken = default)
        {
            return await _RealTimeDbContext.WebhookSubscriptions.Where(x => x.IsActive).ToListAsync(cancellationToken);

        }
        public async Task<WebhookSubscription> AddWebhookSubscriptionAsync(WebhookSubscription request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            await _RealTimeDbContext.WebhookSubscriptions.AddAsync(request, cancellationToken);
            await _RealTimeDbContext.SaveChangesAsync(cancellationToken);
            return request;
        }
        #endregion
        #region session Log
        public async Task<WebhookLog> AddWebhookLogAsync(WebhookLog request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            await _RealTimeDbContext.WebhookLogs.AddAsync(request, cancellationToken);
            await _RealTimeDbContext.SaveChangesAsync(cancellationToken);
            return request;
        }
        #endregion

    }
}