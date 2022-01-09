using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Business.Staff;
using ShopOnline.Core.Filters;
using ShopOnline.Core.Models;
using ShopOnline.Core.Models.Staff;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ShopOnline.Core.Models.Enum.AppEnum;

namespace ShopOnline.Controllers.Staff
{
    public class StaffController : Controller
    {
        private readonly IStaffBusiness _staffBusiness;

        public StaffController(IStaffBusiness staffBusiness)
        {
            _staffBusiness = staffBusiness;
        }

        [Authorize(Roles = ROLE.MANAGER)]
        public async Task<IActionResult> ListStaff(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("name") ? "name_desc" : "name";

            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";

            if (searchString != null) page = 1;
            else searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;

            var model = new StaffModel
            {
                ListStaff = await _staffBusiness.GetListStaffAccAsync(sortOrder, TypeAcc.Staff, searchString, page)
            };
            return View(model);
        }

        [Authorize(Roles = ROLE.MANAGER)]
        [HttpGet]
        public IActionResult CreateStaff()
        {
            var model = new StaffCreateViewModel
            {
                StaffCreate = new StaffCreate(),
            };
            return View(model);
        }

        [Authorize(Roles = ROLE.MANAGER)]
        [HttpPost]
        [TypeFilter(typeof(ModelStateAjaxFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        public async Task<IActionResult> CreateStaff([FromForm] StaffCreate staffCreate)
        {
            await _staffBusiness.CreateAsync(staffCreate, TypeAcc.Staff);
            return Ok();
        }

        [Authorize(Roles = ROLE.MANAGER)]
        [HttpGet]
        public IActionResult UpdateStaff(int id)
        {
            var model = new StaffEditViewModel
            {
                StaffEdit = _staffBusiness.GetStaffById(id, TypeAcc.Staff),
            };
            return View(model);
        }

        [Authorize(Roles = ROLE.MANAGER)]
        [HttpPost]
        [TypeFilter(typeof(ModelStateAjaxFilter))]
        public async Task<IActionResult> UpdateStaff(StaffEdit staffEdit)
        {
            await _staffBusiness.EditAsync(staffEdit, TypeAcc.Staff);
            return Ok(staffEdit.Id);
        }

        [Authorize(Roles = ROLE.MANAGER)]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            await _staffBusiness.DeleteStaffAsync(id, TypeAcc.Staff);
            return Ok();
        }

        [Authorize(Roles = ROLE.MANAGER)]
        public async Task<IActionResult> ListShipper(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("name") ? "name_desc" : "name";

            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";

            if (searchString != null) page = 1;
            else searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;

            var model = new StaffModel
            {
                ListStaff = await _staffBusiness.GetListShipperAccAsync(sortOrder, searchString, page)
            };
            return View(model);
        }

        [Authorize(Roles = ROLE.MANAGER)]
        [HttpGet]
        public IActionResult CreateShipper()
        {
            var model = new StaffCreateViewModel
            {
                StaffCreate = new StaffCreate(),
            };
            return View(model);
        }

        [Authorize(Roles = ROLE.MANAGER)]
        [HttpPost]
        [TypeFilter(typeof(ModelStateAjaxFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        public async Task<IActionResult> CreateShipper([FromForm] StaffCreate staffCreate)
        {
            await _staffBusiness.CreateShipperAsync(staffCreate);
            return Ok();
        }

        [Authorize(Roles = ROLE.MANAGER)]
        [HttpGet]
        public IActionResult UpdateShipper(int id)
        {
            var model = new StaffEditViewModel
            {
                StaffEdit = _staffBusiness.GetShipperById(id),
            };
            return View(model);
        }

        [Authorize(Roles = ROLE.MANAGER)]
        [HttpPost]
        [TypeFilter(typeof(ModelStateAjaxFilter))]
        public async Task<IActionResult> UpdateShipper(StaffEdit staffEdit)
        {
            await _staffBusiness.EditShipperAsync(staffEdit);
            return Ok(staffEdit.Id);
        }

        [Authorize(Roles = ROLE.MANAGER)]
        public async Task<IActionResult> DeleteShipper(int id)
        {
            await _staffBusiness.DeleteShipperAsync(id);
            return Ok();
        }

        [Authorize(Roles = ROLE.ADMIN)]
        public async Task<IActionResult> ListManager(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("name") ? "name_desc" : "name";

            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";

            if (searchString != null) page = 1;
            else searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;

            var model = new StaffModel
            {
                ListStaff = await _staffBusiness.GetListStaffAccAsync(sortOrder, TypeAcc.Manager, searchString, page)
            };
            return View(model);
        }

        [Authorize(Roles = ROLE.ADMIN)]
        [HttpGet]
        public IActionResult CreateManager()
        {
            var model = new StaffCreateViewModel
            {
                StaffCreate = new StaffCreate(),
            };
            return View(model);
        }

        [Authorize(Roles = ROLE.ADMIN)]
        [HttpPost]
        [TypeFilter(typeof(ModelStateAjaxFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        public async Task<IActionResult> CreateManager([FromForm] StaffCreate staffCreate)
        {
            await _staffBusiness.CreateAsync(staffCreate, TypeAcc.Manager);
            return Ok();
        }

        [Authorize(Roles = ROLE.ADMIN)]
        [HttpGet]
        public IActionResult UpdateManager(int id)
        {
            var model = new StaffEditViewModel
            {
                StaffEdit = _staffBusiness.GetStaffById(id, TypeAcc.Manager),
            };
            return View(model);
        }

        [Authorize(Roles = ROLE.ADMIN)]
        [HttpPost]
        [TypeFilter(typeof(ModelStateAjaxFilter))]
        public async Task<IActionResult> UpdateManager(StaffEdit staffEdit)
        {
            await _staffBusiness.EditAsync(staffEdit, TypeAcc.Manager);
            return Ok(staffEdit.Id);
        }

        [Authorize(Roles = ROLE.ADMIN)]
        public async Task<IActionResult> DeleteManager(int id)
        {
            await _staffBusiness.DeleteStaffAsync(id, TypeAcc.Manager);
            return Ok();
        }
    }
}
