using MediatR;

namespace Application.Commands
{
    public class CreateShoppingCartCommand : IRequest<Guid>
    {
        public DateTime CreatedAt { get; set; }

        public string Name { get; set; }

        public int TotalItems { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
