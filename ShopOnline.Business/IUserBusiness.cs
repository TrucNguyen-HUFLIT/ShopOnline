using ShopOnline.Core.Models.Account;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopOnline.Business
{
    public interface IUserBusiness
    {
        Task<ClaimsPrincipal> LoginAsync(AccountLogin accountLogin);

        Task<bool> RegisterAsync(AccountRegister accountRegister);

        Task ResetPasswordAsync(string email);
    }
}
