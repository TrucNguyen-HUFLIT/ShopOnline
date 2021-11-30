using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using X.PagedList;
using static ShopOnline.Core.Models.Enum.AppEnum;

namespace ShopOnline.Core.Models.Product
{
    public class ProductDetailModel
    {
        public ProductDetailInfor productDetail { get; set; }
        public List<ProductTypeInfor> ListProductType { get; set; }
        public IPagedList<ProductDetailInfor> ListProductDetail { get; set; }
    }
    public class ProductDetailInfor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pic1 { get; set; }
        public int Price { get; set; }
        public ProductStatus Status { get; set; }
        [Display(Name = "Type")]
        public int IdProductType { get; set; }
    }
}
