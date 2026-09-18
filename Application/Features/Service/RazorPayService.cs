using Application.DTO.Payment;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Razorpay.Api;
using System.Security.Cryptography;
using System.Text;

namespace Application.Features.Service
{
    internal class RazorPayService : IRazorpayService
    {
        private readonly string _razorpayApiKey;
        private readonly string _razorpayApiSecret;
        private readonly string _currency;

        public RazorPayService(IConfiguration configuration)
        {
            _razorpayApiKey = configuration["Razorpay:ApiKey"] ?? string.Empty;
            _razorpayApiSecret = configuration["Razorpay:ApiSecret"] ?? string.Empty;
            if (string.IsNullOrWhiteSpace(_razorpayApiKey) || string.IsNullOrWhiteSpace(_razorpayApiSecret))
                throw new InvalidOperationException("Razorpay:ApiKey and Razorpay:ApiSecret must be configured.");
            _currency = configuration["Razorpay:Currency"] ?? "INR";
        }

        public async Task<RazorpayPaymentLinkResponse> CreatePaymentLinkAsync(RazorpayPaymentLinkRequest paymentLinkRequest)
        {
            try
            {
                RazorpayClient client = CreateClient();
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

        public Task<RazorpayOrderResponseDTO> CreateOrderAsync(RazorpayOrderRequestDTO request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var currency = string.IsNullOrWhiteSpace(request.Currency) ? _currency : request.Currency.ToUpperInvariant();
            var orderData = new Dictionary<string, object>
            {
                ["amount"] = request.Amount,
                ["currency"] = currency,
                ["receipt"] = string.IsNullOrWhiteSpace(request.Receipt) ? $"receipt_{Guid.NewGuid():N}"[..40] : request.Receipt
            };

            if (request.Notes is { Count: > 0 })
                orderData["notes"] = request.Notes;

            var order = CreateClient().Order.Create(orderData);
            return Task.FromResult(new RazorpayOrderResponseDTO
            {
                KeyId = _razorpayApiKey,
                OrderId = order["id"].ToString()!,
                Amount = Convert.ToInt64(order["amount"]),
                Currency = order["currency"].ToString()!,
                Status = order["status"].ToString()!,
                Receipt = order["receipt"]?.ToString()
            });
        }

        public bool VerifyPaymentSignature(RazorpayPaymentVerificationRequestDTO request)
        {
            var payload = $"{request.RazorpayOrderId}|{request.RazorpayPaymentId}";
            var expected = HMACSHA256.HashData(Encoding.UTF8.GetBytes(_razorpayApiSecret), Encoding.UTF8.GetBytes(payload));
            byte[] received;
            try { received = Convert.FromHexString(request.RazorpaySignature); }
            catch (FormatException) { return false; }

            return CryptographicOperations.FixedTimeEquals(expected, received);
        }

        private RazorpayClient CreateClient() => new(_razorpayApiKey, _razorpayApiSecret);
        private Dictionary<string, object> ConvertToDictionary(object obj)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }
    }
}
