using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Business.Order;
using ShopOnline.Business.Staff;
using ShopOnline.Core.Models;
using ShopOnline.Core.Models.HistoryOrder;
using ShopOnline.Core.Models.Order;
using System;
using System.Threading.Tasks;
using static ShopOnline.Core.Models.Enum.AppEnum;

namespace ShopOnline.Controllers.Staff
{
    public class OrderController : Controller
    {
        private readonly IOrderBusiness _orderBusiness;
        private readonly IReviewBusiness _reviewBusiness;

        public OrderController(IOrderBusiness orderBusiness, IReviewBusiness reviewBusiness)
        {
            _orderBusiness = orderBusiness;
            _reviewBusiness = reviewBusiness;
        }

        [Authorize(Roles = ROLE.STAFF)]
        public async Task<IActionResult> ListOrder(string sortOrder, int statusOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.OrderDay = String.IsNullOrEmpty(sortOrder) ? "order_day_desc" : "";

            var enumStatusOrder = (StatusOrder)statusOrder;

            var model = new OrderModel
            {
                ListCustomer = await _reviewBusiness.GetListCustomer(),
                ListOrder = await _orderBusiness.GetListOrderAsync(sortOrder, enumStatusOrder, page)
            };

            return View(model);
        }

        [Authorize(Roles = ROLE.SHIPPER)]
        public async Task<IActionResult> ListHistoryOrderShipperAsync(string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.OrderDay = String.IsNullOrEmpty(sortOrder) ? "order_day" : "";

            var model = new HistoryOrderShipperModel
            {
                ListCustomer = await _reviewBusiness.GetListCustomer(),
                ListHistoryOrderShipper = await _orderBusiness.GetHistoryOrderShipperAsync(sortOrder, currentFilter, page, User)
            };

            return View(model);
        }

        [Authorize(Roles = ROLE.SHIPPER)]
        public async Task<IActionResult> ListOrderShipperAsync(string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.OrderDay = String.IsNullOrEmpty(sortOrder) ? "order_day" : "";

            var model = new OrderInforShipperModel
            {
                ListCustomer = await _reviewBusiness.GetListCustomer(),
                ListOrderInforShipper = await _orderBusiness.GetOrderAcceptedShipperAsync(sortOrder, currentFilter, page)
            };

            return View(model);
        }

        [Authorize(Roles = ROLE.SHIPPER)]
        public async Task<IActionResult> AcceptDelivery(int id)
        {
            await _orderBusiness.AcceptDeliveryAsync(id, User);
            return RedirectToAction("ListOrderShipper");
        }
    }
}
