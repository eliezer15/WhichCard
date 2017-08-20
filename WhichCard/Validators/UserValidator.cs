using FluentValidation;
using WhichCard.Entities;

namespace WhichCard.Validators
{
    public class UserValidator : EntityValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Id).NotNull().NotEmpty();
            RuleFor(user => user.Email).NotNull().EmailAddress();
            RuleFor(user => user.FirstName).NotNull().NotEmpty();
            RuleFor(user => user.LastName).NotNull().NotEmpty();
        }
    }
}
