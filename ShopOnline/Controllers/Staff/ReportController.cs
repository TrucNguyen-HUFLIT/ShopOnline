using Microsoft.AspNetCore.Mvc;
using ShopOnline.Business.Staff;
using ShopOnline.Core.Models.Report;
using System.Threading.Tasks;

namespace ShopOnline.Controllers.Staff
{
    public class ReportController : Controller
    {
        private readonly IReportBusiness _reportBusiness;

        public ReportController(IReportBusiness reportBusiness)
        {
            _reportBusiness = reportBusiness;
        }

        public async Task<IActionResult> ListReport(int? page)
        {
            page = page ?? 1;
            var reportVM = new ReportViewModel
            {
                Reports = await _reportBusiness.GetListReportAsync(page)
            };
            return View(reportVM);
        }
    }
}
