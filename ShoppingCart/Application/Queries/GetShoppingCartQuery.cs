using Application.DTOs;
using MediatR;


namespace Application.Queries
{
    public class GetShoppingCartQuery : IRequest<List<ShoppingCartDto>>
    {
    }
}
