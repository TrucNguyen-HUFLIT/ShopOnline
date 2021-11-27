using System.ComponentModel.DataAnnotations;
using static ShopOnline.Core.Models.Enum.AppEnum;

namespace ShopOnline.Core.Models.Account
{
    public class AccountLoginModel
    {
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class BaseInforAccountModel : AccountLoginModel
    {
        public string FullName { get; set; }
        
        public TypeAcc TypeAcc { get; set; }
    }

    public class AccountRegisterModel : AccountLoginModel
    {
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordModel
    {
        public string Email { get; set; }
       
        public string PhoneNumber { get; set; }
    }
}
