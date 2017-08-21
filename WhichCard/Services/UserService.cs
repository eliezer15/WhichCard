using System.Threading.Tasks;
using WhichCard.Entities;
using WhichCard.Repositories;
using WhichCard.Validators;

namespace WhichCard.Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        IEntityValidator<User> _userValidator;
        //ICreditCardService _creditCardService;

        public UserService(IUserRepository userRepo, IEntityValidator<User> userValidator)
        {
            _userRepository = userRepo;
            _userValidator = userValidator;
        }

        public async Task<User> CreateAsync(User user)
        {
            await _userValidator.ValidateAndThrowAsync(user);
            await _userRepository.InsertAsync(user);

            return await GetAsync(user.Id);
        }

		public async Task<User> UpdateAsync(string id, User updateUser)
		{
            var user = await GetAsync(id);
            if (user == null)
            {
                return null;
            }

            updateUser.Id = id;
            updateUser.CreationDate = user.CreationDate;

			await _userValidator.ValidateAndThrowAsync(updateUser);
            await _userRepository.InsertAsync(updateUser);

			return await GetAsync(updateUser.Id);
		}

        public Task<User> GetAsync(string userId) => _userRepository.GetAsync(userId);

        public async Task DeleteAsync(User user)
        {
            await _userRepository.DeleteAsync(user);
            // await _creditCardService.DeleteAllForUserAsync(string userId);
        }
    }
}
