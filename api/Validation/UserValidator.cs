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
                .Length(12, 30).WithMessage("Username minimum of 12 characters and maximum of 30")
                .Matches(new Regex(@"^(?=.*[!@#$%^&*()_+{}:""<>?~`\-=[$$\\;',./])(?=.*[A-Z])(?=.*\d).{12}$"))
                .WithMessage("1 special character,1 uppercase letter,and 1 number.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .Length(12, 30).WithMessage("Password minimum of 12 characters and maximum of 30")
                .Matches(new Regex(@"^(?=.*[!@#$%^&*()_+{}:""<>?~`\-=[$$\\;',./])(?=.*[A-Z])(?=.*\d).{12}$"))
                .WithMessage("1 special character,1 uppercase letter,and 1 number.");
        }
    }
}
