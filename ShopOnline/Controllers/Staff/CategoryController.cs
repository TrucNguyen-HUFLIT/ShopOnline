using Microsoft.AspNetCore.Mvc;

namespace ShopOnline.Controllers.Staff
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
