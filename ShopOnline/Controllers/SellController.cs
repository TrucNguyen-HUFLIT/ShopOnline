using Microsoft.AspNetCore.Mvc;

namespace ShopOnline.Controllers
{
    public class SellController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
