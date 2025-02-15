namespace Realtime.Domain.Entity
{
    public class PaymentWeekHook
    {
        public Guid Id { get; set; }
        public int Type { get; set; }
        public WebHookData? Data { get; set; }
    }
}