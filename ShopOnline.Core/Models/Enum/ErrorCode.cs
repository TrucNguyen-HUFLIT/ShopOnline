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
        [Description("The email isn't incorrect")]
        WrongEmail = Account + 1,

        [Display(GroupName = "Account")]
        [Description("The password isn't incorrect")]
        WrongPassword = Account + 2,

        [Display(GroupName = "Account")]
        [Description("The phone number isn't incorrect")]
        WrongPhoneNumber = Account + 3,

        [Display(GroupName = "Account")]
        [Description("The email was existed")]
        EmailExisted = Account + 4,

        #endregion

    }
}
