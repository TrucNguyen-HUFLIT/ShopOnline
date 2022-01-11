using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Business;
using ShopOnline.Core.Filters;
using ShopOnline.Core.Models;
using System.Threading.Tasks;

namespace ShopOnline.Controllers
{
    [Authorize(Roles = ROLE.ALL)]
    public class ProfileController : Controller
    {
        private readonly IUserBusiness _userBusiness;

        public ProfileController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> UpdateDetailAsync()
        {
            var model = new ProfileViewModel
            {
                UserInfor = await _userBusiness.GetUserInforByClaimAsync(User)
            };

            if (model.UserInfor != null)
                return View(model);
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDetailAsync(UserInfor userInfor)
        {
            await _userBusiness.UpdateProfileAsync(userInfor);
            return RedirectToAction("UpdateDetail");
        }

        [HttpGet]

        public async Task<IActionResult> ChangePasswordAsync()
        {
            var model = new ChangePasswordViewModel
            {
                ChangePassword = await _userBusiness.GetInforUserChangePassword(User)
            };
            if (model.ChangePassword != null)
                return View(model);
            else
                return NotFound();
        }

        [HttpPost]
        [TypeFilter(typeof(ModelStateAjaxFilter))]
        [TypeFilter(typeof(ExceptionFilter))]
        public async Task<IActionResult> ChangePasswordAsync(ChangePassword changePassword)
        {
            await _userBusiness.ChangePasswordUser(changePassword);
            return Ok();
        }
    }
}
