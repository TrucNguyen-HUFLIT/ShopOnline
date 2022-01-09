using ShopOnline.Core.Models.Product;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace ShopOnline.Business.Staff
{
    public interface IProductBusiness
    {

        // Product Type
        Task<IPagedList<ProductTypeInfor>> GetListProductTypeAsync(string sortOrder, string currentFilter, string searchString, int? page);
        ProductTypeInfor GetProductTypeByIdAsync(int id);
        Task<bool> EditProductTypeAsync(ProductTypeInfor productType);
        Task CreateProductTypeAsync(ProductTypeInfor productTypeInfor);
        Task<bool> DeleteProductTypeAsync(int id);


        // ProductDetail
        Task<IPagedList<ProductDetailInfor>> GetListProductDetailAsync(string sortOrder, string currentFilter, string searchString, int? page);
        Task<List<ProductTypeInfor>> GetListProductType();
        ProductDetailUpdate GetProductDetailByIdAsync(int id);
        Task CreateProductDetailAsync(ProductDetailCreate productDetailCreate);
        Task<bool> UpdateProductDetailAsync(ProductDetailUpdate productDetailUpdate);
        Task<bool> DeleteProductDetailAsync(int id);

    }
}
