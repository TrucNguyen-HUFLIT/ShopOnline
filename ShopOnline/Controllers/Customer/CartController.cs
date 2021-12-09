using Microsoft.AspNetCore.Mvc;
using ShopOnline.Business.Customer;
using System.Threading.Tasks;

namespace ShopOnline.Controllers.Customer
{
    public class CartController : Controller
    {
        private readonly ICartBusiness _cartBusiness;
        private readonly IClientBusiness _clientBusiness;

        public CartController(ICartBusiness cartBusiness,
                IClientBusiness clientBusiness)
        {
            _cartBusiness = cartBusiness;
            _clientBusiness = clientBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> ProductCartAsync()
        {
            await _clientBusiness.InitBrands();

            var productCart = _cartBusiness.GetProductsCart();
            return View(productCart);
        }

        public async Task<IActionResult> CheckOut()
        {
            await _clientBusiness.InitBrands();
            return View();
        }

        public async Task<IActionResult> DigitalPayment()
        {
            await _clientBusiness.InitBrands();
            return View();
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
