using FluentValidation;
using ShopOnline.Core.Models.Account;

namespace ShopOnline.Core.Validators.Account
{
    public class AccountLoginValidator : AbstractValidator<AccountLogin>
    {
        public AccountLoginValidator()
        {
            RuleFor(actor => actor.Email).NotEmpty();
            RuleFor(actor => actor.Password).NotEmpty();
        }
    }
}
