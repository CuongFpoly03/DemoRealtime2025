using System.Text;
using Microsoft.Extensions.Logging;
using Realtime.Domain.Entity;
using Realtime.Infrastructure.Interfaces;

namespace Realtime.Infrastructure
{
    public class WebHookRepository  : IWebhookRepository
    {
        private readonly IRealtimeDbContext _RealTimeDbContext;
        private readonly ILogger<WebHookRepository > _logger;
        private readonly HttpClient _httpClient;
        public WebHookRepository (IRealtimeDbContext RealTimeDbContext, ILogger<WebHookRepository > logger, HttpClient httpClient)
        {
            _RealTimeDbContext = RealTimeDbContext;
            _logger = logger;
            _httpClient = httpClient;
        }
        

    }
}