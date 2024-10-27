using System;
using System.Threading;
using System.Threading.Tasks;
using Application.CommandHandlers;
using Application.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using Xunit;

public class UpdateShoppingCartCommandHandlerTests
{
    private readonly IShoppingCartRepository repository;
    private readonly IMapper mapper;

    public UpdateShoppingCartCommandHandlerTests()
    {
        repository = Substitute.For<IShoppingCartRepository>();
        mapper = Substitute.For<IMapper>();
    }

    [Fact]
    public async Task UpdateShoppingCartCommandHandler_Success()
    {
        // Arrange
        var command = new UpdateShoppingCartCommand
        {
            Id = new Guid("a8a3b693-a136-4711-a43b-8d11415106bb"),
            CreatedAt = DateTime.UtcNow,
            Name = "UpdatedCart",
            TotalItems = 8,
            TotalPrice = 150.0m
        };
        var handler = new UpdateShoppingCartCommandHandler(repository, mapper);
        var shoppingCart = new ShoppingCart
        {
            Id = command.Id,
            CreatedAt = command.CreatedAt,
            Name = command.Name,
            TotalItems = command.TotalItems,
            TotalPrice = command.TotalPrice
        };

        mapper.Map<ShoppingCart>(command).Returns(shoppingCart);
        repository.UpdateAsync(Arg.Any<ShoppingCart>()).Returns(Task.CompletedTask);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        shoppingCart.Id.Should().Be(command.Id);
        await repository.Received(1).UpdateAsync(shoppingCart);
    }

    [Fact]
    public async Task Handle_ShouldThrowArgumentException_WhenNameIsEmpty()
    {
        // Arrange
        var command = new UpdateShoppingCartCommand
        {
            Id = Guid.NewGuid(),
            Name = "", 
            CreatedAt = DateTime.UtcNow,
            TotalItems = 5,
            TotalPrice = 100.0m
        };
        var handler = new UpdateShoppingCartCommandHandler(repository, mapper);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldThrowArgumentException_WhenCreatedAtIsDefault()
    {
        // Arrange
        var command = new UpdateShoppingCartCommand
        {
            Id = Guid.NewGuid(),
            Name = "UpdatedCart",
            CreatedAt = default,
            TotalItems = 5,
            TotalPrice = 100.0m
        };
        var handler = new UpdateShoppingCartCommandHandler(repository, mapper);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldThrowArgumentOutOfRangeException_WhenTotalItemsIsNegative()
    {
        // Arrange
        var command = new UpdateShoppingCartCommand
        {
            Id = Guid.NewGuid(),
            Name = "UpdatedCart",
            CreatedAt = DateTime.UtcNow,
            TotalItems = -5, 
            TotalPrice = 100.0m
        };
        var handler = new UpdateShoppingCartCommandHandler(repository, mapper);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldThrowArgumentOutOfRangeException_WhenTotalPriceIsNegative()
    {
        // Arrange
        var command = new UpdateShoppingCartCommand
        {
            Id = Guid.NewGuid(),
            Name = "UpdatedCart",
            CreatedAt = DateTime.UtcNow,
            TotalItems = 5,
            TotalPrice = -100.0m 
        };
        var handler = new UpdateShoppingCartCommandHandler(repository, mapper);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => handler.Handle(command, CancellationToken.None));
    }
}
