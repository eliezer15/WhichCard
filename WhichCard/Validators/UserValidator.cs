﻿using FluentValidation;
using WhichCard.Entities;

namespace WhichCard.Validators
{
    public class UserValidator : EntityValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.EmailAddress).NotNull().EmailAddress();
            RuleFor(user => user.FirstName).NotNull();
            RuleFor(user => user.LastName).NotNull();
        }
    }
}