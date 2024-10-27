using MediatR;

namespace Application.Commands
{
    public class UpdateShoppingCartCommand : CreateShoppingCartCommand, IRequest
    {
        public Guid Id { get; set; }
    }
}
