using System.Threading.Tasks;
using WhichCard.Entities;
namespace WhichCard.Repositories
{
    public interface IUserRepository
    {
        Task InsertAsync(User user);
        Task DeleteAsync(User user);
        Task<User> GetAsync(string id);
    }
}
