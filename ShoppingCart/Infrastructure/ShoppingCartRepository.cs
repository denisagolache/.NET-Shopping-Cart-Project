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

        public async Task<IEnumerable<ShoppingCart>> GetAllAsync()
        {
            return await context.ShoppingCarts.ToListAsync();
        }

        public Task<ShoppingCart> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ShoppingCart shoppingCart)
        {
            context.Entry(shoppingCart).State = EntityState.Modified;
            return context.SaveChangesAsync();
        }
    }
}
