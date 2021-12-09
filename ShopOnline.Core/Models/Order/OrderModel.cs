using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using X.PagedList;
using static ShopOnline.Core.Models.Enum.AppEnum;

namespace ShopOnline.Core.Models.Order
{
    public class OrderModel
    {
        public OrderInfor orderInfor { get; set; }
        public IPagedList<OrderInfor> ListOrder { get; set; }
    }
    public class OrderInfor
    {
        public int Id { get; set; }
        public DateTime OrderDay { get; set; }
        public StatusOrder StatusOrder { get; set; }
        public int IdCustomer{get; set;}
        public  int ExtraFee { get; set; }
    }
}