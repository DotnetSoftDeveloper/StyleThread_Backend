using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Features.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.DTO.Auth;

namespace WebApi.Controllers.Brand
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllBrands(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetAllBrandsQuery(), cancellationToken));
        }
        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] GenericCreateCommand<BrandDto, GenericResponse<BrandDto>> brandRequest, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(brandRequest, cancellationToken));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBrand(GenericUpdateCommand<BrandDto, GenericResponse<string>> updateBrandRequest, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(updateBrandRequest, cancellationToken));
        }
        [HttpDelete("{BrandId}")]
        public async Task<IActionResult> DeleteBrand(GenericDeleteCommand<BrandDto, GenericResponse<string>> BrandId, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(BrandId, cancellationToken));
        }
    }
}
