using Microsoft.AspNetCore.Mvc;
using ShopOnline.Business.Customer;
using System.Threading.Tasks;

namespace ShopOnline.Controllers.Customer
{
    public class CartController : Controller
    {
        private readonly ICartBusiness _cartBusiness;

        public CartController(ICartBusiness cartBusiness)
        {
            _cartBusiness = cartBusiness;
        }

        [HttpGet]
        public IActionResult GetProductCart()
        {
            var productCart = _cartBusiness.GetProductsCart();
            return View(productCart);
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
