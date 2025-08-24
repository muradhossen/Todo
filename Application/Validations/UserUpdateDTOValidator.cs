
using Application.DTOs.Users;
using FluentValidation;

namespace Application.Validations
{
    public class UserUpdateDTOValidator : AbstractValidator<UserUpdateDTO>
    {
        public UserUpdateDTOValidator()
        {
            RuleFor(x => x.FullName)
    .NotEmpty().WithMessage("Name is required")
    .Length(2, 50);
             
        }
    }
}
