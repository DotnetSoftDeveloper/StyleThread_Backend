using Application.DTO;
using Application.DTO.Payment;
using Application.Features.Command.GenericCommands;
using Application.Features.Queries;
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
        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("GetPaymentLink")]
        public async Task<IActionResult> GetPaymentLink([FromBody] RazorpayPaymentLinkRequestDTO requestPayment, CancellationToken cancellationToken)
        {

            var createPaymentLink = new GenericCreateCommand<RazorpayPaymentLinkRequestDTO, GenericResponse<RazorpayPaymentLinkResponseDTO>>(requestPayment);
            var response = await _mediator.Send(createPaymentLink, cancellationToken);

            return Ok(response);
        }
    }
}
  