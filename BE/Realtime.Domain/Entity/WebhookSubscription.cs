namespace Realtime.Domain.Entity
{
    public class WebhookSubscription
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string SubscriberName { get; set; } = string.Empty;

        public string Endpoint { get; set; } = string.Empty;

        public string SecretKey { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}