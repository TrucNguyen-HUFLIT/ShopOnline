using FluentValidation;
using ShopOnline.Core.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Core.Validators.Product
{
    public class ProductDetailValidator: AbstractValidator<ProductDetailInfor>
    {
        public ProductDetailValidator()
        {
        }
    }
    public class ProductDetailCreateValidator : AbstractValidator<ProductDetailCreate>
    {
        public ProductDetailCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.UploadPic1).NotEmpty();
        }
    }
}
