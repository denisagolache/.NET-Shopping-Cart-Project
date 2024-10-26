using Domain.Entities;
using Domain.Repositories;
using System;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;

namespace Infrastructure
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext context;
        public ShoppingCartRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Guid> AddAsync(ShoppingCart shoppingCart)
        {
            await context.ShoppingCarts.AddAsync(shoppingCart);
            await context.SaveChangesAsync();
            return shoppingCart.Id;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ShoppingCart>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ShoppingCart> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }
    }
}
