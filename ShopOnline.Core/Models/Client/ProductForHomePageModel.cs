using System.Collections.Generic;

namespace ShopOnline.Core.Models.Client
{
    public class ProductForHomePageModel
    {
        public ProductDetailInforModel[] ProductDetailsInfors { get; set; }
        public ProductTypeInforModel[] ProductTypeInfors { get; set; }
    }

    public class ProductDetailInforModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public ProductTypeInforModel ProductTypeInfor { get; set; }
    }

    public class ProductTypeInforModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
    }
}
