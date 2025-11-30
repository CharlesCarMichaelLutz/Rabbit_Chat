using api.Models;
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
                .Length(12).WithMessage("Username must be 12 characters long")
                .Matches(new Regex(@"^(?=.*[!@#$%^&*()_+{}:""<>?~`\-=[$$\\;',./])(?=.*[A-Z])(?=.*\d).{12}$"))
                .WithMessage("1 special character,1 uppercase letter,and 1 number.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .Length(12).WithMessage("Password must be 12 characters long")
                .Matches(new Regex(@"^(?=.*[!@#$%^&*()_+{}:""<>?~`\-=[$$\\;',./])(?=.*[A-Z])(?=.*\d).{12}$"))
                .WithMessage("1 special character,1 uppercase letter,and 1 number.");
        }
    }
}
