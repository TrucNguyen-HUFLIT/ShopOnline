using Microsoft.EntityFrameworkCore;
using ShopOnline.Business.Order;
using ShopOnline.Core;
using ShopOnline.Core.Models.Enum;
using ShopOnline.Core.Models.HistoryOrder;
using ShopOnline.Core.Models.Order;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public async Task<IPagedList<OrderInfor>> GetListOrderAsync(string sortOrder, string currentFilter, int? page)
        {
            var listOrder = new List<OrderInfor>();
            var orders = await _context.Orders.Where(x => !x.IsDeleted).ToListAsync();
            if (orders != null)
            {
                foreach (var order in orders)
                {
                    var totalPrice = await _context.OrderDetails.Where(x => x.IdOrder == order.Id).SumAsync(x => x.TotalPrice);
                    var orderInforForList = new OrderInfor
                    {
                        Id = order.Id,
                        OrderDay = order.OrderDay,
                        StatusOrder = order.StatusOrder,
                        IdCustomer = order.IdCustomer,
                        Address = order.Address,
                        ExtraFee = order.ExtraFee,
                        Payment =  order.Payment,
                        TotalPrice = totalPrice
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
                    var totalPrice = await _context.OrderDetails.Where(x => x.IdOrder == order.Id).SumAsync(x => x.TotalPrice);
                    var orderInforForList = new OrderInfor
                    {
                        Id = order.Id,
                        OrderDay = order.OrderDay,
                        StatusOrder = order.StatusOrder,
                        IdCustomer = order.IdCustomer,
                        Address = order.Address,
                        ExtraFee = order.ExtraFee,
                        Payment = order.Payment,
                        TotalPrice = totalPrice
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
                    var totalPrice = await _context.OrderDetails.Where(x => x.IdOrder == order.Id).SumAsync(x => x.TotalPrice);
                    var orderInforForList = new OrderInfor
                    {
                        Id = order.Id,
                        OrderDay = order.OrderDay,
                        StatusOrder = order.StatusOrder,
                        IdCustomer = order.IdCustomer,
                        Address = order.Address,
                        ExtraFee = order.ExtraFee,
                        Payment = order.Payment,
                        TotalPrice = totalPrice
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

        public async Task<IPagedList<HistoryOrderInfor>> GetHistoryOrderCustomerAsync(string sortOrder, string currentFilter, int? page, ClaimsPrincipal user)
        {
            string email = user.FindFirst(ClaimTypes.Email).Value;
            var customerId = _context.Customers.Where(x => x.Email == email && !x.IsDeleted).Select(x => x.Id).FirstOrDefault();
            var listHistoryOrder = new List<HistoryOrderInfor>();
            var historyOrders = await _context.Orders.Where(x => !x.IsDeleted && x.IdCustomer == customerId).ToListAsync();
            if (historyOrders != null)
            {
                foreach (var historyOrder in historyOrders)
                {
                    var totalPrice = await _context.OrderDetails.Where(x => x.IdOrder == historyOrder.Id).SumAsync(x => x.TotalPrice);
                    var historyOrderInfor = new HistoryOrderInfor
                    {
                        Id = historyOrder.Id,
                        OrderDay = historyOrder.OrderDay,
                        Address = historyOrder.Address,
                        ExtraFee = historyOrder.ExtraFee,
                        StatusOrder = historyOrder.StatusOrder,
                        Payment = historyOrder.Payment,
                        TotalPrice = totalPrice
                    };
                    listHistoryOrder.Add(historyOrderInfor);
                }
                listHistoryOrder = sortOrder switch
                {
                    "order_day_desc" => listHistoryOrder.OrderBy(x => x.OrderDay).ToList(),
                    _ => listHistoryOrder.OrderByDescending(x => x.OrderDay).ToList(),
                };
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return listHistoryOrder.ToPagedList(pageNumber, pageSize);
            }

            return null;
        }

        public async Task<IPagedList<HistoryOrderShipperInfor>> GetHistoryOrderShipperAsync(string sortOrder, string currentFilter, int? page, ClaimsPrincipal user)
        {
            string email = user.FindFirst(ClaimTypes.Email).Value;
            var shipperId = _context.Shippers.Where(x => x.Email == email && !x.IsDeleted).Select(x => x.Id).FirstOrDefault();
            var listHistoryOrderShipper = new List<HistoryOrderShipperInfor>();
            var historyOrdersShipper = await _context.Orders.Where(x => !x.IsDeleted && x.IdShipper == shipperId).ToListAsync();
            if (historyOrdersShipper != null)
            {
                foreach (var historyOrderShipper in historyOrdersShipper)
                {
                    var totalPrice = await _context.OrderDetails.Where(x => x.IdOrder == historyOrderShipper.Id).SumAsync(x => x.TotalPrice);
                    var historyOrderInfor = new HistoryOrderShipperInfor
                    {
                        Id = historyOrderShipper.Id,
                        OrderDay = historyOrderShipper.OrderDay,
                        Address = historyOrderShipper.Address,
                        ExtraFee = historyOrderShipper.ExtraFee,
                        StatusOrder = historyOrderShipper.StatusOrder,
                        Payment = historyOrderShipper.Payment,
                        IdCustomer = historyOrderShipper.IdCustomer,
                        TotalPrice = totalPrice
                    };
                    listHistoryOrderShipper.Add(historyOrderInfor);
                }
                listHistoryOrderShipper = sortOrder switch
                {
                    "order_day" => listHistoryOrderShipper.OrderBy(x => x.OrderDay).ToList(),
                    _ => listHistoryOrderShipper.OrderByDescending(x => x.OrderDay).ToList(),
                };
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return listHistoryOrderShipper.ToPagedList(pageNumber, pageSize);
            }
            return null;
        }

        public async Task<IPagedList<OrderInforShipper>> GetOrderShipperAsync(string sortOrder, string currentFilter, int? page, ClaimsPrincipal user)
        {
            string email = user.FindFirst(ClaimTypes.Email).Value;
            var shipperId = _context.Shippers.Where(x => x.Email == email && !x.IsDeleted).Select(x => x.Id).FirstOrDefault();
            var listOrderShipper = new List<OrderInforShipper>();
            var ordersShipper = await _context.Orders.Where(x => !x.IsDeleted && x.IdShipper == shipperId && x.StatusOrder == AppEnum.StatusOrder.Accepted).ToListAsync();
            if (ordersShipper != null)
            {
                foreach (var orderShipper in ordersShipper)
                {
                    var totalPrice = await _context.OrderDetails.Where(x => x.IdOrder == orderShipper.Id).SumAsync(x => x.TotalPrice);
                    var orderInforShipper = new OrderInforShipper
                    {
                        Id = orderShipper.Id,
                        OrderDay = orderShipper.OrderDay,
                        Address = orderShipper.Address,
                        ExtraFee = orderShipper.ExtraFee,
                        StatusOrder = orderShipper.StatusOrder,
                        Payment = orderShipper.Payment,
                        IdCustomer = orderShipper.IdCustomer,
                        TotalPrice = totalPrice
                    };
                    listOrderShipper.Add(orderInforShipper);
                }
                listOrderShipper = sortOrder switch
                {
                    "order_day" => listOrderShipper.OrderBy(x => x.OrderDay).ToList(),
                    _ => listOrderShipper.OrderByDescending(x => x.OrderDay).ToList(),
                };
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return listOrderShipper.ToPagedList(pageNumber, pageSize);
            }
            return null;
        }
    }
}
