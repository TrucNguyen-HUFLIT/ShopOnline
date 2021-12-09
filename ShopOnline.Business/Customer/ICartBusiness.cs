using ShopOnline.Core.Models.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopOnline.Business.Customer
{
    public interface ICartBusiness
    {
        Task AddProductToCartAsync(int idProduct, int quantity);
        List<ProductCartModel> GetProductsCart();
        Task ReduceProductFromCartAsync(int idProduct, int? quantity);
        Task RemoveAllProductFromCartAsync();
        Task RemoveProductFromCartAsync(int idProduct);
    }
}
