# StyleThread_Backend

## Razorpay Checkout API

Set the Razorpay test/live keys outside source control before starting the API:

```powershell
dotnet user-secrets init --project WebApi
dotnet user-secrets set "Razorpay:ApiKey" "rzp_test_..." --project WebApi
dotnet user-secrets set "Razorpay:ApiSecret" "..." --project WebApi
```

The frontend Checkout flow is:

1. `POST /api/Payment/razorpay/orders` with `{ "amount": 49900, "receipt": "checkout_123" }`. `amount` is in paise, so ₹499 is `49900`.
2. Open Razorpay Checkout with the returned `keyId`, `orderId`, `amount`, and `currency`.
3. Send Checkout's response unchanged to `POST /api/Payment/razorpay/verify`:

```json
{
  "razorpay_order_id": "order_...",
  "razorpay_payment_id": "pay_...",
  "razorpay_signature": "..."
}
```

Only treat the order as paid when `verified` is true. The `ApiSecret` must never be sent to the frontend. In production, calculate the amount from server-side cart/product data rather than trusting a client-provided amount.
