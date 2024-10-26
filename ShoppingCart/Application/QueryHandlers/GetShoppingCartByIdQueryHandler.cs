using Application.DTOs;
using Application.Queries;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetShoppingCartByIdQueryHandler : IRequestHandler<GetShoppingCartByIdQuery, ShoppingCartDto>
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;
        public GetShoppingCartByIdQueryHandler(IShoppingCartRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<ShoppingCartDto> Handle(GetShoppingCartByIdQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await repository.GetByIdAsync(request.Id);
            return mapper.Map<ShoppingCartDto>(shoppingCart);
        }
    }
}
