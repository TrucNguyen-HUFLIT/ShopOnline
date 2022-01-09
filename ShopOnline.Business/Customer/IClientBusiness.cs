
using ShopOnline.Core.Models.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopOnline.Business.Customer
{
    public interface IClientBusiness
    {
        Task InitTypes();
        Task<List<ProductInforViewModel>> GetProductsAsync(int? amountTake);
        Task<ProductDetailViewModel> GetDetailProductAsync(int id);
        Task<List<ProductInforModel>> GetCurrentProductsInforAsync(int amountTake);
        Task CreateReviewDetailAsync(ReviewDetailModel reviewDetail);
        Task<ProductsViewModel> GetProductsByTypeAsync(int typeId);
        Task<TypeInforModel> GetTypesAsync(int typeId);
    }
}
