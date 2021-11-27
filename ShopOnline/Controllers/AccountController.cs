using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Business;
using ShopOnline.Core.Models.Account;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopOnline.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserBusiness _userBusiness;
        public AccountController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginModel accountLogin)
        {
            var claimsPrincipal = await _userBusiness.LoginAsync(accountLogin);

            if (claimsPrincipal != null)
            {
                await HttpContext.SignInAsync(claimsPrincipal);
                var role = claimsPrincipal.Claims.Where(x => x.Type == ClaimTypes.Role).FirstOrDefault().Value;
                return Ok(role);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return View("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterModel accountRegister)
        {
            bool isSuccess = await _userBusiness.RegisterAsync(accountRegister);
            if (isSuccess)
            {
                return Created("/Login", new AccountLoginModel
                {
                    Email = accountRegister.Email,
                    Password = accountRegister.Password
                });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View("ResetPassword");
        }

        [HttpPost]
        public async Task ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            await _userBusiness.ResetPasswordAsync(resetPasswordModel);
        }
    }
}
