using Application.DTOs;
using Application.Queries;
using Application.QueryHandlers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace ShoppingCartUnitTests
{
    public class GetShoppingCartsQueryHandlerTests
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;

        public GetShoppingCartsQueryHandlerTests()
        {
            repository = Substitute.For<IShoppingCartRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfShoppingCarts()
        {
            // Arrange
            List<Domain.Entities.ShoppingCart> shoppingCarts = GenerateShoppingCarts();
            repository.GetAllAsync().Returns(shoppingCarts);
            var query = new GetShoppingCartQuery();
            GenerateShoppingCartDTOs(shoppingCarts);
            var handler = new GetShoppingCartsQueryHandler(repository, mapper);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(shoppingCarts.Count, result.Count);
            Assert.Equal(shoppingCarts.First().Id, result.First().Id);
            Assert.Equal(shoppingCarts.First().Name, result.First().Name);
            Assert.Equal(shoppingCarts.First().TotalItems, result.First().TotalItems);
            Assert.Equal(shoppingCarts.First().TotalPrice, result.First().TotalPrice);
            Assert.Equal(shoppingCarts.Last().Id, result.Last().Id);
            Assert.Equal(shoppingCarts.Last().Name, result.Last().Name);
            Assert.Equal(shoppingCarts.Last().TotalItems, result.Last().TotalItems);
            Assert.Equal(shoppingCarts.Last().TotalPrice, result.Last().TotalPrice);

            // Fluent Assertions for additional verification
            result.Should().NotBeNull();
            result.Count.Should().Be(shoppingCarts.Count);
            result.First().Id.Should().Be(shoppingCarts.First().Id);
            result.First().Name.Should().Be(shoppingCarts.First().Name);
            result.First().TotalItems.Should().Be(shoppingCarts.First().TotalItems);
            result.First().TotalPrice.Should().Be(shoppingCarts.First().TotalPrice);
            result.Last().Id.Should().Be(shoppingCarts.Last().Id);
            result.Last().Name.Should().Be(shoppingCarts.Last().Name);
            result.Last().TotalItems.Should().Be(shoppingCarts.Last().TotalItems);
            result.Last().TotalPrice.Should().Be(shoppingCarts.Last().TotalPrice);
        }

        private void GenerateShoppingCartDTOs(List<ShoppingCart> shoppingCarts)
        {
            mapper.Map<List<ShoppingCartDto>>(Arg.Any<List<Domain.Entities.ShoppingCart>>()).Returns(new List<ShoppingCartDto>
        {
            new ShoppingCartDto
            {
                Id = shoppingCarts.First().Id,
                Name = shoppingCarts.First().Name,
                TotalItems = shoppingCarts.First().TotalItems,
                TotalPrice = shoppingCarts.First().TotalPrice
            },
            new ShoppingCartDto
            {
                Id = shoppingCarts.Last().Id,
                Name = shoppingCarts.Last().Name,
                TotalItems = shoppingCarts.Last().TotalItems,
                TotalPrice = shoppingCarts.Last().TotalPrice
            }
        });
        }

        private List<Domain.Entities.ShoppingCart> GenerateShoppingCarts()
        {
            // Arrange
            return new List<Domain.Entities.ShoppingCart>
        {
            new Domain.Entities.ShoppingCart
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Name = "ShoppingCart1",
                TotalItems = 3,
                TotalPrice = 150.00m
            },
            new Domain.Entities.ShoppingCart
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Name = "ShoppingCart2",
                TotalItems = 5,
                TotalPrice = 250.00m
            }
        };
        }
    }

}
