using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Business.Staff;
using ShopOnline.Core.Filters;
using ShopOnline.Core.Models;
using ShopOnline.Core.Models.Product;
using System;
using System.Threading.Tasks;

namespace ShopOnline.Controllers.Staff
{
    [Authorize(Roles = ROLE.STAFF)]
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
            var model = new BrandCreateViewModel
            {
                BrandCreate = new BrandCreate(),
            };
            return View(model);
        }

        [HttpPost]
        [TypeFilter(typeof(ModelStateAjaxFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        public async Task<IActionResult> CreateBrand([FromForm] BrandCreate brandCreate)
        {
            await _productBusiness.CreateBrandAsync(brandCreate);
            return Ok();
        }

        [HttpGet]
        public IActionResult UpdateBrand(int id)
        {
            var model = new BrandInforViewModel
            {
                BrandInfor = _productBusiness.GetBrandByIdAsync(id),
            };
            return View(model);
        }

        [HttpPost]
        [TypeFilter(typeof(ModelStateAjaxFilter))]
        public async Task<IActionResult> UpdateBrand(BrandInfor brandInfor)
        {
            await _productBusiness.EditBrandAsync(brandInfor);
            return Ok(brandInfor.Id);
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

        [HttpGet]
        public async Task<IActionResult> CreateProductType()
        {
            var model = new ProductTypeViewModel
            {
                productType = new ProductTypeInfor(),
                ListBrand = await _productBusiness.GetListBrand(),
            };
            return View(model);
        }

        [HttpPost]
        [TypeFilter(typeof(ModelStateAjaxFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        public async Task<IActionResult> CreateProductType([FromForm] ProductTypeInfor productType)
        {
            await _productBusiness.CreateProductTypeAsync(productType);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProductTypeAsync(int id)
        {
            var model = new ProductTypeViewModel
            {
                productType = _productBusiness.GetProductTypeByIdAsync(id),
                ListBrand = await _productBusiness.GetListBrand(),
            };
            return View(model);
        }

        [HttpPost]
        [TypeFilter(typeof(ModelStateAjaxFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        public async Task<IActionResult> UpdateProductType(ProductTypeInfor productType)
        {
            await _productBusiness.EditProductTypeAsync(productType);
            return Ok(productType.Id);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProductDetail()
        {
            var model = new ProductDetailCreateViewModel
            {
                ProductDetailCreate = new ProductDetailCreate(),
                ListProductType = await _productBusiness.GetListProductType(),
            };
            return View(model);
        }

        [HttpPost]
        [TypeFilter(typeof(ModelStateAjaxFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        public async Task<IActionResult> CreateProductDetail([FromForm] ProductDetailCreate productDetailCreate)
        {
            await _productBusiness.CreateProductDetailAsync(productDetailCreate);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProductDetail(int id)
        {
            var model = new ProductDetailUpdateViewModel
            {
                ProductDetailUpdate = _productBusiness.GetProductDetailByIdAsync(id),
                ListProductType = await _productBusiness.GetListProductType(),
            };
            return View(model);
        }

        [HttpPost]
        [TypeFilter(typeof(ModelStateAjaxFilter))]
        public async Task<IActionResult> UpdateProductDetail(ProductDetailUpdate productDetailUpdate)
        {
            await _productBusiness.UpdateProductDetailAsync(productDetailUpdate);
            return Ok(productDetailUpdate.Id);
        }

        public async Task<IActionResult> ListProductDetail(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("name") ? "name_desc" : "name";

            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";

            //StaticAcc.Name = User.Claims.Where(x => x.Type == "name").FirstOrDefault().Value;

            if (searchString != null) page = 1;
            else searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;

            var model = new ProductDetailModel
            {
                ListProductType = await _productBusiness.GetListProductType(),
                ListProductDetail = await _productBusiness.GetListProductDetailAsync(sortOrder, currentFilter, searchString, page)
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var model = new ProductCreateViewModel
            {
                ProductCreate = new ProductCreate(),
                ListProductDetail = await _productBusiness.GetListProductDetail(),
            };
            return View(model);
        }

        [HttpPost]
        [TypeFilter(typeof(ModelStateAjaxFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreate productCreate)
        {
            await _productBusiness.CreateProductAsync(productCreate);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var model = new ProductUpdateViewModel
            {
                ProductUpdate = _productBusiness.GetProductByIdAsync(id),
                ListProductDetail = await _productBusiness.GetListProductDetail(),
            };
            return View(model);
        }

        [HttpPost]
        [TypeFilter(typeof(ModelStateAjaxFilter))]
        public async Task<IActionResult> UpdateProduct(ProductUpdate productUpdate)
        {
            await _productBusiness.UpdateProductAsync(productUpdate);
            return Ok(productUpdate.Id);
        }

        public async Task<IActionResult> ListProduct(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("name") ? "name_desc" : "name";

            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";

            //StaticAcc.Name = User.Claims.Where(x => x.Type == "name").FirstOrDefault().Value;

            if (searchString != null) page = 1;
            else searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;

            var model = new ProductModel
            {
                ListProductDetail = await _productBusiness.GetListProductDetail(),
                ListProduct = await _productBusiness.GetListProductAsync(sortOrder, currentFilter, searchString, page)
            };

            return View(model);
        }

    }
}
