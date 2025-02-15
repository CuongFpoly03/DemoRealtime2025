using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Realtime.Share.Helpers
{
    public static  class WebhookSecurity
    {
        public static bool ValidateSignature(string payload, string secret, string ProvidedSignature) {
            using(var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret))){
                var computedSignature = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
                var base64String = Convert.ToBase64String(computedSignature);
                return base64String == ProvidedSignature;
            }
        }
    }
}