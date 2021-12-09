using ShopOnline.Business.Staff;
using ShopOnline.Core.Models.Order;
using System.Threading.Tasks;
using X.PagedList;

namespace ShopOnline.Business.Order
{
    public interface IOrderBusiness
    {
        Task<IPagedList<OrderInfor>> GetListOrderAsync(string sortOrder, string currentFilter, string searchString, int? page);
    }
}
