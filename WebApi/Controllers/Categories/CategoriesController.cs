using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Application.DTO.Auth;

namespace WebApi.Controllers.Categories
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetAllCategoriesQuery(), cancellationToken));
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(GenericCreateCommand<CategoryDto, GenericResponse<CategoryDto>> categoryRequest, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(categoryRequest, cancellationToken));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(GenericUpdateCommand<CategoryDto, GenericResponse<string>> updateCategoryRequest, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(updateCategoryRequest, cancellationToken));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(GenericDeleteCommand<CategoryDto, GenericResponse<string>> categoryId, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(categoryId, cancellationToken));
        }
    }
}
