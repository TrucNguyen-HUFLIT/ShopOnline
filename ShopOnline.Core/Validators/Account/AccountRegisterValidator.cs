using FluentValidation;
using ShopOnline.Core.Models.Account;

namespace ShopOnline.Core.Validators.Account
{
    public class AccountRegisterValidator : AbstractValidator<AccountRegister>
    {
        public AccountRegisterValidator()
        {
            RuleFor(actor => actor.Email).NotEmpty();
            RuleFor(actor => actor.FullName).NotEmpty();
            RuleFor(actor => actor.ConfirmPassword).NotEmpty();
            RuleFor(actor => actor.Password).NotEmpty().MinimumLength(6)
                    .Matches(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{6,20}$")
                    .WithMessage("Password contains at least 1 uppercase letters, 1 lowercase letters and 1 numbers, and the entire string is longer than 6");
        }
    }
}
