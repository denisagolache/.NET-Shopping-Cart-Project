using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.CommandHandlers
{
    public class UpdateShoppingCartCommandHandler : IRequestHandler<UpdateShoppingCartCommand>
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;
        public UpdateShoppingCartCommandHandler(IShoppingCartRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public Task Handle(UpdateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = mapper.Map<ShoppingCart>(request);
            return repository.UpdateAsync(shoppingCart);
        }
    }
}
