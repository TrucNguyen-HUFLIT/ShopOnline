using FluentValidation;
using ShopOnline.Core.Models.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Core.Validators.Staff
{
    public class StaffEditValidator:AbstractValidator<StaffEdit>
    {
        public StaffEditValidator()
        {
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
        }
    }
}
