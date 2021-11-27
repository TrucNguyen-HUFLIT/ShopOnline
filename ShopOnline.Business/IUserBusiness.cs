using ShopOnline.Core.Models.Account;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopOnline.Business
{
    public interface IUserBusiness
    {
        Task<ClaimsPrincipal> LoginAsync(AccountLoginModel accountLogin);

        Task<bool> RegisterAsync(AccountRegisterModel accountRegister);

        Task ResetPasswordAsync(ResetPasswordModel resetPasswordModel);
    }
}
