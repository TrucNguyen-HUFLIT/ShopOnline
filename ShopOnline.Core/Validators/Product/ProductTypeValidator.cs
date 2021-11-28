using FluentValidation;
using ShopOnline.Core.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Core.Validators.Product
{
    public class ProductTypeValidator: AbstractValidator<ProductTypeInfor>
    {
        public ProductTypeValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
