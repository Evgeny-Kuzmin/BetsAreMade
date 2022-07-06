using BetsAreMade.DataContracts.Dto.Users;
using FluentValidation;

namespace BetsAreMade.Controllers.Validator
{
    public class BetValidator : AbstractValidator<BetDto>
    {
        public BetValidator()
        {
            RuleFor(c => c.Team)
               .NotNull()
               .NotEmpty()
               .WithMessage("Required field");
        }
    }
}