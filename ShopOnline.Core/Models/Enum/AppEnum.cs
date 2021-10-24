
namespace ShopOnline.Core.Models.Enum
{
    public class AppEnum
    {
        public enum TypeAcc
        {
            Admin = 1,
            Staff = 2,
            Customer = 3,
            Shipper = 4,
        }

        public enum StatusOrder
        {
            Processing = 1,
            Accepted = 2,
            Paid = 3,
            Delivering = 4,
            Completed = 5,
        }

        public enum ProductSize
        {
            Size_34 = 34,
            Size_35 = 35,
            Size_36 = 36,
            Size_37 = 37,
            Size_38 = 38,
            Size_39 = 39,
            Size_40 = 40,
            Size_41 = 41,
            Size_42 = 42,
            Size_43 = 43,
            Size_44 = 44,
            Size_45 = 45,
            Size_46 = 46,
            Size_47 = 47,
        }

        public enum ProductStatus
        {
            Available = 1,
            Unavailable = 2,
        }
    }
}
