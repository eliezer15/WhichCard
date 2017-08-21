using System.Threading.Tasks;
using WhichCard.Entities;

namespace WhichCard.Services
{
    public interface IUserService
    {
		Task<User> CreateAsync(User user);

		Task<User> UpdateAsync(string id, User user);

		Task<User> GetAsync(string userId);

        Task DeleteAsync(User user);
    }
}
