
namespace Realtime.Domain.Entity
{
    public class RefreshToken
    {
        public Guid Id {get; set;}
        public Guid UserId {get; set;}
        public string Token { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpirationDate;
        public virtual ApplicationUser? User { get; set; }
        
    }
}