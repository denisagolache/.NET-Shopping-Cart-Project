using Domain.Entities;
using Domain.Repositories;
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

        public async Task DeleteAsync(Guid id)
        {
            var shoppingCart = await context.ShoppingCarts.FirstOrDefaultAsync(sC => sC.Id == id);
            if (shoppingCart != null)
            {
                context.ShoppingCarts.Remove(shoppingCart);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllAsync()
        {
            return await context.ShoppingCarts.ToListAsync();
        }

        public async Task<ShoppingCart> GetByIdAsync(Guid id)
        {
            return await context.ShoppingCarts.FindAsync(id);
        }

        public Task UpdateAsync(ShoppingCart shoppingCart)
        {
            context.Entry(shoppingCart).State = EntityState.Modified;
            return context.SaveChangesAsync();
        }
    }
}
