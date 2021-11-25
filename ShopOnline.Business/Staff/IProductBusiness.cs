using ShopOnline.Core.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ShopOnline.Business.Staff
{
    public interface IProductBusiness
    {
       Task<IPagedList<BrandInfor>> GetListBrandAsync(string sortOrder, string currentFilter, string searchString, int? page);
        Task CreateBrandAsync(BrandCreate brandCreate);
        Task<bool> EditBrandAsync(BrandInfor brandInfor);
        BrandInfor GetBrandByIdAsync(int id);

        Task<IPagedList<ProductTypeInfor>> GetListProductTypeAsync(string sortOrder, string currentFilter, string searchString, int? page);
        ProductTypeInfor GetProductTypeByIdAsync(int id);
        Task<bool> EditBrandAsync(ProductTypeInfor productTypeInfor);
        Task<List<BrandInfor>> GetListBrand();
    }
}
