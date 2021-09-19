using System.ComponentModel.DataAnnotations;

namespace ShopOnline.Core.Models.Account
{
    public class AccountRegister
    {
        public string FullName { get; set; }

        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
