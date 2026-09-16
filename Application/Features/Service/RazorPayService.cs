using Application.DTO.Payment;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Razorpay.Api;
using SixLabors.ImageSharp;

namespace Application.Features.Service
{
    internal class RazorPayService : IRazorpayService
    {
        private readonly string razorpayApiKey = "rzp_test_4EGictPixaiGLc";
        private readonly string razorpayApiSecret = "IGlb5VOIEkfq5MVFG7oU7Z6E";

        public async Task<RazorpayPaymentLinkResponse> CreatePaymentLinkAsync(RazorpayPaymentLinkRequest paymentLinkRequest)
        {
            try
            {
                RazorpayClient client = new RazorpayClient(razorpayApiKey, razorpayApiSecret);
                var paymentLinkData = ConvertToDictionary(paymentLinkRequest);
                PaymentLink paymentLink = client.PaymentLink.Create(paymentLinkData);

                return new RazorpayPaymentLinkResponse
                {
                    Id = paymentLink["id"].ToString(),
                    ReferenceId = paymentLink["reference_id"].ToString(),
                    ShortUrl = paymentLink["short_url"].ToString(),
                    Status = paymentLink["status"].ToString(),
                    Amount = Convert.ToInt32(paymentLink["amount"]),
                    Currency = paymentLink["currency"].ToString(),
                    ExpireBy = Convert.ToInt32(paymentLink["expire_by"]),
                    Description = paymentLink["description"].ToString(),
                    CreatedAt = Convert.ToInt64(paymentLink["created_at"])
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create payment link", ex);
            }
        }
        private Dictionary<string, object> ConvertToDictionary(object obj)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }
    }
}
