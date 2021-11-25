using System.ComponentModel.DataAnnotations;
using static ShopOnline.Core.Models.Enum.AppEnum;

namespace ShopOnline.Core.Models.Account
{
    public class AccountLogin
    {
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class BaseInforAccount : AccountLogin
    {
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public TypeAcc TypeAcc { get; set; }
    }
}
