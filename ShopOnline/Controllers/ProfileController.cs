using Microsoft.AspNetCore.Mvc;

namespace ShopOnline.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
