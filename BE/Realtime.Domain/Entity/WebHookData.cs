
namespace Realtime.Domain.Entity
{
    public class WebHookData
    {
        public Guid Id { get; set; }
        public Payment? Payment { get; set; }
    }
}