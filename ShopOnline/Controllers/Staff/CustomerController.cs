using Microsoft.AspNetCore.Mvc;

namespace ShopOnline.Controllers.Staff
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
