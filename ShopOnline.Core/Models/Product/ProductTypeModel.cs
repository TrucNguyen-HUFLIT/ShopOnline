using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using X.PagedList;

namespace ShopOnline.Core.Models.Product
{
    public class ProductTypeModel
    {
        public ProductTypeInfor productType { get; set; }
        public IPagedList<ProductTypeInfor> ListProductType { get; set; }
    }
    public class ProductTypeInfor
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ProductTypeViewModel
    {
        public ProductTypeInfor productType { get; set; }
    }
}
