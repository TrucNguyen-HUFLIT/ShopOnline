
using ShopOnline.Core.Models.Client;
using System.Threading.Tasks;

namespace ShopOnline.Business.Customer
{
    public interface IClientBusiness
    {
        Task<ProductForHomePageModel> GetProductForHomePageAsync();
    }
}
