using Application.Commands;
using Application.CommandHandlers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace ShoppingCartUnitTests
{
    public class CreateShoppingCartCommandHandlerTests
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;

        public CreateShoppingCartCommandHandlerTests()
        {
            repository = Substitute.For<IShoppingCartRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task CreateShoppingCartCommandHandler_Success()
        {
            // Arrange
            var command = new CreateShoppingCartCommand
            {
                CreatedAt = DateTime.UtcNow,
                Name = "ShoppingCart1",
                TotalItems = 10,
                TotalPrice = 200.0m
            };
            var handler = new CreateShoppingCartCommandHandler(repository, mapper);

            var shoppingCart = new ShoppingCart
            {
                Id = Guid.NewGuid(),
                CreatedAt = command.CreatedAt,
                Name = command.Name,
                TotalItems = command.TotalItems,
                TotalPrice = command.TotalPrice
            };

            mapper.Map<ShoppingCart>(command).Returns(shoppingCart);
            repository.AddAsync(Arg.Any<ShoppingCart>()).Returns(shoppingCart.Id);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBe(Guid.Empty, "a new shopping cart should have a valid non-empty Guid");
        }


        [Fact]
        public async Task Handle_ShouldThrowArgumentException_WhenNameIsEmpty()
        {
            // Arrange
            var command = new CreateShoppingCartCommand
            {
                Name = "", // invalid
                CreatedAt = DateTime.UtcNow,
                TotalItems = 5,
                TotalPrice = 100.0m
            };
            var handler = new CreateShoppingCartCommandHandler(repository, mapper);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowArgumentException_WhenCreatedAtIsDefault()
        {
            // Arrange
            var command = new CreateShoppingCartCommand
            {
                Name = "ShoppingCart1",
                CreatedAt = default, // invalid
                TotalItems = 5,
                TotalPrice = 100.0m
            };
            var handler = new CreateShoppingCartCommandHandler(repository, mapper);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowArgumentOutOfRangeException_WhenTotalItemsIsNegative()
        {
            // Arrange
            var command = new CreateShoppingCartCommand
            {
                Name = "ShoppingCart1",
                CreatedAt = DateTime.UtcNow,
                TotalItems = -5, // invalid
                TotalPrice = 100.0m
            };
            var handler = new CreateShoppingCartCommandHandler(repository, mapper);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowArgumentOutOfRangeException_WhenTotalPriceIsNegative()
        {
            // Arrange
            var command = new CreateShoppingCartCommand
            {
                Name = "ShoppingCart1",
                CreatedAt = DateTime.UtcNow,
                TotalItems = 5,
                TotalPrice = -100.0m // invalid
            };
            var handler = new CreateShoppingCartCommandHandler(repository, mapper);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}