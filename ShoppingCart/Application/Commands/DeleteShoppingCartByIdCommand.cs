using MediatR;

namespace Application.Commands
{
    public class DeleteShoppingCartByIdCommand: IRequest
    {
        public Guid Id { get; set; }
    }
}
