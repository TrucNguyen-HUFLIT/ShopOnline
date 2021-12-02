using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using static ShopOnline.Core.Models.Enum.AppEnum;

namespace ShopOnline.Core.Models
{
    public class UserInfor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public TypeAcc TypeAcc { get; set; }
        [NotMapped]
        public IFormFile UploadAvt { get; set; }
    }

    public class ProfileViewModel
    {
        public UserInfor UserInfor { get; set; }
    }
}
