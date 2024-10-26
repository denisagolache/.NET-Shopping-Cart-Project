using Domain.Entities;


namespace Domain.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<Guid> AddAsync(ShoppingCart shoppingCart);
        Task<ShoppingCart> GetByIdAsync(Guid id);
        Task<IEnumerable<ShoppingCart>> GetAllAsync();
        Task UpdateAsync(ShoppingCart shoppingCart);
        Task DeleteAsync(Guid id);
    }
}
