using System.ComponentModel.DataAnnotations;

namespace ShopOnline.Core.Models.Account
{
    public class AccountLogin
    {
        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
