using Application.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CommandHandlers
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, Guid>
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;

        public CreateShoppingCartCommandHandler(IShoppingCartRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Name cannot be null or whitespace.", nameof(request.Name));

            if (request.TotalItems < 0)
                throw new ArgumentOutOfRangeException(nameof(request.TotalItems), "TotalItems cannot be negative.");

            if (request.TotalPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(request.TotalPrice), "TotalPrice cannot be negative.");

            if (request.CreatedAt == default)
                throw new ArgumentException("CreatedAt must be a valid date.", nameof(request.CreatedAt));

            var shoppingCart = mapper.Map<ShoppingCart>(request);
            shoppingCart.Id = Guid.NewGuid(); 

            return await repository.AddAsync(shoppingCart);
        }
    }
}
