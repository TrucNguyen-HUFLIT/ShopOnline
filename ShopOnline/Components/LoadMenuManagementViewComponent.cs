using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using static ShopOnline.Core.Models.Enum.AppEnum;

namespace ShopOnline.Components
{
    public class LoadMenuManagementViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string role = UserClaimsPrincipal.FindFirst(ClaimTypes.Role)?.Value;
            role = char.ToUpper(role[0]) + role.Substring(1);
            Enum.TryParse(role, out TypeAcc enumRole);

            var vm = new LoadMenuManagementViewModel
            {
                Role = enumRole
            };
            return View(vm);
        }
    }

    public class LoadMenuManagementViewModel
    {
        public TypeAcc Role { get; set; }
    }
}
