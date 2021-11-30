using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using static ShopOnline.Core.Models.Enum.AppEnum;

namespace ShopOnline.Core.Models.Product
{
    public class ProductModel
    {
        public ProductInfor productInfor { get; set; }
        public List<ProductDetailInfor> ListProductDetail { get; set; }
        public IPagedList<ProductInfor> ListProduct { get; set; }
    }

    public class ProductInfor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ProductSize Size { get; set; }
        public int IdProductDetail { get; set; }
    }
}
