using FluentValidation;
using ShopOnline.Core.Models.Staff;

namespace ShopOnline.Core.Validators.Staff
{
    public class StaffCreateValidator : AbstractValidator<StaffCreate>
    {
        public StaffCreateValidator()
        {
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$").WithMessage("Password minimum 6 characters, at least one uppercase letter, one lowercase letter and a number");
            RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.Password).WithMessage("ConfirmPassword and Password do not match");
        }
    }
}
