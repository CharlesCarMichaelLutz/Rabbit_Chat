using api.Models.User;
using FluentValidation;
using System.Text.RegularExpressions;

namespace api.Validation
{
    public class UserValidator : AbstractValidator<UserRequest>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .Length(12, 30).WithMessage("Must be between 12 and 30 characters")
                .Matches("[A-Z]").WithMessage("Must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Must contain at least one special character.");
               
            RuleFor(x => x.Password)
                .NotEmpty()
                .Length(12, 30).WithMessage("Must be between 12 and 30 characters")
                .Matches("[A-Z]").WithMessage("Must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Must contain at least one special character.");

        }
    }
}
