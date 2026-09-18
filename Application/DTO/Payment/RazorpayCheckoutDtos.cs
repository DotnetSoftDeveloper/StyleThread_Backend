using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.DTO.Payment;

/// <summary>
/// Amount is expressed in the currency's smallest unit (paise for INR).
/// For example, ₹499.00 is 49900.
/// </summary>
public sealed class RazorpayOrderRequestDTO
{
    [Range(1, long.MaxValue)]
    public long Amount { get; init; }

    [MaxLength(40)]
    public string? Receipt { get; init; }

    [MaxLength(3)]
    public string? Currency { get; init; }

    public Dictionary<string, string>? Notes { get; init; }
}

public sealed class RazorpayOrderResponseDTO
{
    [JsonPropertyName("keyId")]
    public required string KeyId { get; init; }

    [JsonPropertyName("orderId")]
    public required string OrderId { get; init; }

    public long Amount { get; init; }
    public required string Currency { get; init; }
    public required string Status { get; init; }
    public string? Receipt { get; init; }
}

public sealed class RazorpayPaymentVerificationRequestDTO
{
    [Required]
    [JsonPropertyName("razorpay_order_id")]
    public required string RazorpayOrderId { get; init; }

    [Required]
    [JsonPropertyName("razorpay_payment_id")]
    public required string RazorpayPaymentId { get; init; }

    [Required]
    [JsonPropertyName("razorpay_signature")]
    public required string RazorpaySignature { get; init; }
}

public sealed class RazorpayPaymentVerificationResponseDTO
{
    public bool Verified { get; init; }
    public required string OrderId { get; init; }
    public required string PaymentId { get; init; }
}
