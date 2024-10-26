using Application.Commands;
using Application.CommandHandlers;
using AutoMapper;
using Domain.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Application.Utils;
using Infrastructure;

namespace ShoppingCart.UnitTests
{
    public class CreateShoppingCartCommandTest
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public CreateShoppingCartCommandTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ShoppingCart")
                .Options;
            context = new ApplicationDbContext(options);

            repository = new ShoppingCartRepository(context);

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            mapper = new Mapper(configuration);
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

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
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
