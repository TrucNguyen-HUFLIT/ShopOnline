using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopOnline.Components
{
    public class LoadAccountOptionsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(bool isLayoutManager)
        {
            var vm = new LoadAccountOptionsViewModel();
            await Task.Run(() =>
            {
                var name = UserClaimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;
                var avatar = UserClaimsPrincipal.FindFirst("avatar")?.Value;

                var vm = new LoadAccountOptionsViewModel
                {
                    Name = name,
                    Avatar = avatar,
                    IsLayoutManager = isLayoutManager
                };
            });
            return View(vm);
        }
    }

    public class LoadAccountOptionsViewModel
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
        public bool IsLayoutManager { get; set; }
    }
}
