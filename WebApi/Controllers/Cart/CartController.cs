using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.DTO.Auth;

namespace WebApi.Controllers.Cart
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCartDataById([FromQuery] int customerId, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GenericFindByIdCommand<int, GenericResponse<List<CartDto>>>(customerId), cancellationToken));
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] GenericCreateCommand<CartDto, GenericResponse<CartDto>> CartData, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(CartData, cancellationToken));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCart(GenericUpdateCommand<CartDto, GenericResponse<CartDto>> UpdateCartData, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(UpdateCartData, cancellationToken));
        }
        [HttpDelete]
        public async Task<IActionResult> ClearCart([FromQuery] int cartId, CancellationToken cancellationToken)
        {
            var command = new GenericDeleteByIdCommand<int, GenericResponse<string>>(cartId);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}
