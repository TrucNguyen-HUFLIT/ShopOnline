using Microsoft.AspNetCore.Mvc;

namespace ShopOnline.Controllers.Staff
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
