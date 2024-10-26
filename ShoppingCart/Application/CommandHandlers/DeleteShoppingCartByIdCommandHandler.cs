using Application.Commands;
using Domain.Repositories;
using MediatR;

namespace Application.CommandHandlers
{
    public class DeleteShoppingCartByIdCommandHandler: IRequestHandler<DeleteShoppingCartByIdCommand>
    {
        private readonly IShoppingCartRepository repository;

        public DeleteShoppingCartByIdCommandHandler(IShoppingCartRepository repository)
        {
            this.repository = repository;
        }

        public async Task Handle(DeleteShoppingCartByIdCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteAsync(request.Id);
        }
    }
}
