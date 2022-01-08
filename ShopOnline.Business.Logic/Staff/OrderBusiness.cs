using ShopOnline.Business.Order;
using ShopOnline.Core;
using ShopOnline.Core.Models.Enum;
using ShopOnline.Core.Models.Order;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ShopOnline.Business.Logic.Staff
{
    public class OrderBusiness : IOrderBusiness
    {
        private readonly MyDbContext _context;
        public OrderBusiness(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IPagedList<OrderInfor>> GetListOrderAsync(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var listOrder = new List<OrderInfor>();
            var orders = await _context.Orders.Where(x => !x.IsDeleted).ToListAsync();
            if (orders != null)
            {
                foreach (var order in orders)
                {
                    var orderInforForList = new OrderInfor
                    {
                        Id = order.Id,
                        OrderDay = order.OrderDay,
                        StatusOrder = order.StatusOrder,
                        IdCustomer = order.IdCustomer,
                    };
                    listOrder.Add(orderInforForList);
                }
                listOrder = sortOrder switch
                {
                    "order_day_desc" => listOrder.OrderByDescending(x => x.OrderDay).ToList(),
                    _ => listOrder.OrderBy(x => x.Id).ToList(),
                };
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return listOrder.ToPagedList(pageNumber, pageSize);
            }

            return null;
        }

        public async Task<IPagedList<OrderInfor>> GetListOrderProcessAsync(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var listOrder = new List<OrderInfor>();
            var orders = await _context.Orders.Where(x => !x.IsDeleted && x.StatusOrder == AppEnum.StatusOrder.Processing).ToListAsync();
            if (orders != null)
            {
                foreach (var order in orders)
                {
                    var orderInforForList = new OrderInfor
                    {
                        Id = order.Id,
                        OrderDay = order.OrderDay,
                        StatusOrder = order.StatusOrder,
                        IdCustomer = order.IdCustomer,
                    };
                    listOrder.Add(orderInforForList);
                }
                listOrder = sortOrder switch
                {
                    "order_day_desc" => listOrder.OrderByDescending(x => x.OrderDay).ToList(),
                    _ => listOrder.OrderBy(x => x.Id).ToList(),
                };
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return listOrder.ToPagedList(pageNumber, pageSize);
            }

            return null;
        }

        public async Task<IPagedList<OrderInfor>> GetListOrderPaidAsync(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var listOrder = new List<OrderInfor>();
            var orders = await _context.Orders.Where(x => !x.IsDeleted && x.IsPaid).ToListAsync();
            if (orders != null)
            {
                foreach (var order in orders)
                {
                    var orderInforForList = new OrderInfor
                    {
                        Id = order.Id,
                        OrderDay = order.OrderDay,
                        StatusOrder = order.StatusOrder,
                        IdCustomer = order.IdCustomer,
                    };
                    listOrder.Add(orderInforForList);
                }
                listOrder = sortOrder switch
                {
                    "order_day_desc" => listOrder.OrderByDescending(x => x.OrderDay).ToList(),
                    _ => listOrder.OrderBy(x => x.Id).ToList(),
                };
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return listOrder.ToPagedList(pageNumber, pageSize);
            }

            return null;
        }
    }
}
