using BetsAreMade.DataContracts.Dto.Users;
using FluentValidation;

namespace BetsAreMade.Controllers.Validator
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(c => c.FirstName)
               .NotNull()
               .NotEmpty()
               .WithMessage("Required field");

            RuleFor(c => c.LastName)
               .NotNull()
               .WithMessage("Required field");
        }
    }
}