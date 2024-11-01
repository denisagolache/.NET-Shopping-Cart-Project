﻿using Application.Commands;
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

        [HttpGet("id")]
        public async Task<ActionResult<ShoppingCartDto>> GetShoppingCartById(Guid id)
        {
            var cart =  await mediator.Send(new GetShoppingCartByIdQuery { Id = id });
            if (cart == null)
            {
                return NotFound();
            }
            return cart;
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
        [HttpPut("id")]
        public async Task<IActionResult> UpdateShoppingCart(Guid id, UpdateShoppingCartCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("This should be identical with command id");
            }
            await mediator.Send(command);
            return NoContent();
        }

    }
}
