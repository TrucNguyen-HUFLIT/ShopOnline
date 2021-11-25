using Microsoft.AspNetCore.Mvc;
using ShopOnline.Business.Staff;
using ShopOnline.Core.Filters;
using ShopOnline.Core.Models.Product;
using System;
using System.Threading.Tasks;

namespace ShopOnline.Controllers.Staff
{
    public class ProductController : Controller
    {

        private readonly IProductBusiness _productBusiness;
        public ProductController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }
        public async Task<IActionResult> ListBrand(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("name") ? "name_desc" : "name";

            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";

            //StaticAcc.Name = User.Claims.Where(x => x.Type == "name").FirstOrDefault().Value;

            if (searchString != null) page = 1;
            else searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;

            var model = new BrandModel
            {
                ListBrand = await _productBusiness.GetListBrandAsync(sortOrder, currentFilter, searchString, page)
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateBrand()
        {
            var model = new BrandViewModel
            {
                 brandCreate= new BrandCreate(),
            };
            return View(model);
        }

        [HttpPost]
        [TypeFilter(typeof(ModelStateAjaxFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        public async Task<IActionResult> CreateBrand([FromForm] BrandCreate staffCreate)
        {
            await _productBusiness.CreateBrandAsync(staffCreate);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditBrand(int id)
        {
            var model = new BrandViewModel
            {
                brandInfor = _productBusiness.GetBrandByIdAsync(id),
            };
            return View(model);
        }

        [HttpPost]
        [TypeFilter(typeof(ModelStateAjaxFilter))]
        public async Task<IActionResult> EditBrand(BrandInfor brandInfor)
        {
            await _productBusiness.EditBrandAsync(brandInfor);
            return RedirectToAction("Edit", new { id = brandInfor.Id });
        }

        public async Task<IActionResult> ListProductType(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("name") ? "name_desc" : "name";

            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";

            //StaticAcc.Name = User.Claims.Where(x => x.Type == "name").FirstOrDefault().Value;

            if (searchString != null) page = 1;
            else searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;

            var model = new ProductTypeModel
            {
                ListBrand = await _productBusiness.GetListBrand(),
                ListProductType = await _productBusiness.GetListProductTypeAsync(sortOrder, currentFilter, searchString, page)
            };
            return View(model);
        }


    }
}
