using System.Text;

namespace Realtime.Share.Helpers
{
    public static class MethodCommon
    {
        // gen ký tự ngẫu nhiên 
        public static string GenerateCode()
        {
            // Tạo phần ngẫu nhiên 12 ký tự (chữ hoa, chữ thường và số)
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var randomPart = new StringBuilder();

            for (int i = 0; i < 8; i++)
            {
                randomPart.Append(chars[random.Next(chars.Length)]);
            }

            // Lấy thời gian hiện tại
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss"); // Năm, tháng, ngày, giờ, phút, giây

            // Kết hợp phần ngẫu nhiên với thời gian
            return $"{randomPart}-{timestamp}";
        }

        // restfull response 
        public class ResponseData<Data>
        {
            public int Status { set; get; }
            public string Message { set; get; } = "";
            public Data? data { set; get; }
        }
    }
}