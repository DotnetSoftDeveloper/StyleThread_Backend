using Application.DTO;
using Application.DTO.Payment;
using Application.Features.Command.GenericCommands;
using Application.Features.Queries;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.DTO.Auth;

namespace WebApi.Controllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRazorpayService _razorpayService;
        public PaymentController(IMediator mediator, IRazorpayService razorpayService)
        {
            _mediator = mediator;
            _razorpayService = razorpayService;
        }

        // Razorpay Checkout: create this order before opening the frontend checkout modal.
        [HttpPost("razorpay/orders")]
        [ProducesResponseType(typeof(RazorpayOrderResponseDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateRazorpayOrder([FromBody] RazorpayOrderRequestDTO request, CancellationToken cancellationToken)
        {
            var order = await _razorpayService.CreateOrderAsync(request, cancellationToken);
            return CreatedAtAction(nameof(CreateRazorpayOrder), order);
        }

        // Call after Razorpay Checkout returns razorpay_order_id, razorpay_payment_id and razorpay_signature.
        [HttpPost("razorpay/verify")]
        [ProducesResponseType(typeof(RazorpayPaymentVerificationResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult VerifyRazorpayPayment([FromBody] RazorpayPaymentVerificationRequestDTO request)
        {
            if (!_razorpayService.VerifyPaymentSignature(request))
                return BadRequest(new { success = false, message = "Invalid Razorpay payment signature." });

            return Ok(new RazorpayPaymentVerificationResponseDTO
            {
                Verified = true,
                OrderId = request.RazorpayOrderId,
                PaymentId = request.RazorpayPaymentId
            });
        }

        [Obsolete("Use POST api/Payment/razorpay/orders with Razorpay Checkout instead.")]
        [HttpPost("GetPaymentLink")]
        public async Task<IActionResult> GetPaymentLink([FromBody] RazorpayPaymentLinkRequestDTO requestPayment, CancellationToken cancellationToken)
        {

            var createPaymentLink = new GenericCreateCommand<RazorpayPaymentLinkRequestDTO, GenericResponse<RazorpayPaymentLinkResponseDTO>>(requestPayment);
            var response = await _mediator.Send(createPaymentLink, cancellationToken);

            return Ok(response);
        }
    }
}
