using Application.DTOs;
using MediatR;

namespace Application.Queries
{
    public class GetShoppingCartByIdQuery : IRequest<ShoppingCartDto>
    {
        public Guid Id { get; set; }
    }
}
