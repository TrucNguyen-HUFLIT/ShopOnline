using Microsoft.AspNetCore.Mvc;
using ShopOnline.Business.Staff;
using ShopOnline.Core.Filters;
using ShopOnline.Core.Models.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.Controllers.Staff
{
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
            var model = new StaffViewModel
            {
                staffCreate = new StaffCreate(),
            };
            return View(model);
        }

        [HttpPost]
        [TypeFilter(typeof(ModelStateAjaxFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        public async Task<IActionResult> CreateStaff([FromForm] StaffCreate staffCreate)
        {
            await _staffBusiness.CreateAsync(staffCreate);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateStaff(int id)
        {
            var model = new StaffViewModel
            {
                staffEdit = _staffBusiness.GetStaffById(id),
            };
            return View(model);
        }

        [HttpPost]
        [TypeFilter(typeof(ModelStateAjaxFilter))]
        public async Task<IActionResult> UpdateStaff(StaffEdit staffEdit)
        {
            await _staffBusiness.EditAsync(staffEdit);
            return RedirectToAction("Edit", new {id= staffEdit.Id});
        }

        public async Task<IActionResult> DeleteStaff(StaffInfor staffInfor)
        {
            await _staffBusiness.DeleteStaffAsync(staffInfor);
            return Ok();
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var model = new StaffViewModel
            {
                staffEdit = _staffBusiness.GetDataByClaim(User)
            };
            if(model==null)
            {
                return View(model);
            }    
            else
            {
                return NotFound();
            }    
         
        }

        public async Task<IActionResult> Profile(StaffEdit staffEdit)
        {
            await _staffBusiness.UpdateProfileAsync(staffEdit);
            return RedirectToAction("Profile");
        }
    }
}
