using Application.Commands;
using Application.DTOs;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace ShoppingCartManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator mediator;

        public ShoppingCartController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShoppingCart(CreateShoppingCartCommand command)
        {
            var id = await mediator.Send(command);
            return Ok(id);
        }

        [HttpGet]
        public async Task<ActionResult<List<ShoppingCartDto>>> GetShoppingCarts()
        {
            return await mediator.Send(new GetShoppingCartQuery());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await mediator.Send(new DeleteShoppingCartByIdCommand { Id = id });
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
