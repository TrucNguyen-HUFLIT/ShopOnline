
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
            Manager = 5,
        }

        public enum StatusOrder
        {
            Processing = 1,
            Accepted = 2,
            Delivering = 3,
            Completed = 4,
            Cancelled = 5,
        }

        public enum ProductStatus
        {
            Available = 1,
            Unavailable = 2,
        }

        public enum ReviewStatus
        {
            Waiting = 1,
            Approved = 2,
            Rejected = 3,
        }

        public enum PaymentMethod
        {
            ShipCod = 1,
            EWallet = 2,
            BankTransfer = 3,
        }
    }
}
