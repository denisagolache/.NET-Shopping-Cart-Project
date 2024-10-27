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
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentException("Name cannot be empty");
            }
            if (request.TotalItems < 0)
                throw new ArgumentOutOfRangeException(nameof(request.TotalItems), "TotalItems cannot be negative.");

            if (request.TotalPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(request.TotalPrice), "TotalPrice cannot be negative.");

            if (request.CreatedAt == default)
                throw new ArgumentException("CreatedAt must be a valid date.", nameof(request.CreatedAt));

            var shoppingCart = mapper.Map<ShoppingCart>(request);
            shoppingCart.Id = request.Id;
            return repository.UpdateAsync(shoppingCart);

        }
    }
}
