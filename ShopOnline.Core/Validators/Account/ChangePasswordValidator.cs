using FluentValidation;
using ShopOnline.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Core.Validators.Account
{
    public class ChangePasswordValidator: AbstractValidator<ChangePassword>
    {
        public ChangePasswordValidator()
        {
            RuleFor(actor => actor.OldPassword).NotEmpty();
            RuleFor(actor => actor.NewPassword).NotEmpty();
            RuleFor(actor => actor.ConfirmPassword).Equal(actor => actor.NewPassword).WithMessage("Confirm password didn't match");
        }
    }
}
