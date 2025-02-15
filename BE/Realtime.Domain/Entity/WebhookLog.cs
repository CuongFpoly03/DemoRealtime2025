using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Realtime.Domain.Entity
{
    public class WebhookLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid WebhookEventId { get; set; }
        public string Endpoint { get; set; } = string.Empty;
        public int AttemptCount { get; set; } = 0;
        public bool Success { get; set; } = false;
        public string Response { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}