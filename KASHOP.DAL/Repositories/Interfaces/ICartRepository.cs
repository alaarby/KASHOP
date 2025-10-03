using KASHOP.DAL.Entities;

namespace KASHOP.DAL.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<int>AddAsync(Cart cart);
        Task<List<Cart>> GetUserCartAsync(string UserId);
        Task<bool> ClearCartAsync(string UserId);
    }
}
