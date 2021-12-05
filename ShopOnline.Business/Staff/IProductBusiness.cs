using ShopOnline.Core.Models.Product;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace ShopOnline.Business.Staff
{
    public interface IProductBusiness
    {
        // Product Brand 
        Task<IPagedList<BrandInfor>> GetListBrandAsync(string sortOrder, string currentFilter, string searchString, int? page);
        Task CreateBrandAsync(BrandCreate brandCreate);
        Task<bool> EditBrandAsync(BrandInfor brandInfor);
        BrandInfor GetBrandByIdAsync(int id);

        // Product Type
        Task<IPagedList<ProductTypeInfor>> GetListProductTypeAsync(string sortOrder, string currentFilter, string searchString, int? page);
        ProductTypeInfor GetProductTypeByIdAsync(int id);
        Task<bool> EditProductTypeAsync(ProductTypeInfor productType);
        Task<List<BrandInfor>> GetListBrand();
        Task CreateProductTypeAsync(ProductTypeInfor productTypeInfor);

        // ProductDetail
        Task<IPagedList<ProductDetailInfor>> GetListProductDetailAsync(string sortOrder, string currentFilter, string searchString, int? page);
        Task<List<ProductTypeInfor>> GetListProductType();
        ProductDetailUpdate GetProductDetailByIdAsync(int id);
        Task CreateProductDetailAsync(ProductDetailCreate productDetailCreate);
        Task<bool> UpdateProductDetailAsync(ProductDetailUpdate productDetailUpdate);

        // Product
        Task<IPagedList<ProductInfor>> GetListProductAsync(string sortOrder, string currentFilter, string searchString, int? page);
        Task<List<ProductDetailInfor>> GetListProductDetail();
        ProductUpdate GetProductByIdAsync(int id);
        Task CreateProductAsync(ProductCreate productCreate);
        Task<bool> UpdateProductAsync(ProductUpdate productUpdate);

    }
}
