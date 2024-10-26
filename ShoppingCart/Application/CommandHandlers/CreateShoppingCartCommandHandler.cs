using Application.Commands;
using MediatR;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;

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
            var shoppingCart = mapper.Map<ShoppingCart>(request);
            return await repository.AddAsync(shoppingCart);
        }
    }
}
