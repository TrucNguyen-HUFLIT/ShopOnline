using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Business;
using ShopOnline.Business.Customer;
using ShopOnline.Core.Models;
using ShopOnline.Core.Models.Client;
using System.Threading.Tasks;
using static ShopOnline.Core.Models.Enum.AppEnum;

namespace ShopOnline.Controllers.Customer
{
    public class CartController : Controller
    {
        private readonly ICartBusiness _cartBusiness;
        private readonly IUserBusiness _userBusiness;

        public CartController(ICartBusiness cartBusiness,
                IUserBusiness userBusiness)
        {
            _cartBusiness = cartBusiness;
            _userBusiness = userBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> ProductCartAsync()
        {
            var productCart = _cartBusiness.GetProductsCart();

            return View(productCart);
        }

        [Authorize(Roles = ROLE.CUSTOMER)]
        [HttpGet]
        public async Task<IActionResult> CheckOutAsync()
        {
            var productCart = _cartBusiness.GetProductsCart();
            var userInfor = _userBusiness.LoadInforUser(User);
            var response = new ProductCartViewModel
            {
                ProductCarts = productCart,
                UserInfor = userInfor
            };

            return View(response);
        }

        [Authorize(Roles = ROLE.CUSTOMER)]
        [HttpPost]
        public async Task<IActionResult> CheckOutAsync(PaymentMethod paymentMethod, string address)
        {
            int newOrderId = await _cartBusiness.CheckOutAsync(User, paymentMethod, address);
            return Ok(newOrderId);
        }

        [Authorize(Roles = ROLE.CUSTOMER)]
        [HttpGet]
        public async Task<IActionResult> DigitalPayment(int id)
        {
            var orderInfor = await _cartBusiness.GetOrderById(id);
            return View(orderInfor);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToCartAsync(int idProduct, int quantity)
        {
            await _cartBusiness.AddProductToCartAsync(idProduct, quantity);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ReduceProductFromCartAsync(int idProduct, int? quantity)
        {
            await _cartBusiness.ReduceProductFromCartAsync(idProduct, quantity);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveProductFromCartAsync(int idProduct)
        {
            await _cartBusiness.RemoveProductFromCartAsync(idProduct);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAllProductFromCartAsync()
        {
            await _cartBusiness.RemoveAllProductFromCartAsync();
            return Ok();
        }
    }
}
