using api.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace api.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .Length(12)
                .Matches(new Regex(@"^(?=.*[!@#$%^&*()_+{}:""<>?~`\-=[$$\\;',./])(?=.*[A-Z])(?=.*\d).{12}$"))
                .WithMessage("Username must be 12 characters long with 1 special character, 1 uppercase letter, and 1 number.");
            RuleFor(x => x.PasswordHash).NotEmpty();
        }
    }
}
