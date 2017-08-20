using WhichCard.Entities;
using Amazon.Runtime;
using System.Threading.Tasks;

namespace WhichCard.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
		public Task DeleteAsync(User user) => _context.DeleteAsync(user);

        public Task<User> GetAsync(string email) => _context.LoadAsync<User>(email, DefaultOperationConfig);

		public Task InsertAsync(User user) => _context.SaveAsync(user);

    }
}
