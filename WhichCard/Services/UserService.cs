using System.Threading.Tasks;
using WhichCard.Entities;
using WhichCard.Repositories;
using WhichCard.Validators;

namespace WhichCard.Services
{
    public class UserService
    {
        IUserRepository _userRepository;
        IEntityValidator<User> _userValidator;
        //ICreditCardService _creditCardService;

        public UserService(IUserRepository userRepo, IEntityValidator<User> userValidator)
        {
            _userRepository = userRepo;
            _userValidator = userValidator;
        }

        public async Task InsertAsync(User user)
        {
            await _userValidator.ValidateAndThrowAsync(user);
            await _userRepository.InsertAsync(user);
        }

        public Task<User> GetAsync(string userId) => _userRepository.GetAsync(userId);

        public async Task DeleteAsync(User user)
        {
            await _userRepository.DeleteAsync(user);
            // await _creditCardService.DeleteAllForUserAsync(string userId);
        }
    }
}
