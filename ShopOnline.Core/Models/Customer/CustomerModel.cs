﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopOnline.Core.Models.Customer
{
    public class CustomerModel
    {
    }
    public class CustomerInfor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }

        [NotMapped]
        public IFormFile UploadAvt { get; set; }
    }
}
