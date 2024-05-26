using EventSourcing.API.Commands;
using EventSourcing.API.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto createCategoryDto)
        {
            await _mediator.Send(new CreateCategoryCommand() { CreateCategoryDto= createCategoryDto });
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> ChangeName(ChangeCategoryNameDto changeCategoryNameDto)
        {
            await _mediator.Send(new ChangeCategoryNameCommand() {  ChangeCategoryNameDto= changeCategoryNameDto });
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> ChangePrice(ChangeCategoryPriceDto changeCategoryPriceDto)
        {
            await _mediator.Send(new ChangeCategoryPriceCommand() { ChangeCategoryPriceDto= changeCategoryPriceDto });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteCategoryCommand { Id=id});
            return NoContent();
        }
    }
}
