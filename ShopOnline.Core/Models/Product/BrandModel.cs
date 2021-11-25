
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ShopOnline.Core.Models.Product
{
    public class BrandModel
    {
        public IPagedList<BrandInfor> ListBrand { get; set; }
    }
    public class BrandInfor
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
    }
    public class BrandCreate
    {
        public string BrandName { get; set; }
    }
    public class BrandViewModel
    {
        public BrandInfor brandInfor;
        public BrandCreate brandCreate;
    }
}
