﻿using static ShopOnline.Core.Models.Enum.AppEnum;

namespace ShopOnline.Core.Entities
{
    public class StaffEntity : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public TypeAcc TypeAcc { get; set; }

    }
}
