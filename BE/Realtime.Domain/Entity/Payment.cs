using System.ComponentModel;

namespace Realtime.Domain.Entity
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [Description("các sản phẩm mua")]
        public Guid ProductOrderId {get; set;}
        [Description("Tổng số tiền ")]
        public decimal Amount { get; set; }
        [Description("Tổng số tiền bằng chữ")]
        public string Currency { get; set; } = string.Empty;
        [Description("trạng thái của đơn hàng")]
        public int Status { get; set; }
    }
}