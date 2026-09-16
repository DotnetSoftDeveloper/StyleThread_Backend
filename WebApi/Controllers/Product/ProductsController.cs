//using Application.Features.Command.BrandCommand;
//using Application.Features.Product.Command;
//using Application.Features.Product.Queries;
using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Features.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using static Application.DTO.Auth;

namespace WebApi.Controllers.Product
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetAllProductsQuery(), cancellationToken));

        }
        [HttpGet("GetAllAddProduct")]
        public async Task<IActionResult> GetAllAddProduct(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetAllAddProductQuery(), cancellationToken));
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto, CancellationToken cancellationToken)
        {

            var createProductCommand = new GenericCreateCommand<ProductDto, GenericResponse<ProductDto>>(productDto);
            var response = await _mediator.Send(createProductCommand, cancellationToken);

            return Ok(response);
        }
        [HttpPut("{ProductId}")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto updateProduct,  CancellationToken cancellationToken)
        {
            if (updateProduct != null)
            {
                var updateProductCommand = new GenericUpdateCommand<ProductDto, GenericResponse<string>>(updateProduct);
                var response = await _mediator.Send(updateProductCommand, cancellationToken);

                return Ok(response);
            }
            return BadRequest("Please enter the valid data");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(GenericDeleteCommand<ProductDto, GenericResponse<string>> deleteProduct, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(deleteProduct, cancellationToken));
        }
    }
}