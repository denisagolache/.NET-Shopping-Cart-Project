﻿using Application;
using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;



namespace ShoppingCart.Controllers
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
    }
}