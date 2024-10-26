using Domain.Repositories;
using NSubstitute;
using Application.Commands;
using Application.CommandHandlers;


namespace ShoppingCartUnitTests
{
    public class DeleteShoppingCartByIdCommandHandlerTests
    {
        private readonly IShoppingCartRepository repository;

        public DeleteShoppingCartByIdCommandHandlerTests()
        {
            repository = Substitute.For<IShoppingCartRepository>();
        }

        [Fact]
        public async Task Handler_ReceivesCommandProperly()
        {
            // Arrange
            var idToDelete = Guid.NewGuid();
            var command = new DeleteShoppingCartByIdCommand { Id = idToDelete };
            var handler = new DeleteShoppingCartByIdCommandHandler(repository);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            await repository.Received(1).DeleteAsync(idToDelete);
        }
    }
}