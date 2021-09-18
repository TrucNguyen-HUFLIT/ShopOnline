﻿
using static ShopOnline.Core.Models.Enum.AppEnum;
using System.Collections.Generic;

namespace ShopOnline.Core.Entities
{
    public class ProductDetailEntity : BaseEntity
    {
        public string ProductName { get; set; }
        public string Pic1 { get; set; }
        public string Pic2 { get; set; }
        public string Pic3 { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public ProductStatus Status { get; set; }

        public int IProductType { get; set; }
        public ProductTypeEntity ProductType { get; set; }
        public ICollection<ProductEntity> Products { get; set; }
        public virtual ICollection<ReviewDetailEntity> ReviewDetails { get; set; }

    }
}
