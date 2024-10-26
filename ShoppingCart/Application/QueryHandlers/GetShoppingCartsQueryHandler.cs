using Application.DTOs;
using Application.Queries;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.QueryHandlers
{
    public class GetShoppingCartsQueryHandler : IRequestHandler<GetShoppingCartQuery, List<ShoppingCartDto>>
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;

        public GetShoppingCartsQueryHandler(IShoppingCartRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<List<ShoppingCartDto>> Handle(GetShoppingCartQuery request, CancellationToken cancellationToken)
        {
            var shoppingCarts = await repository.GetAllAsync();
            return mapper.Map<List<ShoppingCartDto>>(shoppingCarts);
        }
    }
}
