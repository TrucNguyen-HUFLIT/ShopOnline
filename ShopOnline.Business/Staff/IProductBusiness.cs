﻿using ShopOnline.Core.Models.Product;
using System.Collections.Generic;
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
        Task<bool> EditProductTypeAsync(ProductTypeInfor productType);
        Task<List<BrandInfor>> GetListBrand();
        Task CreateProductTypeAsync(ProductTypeInfor productTypeInfor);

        Task<IPagedList<ProductDetailInfor>> GetListProductDetailAsync(string sortOrder, string currentFilter, string searchString, int? page);
        Task<List<ProductTypeInfor>> GetListProductType();

        Task<IPagedList<ProductInfor>> GetListProductAsync(string sortOrder, string currentFilter, string searchString, int? page);
        Task<List<ProductDetailInfor>> GetListProductDetail();

    }
}
