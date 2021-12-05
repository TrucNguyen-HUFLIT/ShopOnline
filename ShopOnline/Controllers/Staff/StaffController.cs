using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Business.Staff;
using ShopOnline.Core.Filters;
using ShopOnline.Core.Models;
using ShopOnline.Core.Models.Staff;
using System;
using System.Threading.Tasks;

namespace ShopOnline.Controllers.Staff
{
    [Authorize(Roles = ROLE.ADMIN)]
    public class StaffController : Controller
    {
        private readonly IStaffBusiness _staffBusiness;

        public StaffController(IStaffBusiness staffBusiness)
        {
            _staffBusiness = staffBusiness;
        }
        public async Task<IActionResult> ListStaff(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("name") ? "name_desc" : "name";

            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";

            //StaticAcc.Name = User.Claims.Where(x => x.Type == "name").FirstOrDefault().Value;

            if (searchString != null) page = 1;
            else searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;

            var model = new StaffModel
            {
                ListStaff = await _staffBusiness.GetListStaffAsync(sortOrder, currentFilter, searchString, page)
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateStaff()
        {
            var model = new StaffCreateViewModel
            {
                StaffCreate = new StaffCreate(),
            };
            return View(model);
        }

        [HttpPost]
        [TypeFilter(typeof(ModelStateAjaxFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        public async Task<IActionResult> CreateStaff([FromForm] StaffCreate staffCreate)
        {
            await _staffBusiness.CreateAsync(staffCreate);
            return Ok();
        }

        [HttpGet]
        public IActionResult UpdateStaff(int id)
        {
            var model = new StaffEditViewModel
            {
                StaffEdit = _staffBusiness.GetStaffById(id),
            };
            return View(model);
        }

        [HttpPost]
        [TypeFilter(typeof(ModelStateAjaxFilter))]
        public async Task<IActionResult> UpdateStaff(StaffEdit staffEdit)
        {
            await _staffBusiness.EditAsync(staffEdit);
            return Ok(staffEdit.Id);
        }

        public async Task<IActionResult> DeleteStaff(StaffInfor staffInfor)
        {
            await _staffBusiness.DeleteStaffAsync(staffInfor);
            return Ok();
        }
    }
}
