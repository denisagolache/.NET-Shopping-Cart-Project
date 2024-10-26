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
    public class GetShoppingCartByIdQueryHandlerTests
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;

        public GetShoppingCartByIdQueryHandlerTests()
        {
            repository = Substitute.For<IShoppingCartRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Handle_ShouldReturnShoppingCartDto_WhenShoppingCartExists()
        {
            //Arange
            Guid shoppingCartId = Guid.NewGuid();
            var shoppingCart = GenerateShoppingCart(shoppingCartId);
            repository.GetByIdAsync(shoppingCartId).Returns(shoppingCart);
            var query = new GetShoppingCartByIdQuery { Id = shoppingCartId};
            GenerateShoppingCartDTO(shoppingCart);
            var handler = new GetShoppingCartByIdQueryHandler(repository, mapper);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(shoppingCart.Id, result.Id);
            Assert.Equal(shoppingCart.Name, result.Name);
            Assert.Equal(shoppingCart.TotalItems, result.TotalItems);
            Assert.Equal(shoppingCart.TotalPrice, result.TotalPrice);

            //Fluent Assertions for additional verification
            result.Should().NotBeNull();
            result.Id.Should().Be(shoppingCart.Id);
            result.Name.Should().Be(shoppingCart.Name);
            result.TotalItems.Should().Be(shoppingCart.TotalItems);
            result.TotalPrice.Should().Be(shoppingCart.TotalPrice);
            
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenShoppingCartDoesNotExist()
        {
            //Arrange
            Guid shoppingCartId = Guid.NewGuid();
            repository.GetByIdAsync(shoppingCartId).Returns((ShoppingCart)null);
            var query = new GetShoppingCartByIdQuery { Id = shoppingCartId };
            var handler = new GetShoppingCartByIdQueryHandler(repository, mapper);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().BeNull();
        }

        private void GenerateShoppingCartDTO(ShoppingCart shoppingCart)
        {
            mapper.Map<ShoppingCartDto>(shoppingCart).Returns(new ShoppingCartDto
            {
                Id = shoppingCart.Id,
                Name = shoppingCart.Name,
                TotalItems = shoppingCart.TotalItems,
                TotalPrice = shoppingCart.TotalPrice
            });

        }

        private ShoppingCart GenerateShoppingCart(Guid id)
        {
            return new Domain.Entities.ShoppingCart
            {
                Id = id,
                CreatedAt = DateTime.UtcNow,
                Name = "ShoppingCart1",
                TotalItems = 3,
                TotalPrice = 150.00m
            };
        }
    }
}
