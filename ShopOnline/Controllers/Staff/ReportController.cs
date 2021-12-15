using Microsoft.AspNetCore.Mvc;

namespace ShopOnline.Controllers.Staff
{
    public class ReportController : Controller
    {
        /*private readonly IReportBusiness _reportBusiness;

        public ReportController(ICustomerBusiness customerBusiness)
        {
            _customerBusiness = customerBusiness;
        }

        public async Task<IActionResult> ListCustomer(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("name") ? "name_desc" : "name";

            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";

            //StaticAcc.Name = User.Claims.Where(x => x.Type == "name").FirstOrDefault().Value;

            if (searchString != null) page = 1;
            else searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;

            var model = new CustomerModel
            {
                ListCustomer = await _customerBusiness.GetListCustomerAsync(sortOrder, currentFilter, searchString, page)
            };
            return View(model);*/

    }
}
