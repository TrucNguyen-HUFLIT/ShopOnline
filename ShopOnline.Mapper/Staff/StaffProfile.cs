using AutoMapper;
using ShopOnline.Core.Entities;
using ShopOnline.Core.Models.Staff;
namespace ShopOnline.Mapper.Staff
{
    public class StaffProfile: Profile
    {
        public StaffProfile()
        {
            CreateMap<StaffEntity, StaffInfor>();
                
        }
    }
}
