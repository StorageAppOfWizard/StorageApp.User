using FluentValidation;
using StorageApp.User.Application.DTO;

namespace StorageApp.User.Application.Validators
{
    public class UserLoginValidator : AbstractValidator<LoginUserDTO>
    {
        public UserLoginValidator()
        {

            RuleFor(x => x.Email)
                   .NotEmpty().WithMessage("Field E-mail is required")
                   .EmailAddress().WithMessage("Field must be a valid e-mail")
                   .Length(3, 100).WithMessage("Field must contain between 3 and 100 caracteres");
        }
    }
}
