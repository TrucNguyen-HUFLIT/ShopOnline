using Microsoft.AspNetCore.Mvc;

namespace ShopOnline.Controllers.Staff
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
