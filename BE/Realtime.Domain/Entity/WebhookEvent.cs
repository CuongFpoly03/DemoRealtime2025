namespace Realtime.Domain.Entity
{
    public class WebhookEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string EventType { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int Status { get; set; } // 1Pending, 2Processed, 3Failed
    }
}