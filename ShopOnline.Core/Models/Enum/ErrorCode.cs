using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopOnline.Core.Models.Enum
{
    public enum ErrorCode
    {
        ErrorCode = 400,

        #region Account = ErrorCode + 1000
        Account = ErrorCode + 1000,

        //[Display(GroupName = "Account")]
        //[Description("Bad Request")]
        //BadRequest = Account,

        //[Display(GroupName = "Account")]
        //[Description("UnAuthorized")]
        //UnAuthorized = Account + 1,

        //[Display(GroupName = "Account")]
        //[Description("Forbidden")]
        //Forbidden = Account + 3,

        //[Display(GroupName = "Account")]
        //[Description("Not Found")]
        //NotFound = Account + 4,

        [Display(GroupName = "Account")]
        [Description("Email isn't incorrect")]
        WrongEmail = Account + 1,

        [Display(GroupName = "Account")]
        [Description("Password isn't incorrect")]
        WrongPassword = Account + 2,

        [Display(GroupName = "Account")]
        [Description("Phone number doesn't match")]
        PhoneNotMatch = Account + 3,

        [Display(GroupName = "Account")]
        [Description("Email already exists")]
        EmailExisted = Account + 4,

        [Display(GroupName = "Account")]
        [Description("Email doesn't exist")]
        EmailNotExisted = Account + 5,

        [Display(GroupName = "Account")]
        [Description("The server does not respond, please try again later!")]
        NotResponse = Account + 6,

        #endregion

        #region Cart = ErrorCode + 2000
        Cart = ErrorCode + 2000,

        [Display(GroupName = "Cart")]
        [Description("Out of stock. We have reduced your cart.")]
        OutOfStock = Cart + 1,

        [Display(GroupName = "Cart")]
        [Description("Not found this product in your cart")]
        NotFoundInCart = Cart + 2,

        #endregion
    }
}
